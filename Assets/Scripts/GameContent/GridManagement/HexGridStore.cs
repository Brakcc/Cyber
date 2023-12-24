using System;
using System.Collections.Generic;
using Enums.GridEnums;
using Enums.UnitEnums.KapaEnums;
using GameContent.Entity.NPC;
using GameContent.GameManagement;
using GameContent.GridManagement.HexPathFind;
using Interfaces.Unit;
using UnityEngine;

namespace GameContent.GridManagement
{
    public class HexGridStore : MonoBehaviour
    {
        #region fields
        
        #region Grid Data
        
        [SerializeField] private Hex[] mapData; 
        public readonly Dictionary<Vector3Int, Hex> hexTiles = new();
        private readonly Dictionary<Vector3Int, List<Vector3Int>> _neighbourgs = new();
        
        #endregion

        #region ComputerList

        public Relay[] relayList;
        [SerializeField] private Computer[] computerList;
        
        #endregion

        #region NPC
        public List<Vector3Int>[] NetworkList => _networkList;
        private readonly List<Vector3Int>[] _networkList = new List<Vector3Int>[(int)NetworkType.OldNet15];
        
        public int EmptySockets { get; set; }
        
        public readonly List<IEntity> emiters = new();
        #endregion

        public static HexGridStore hGs;
        
        #endregion

        #region methodes
        private void Awake() => hGs = this;

        #region  Map Gen
        
        public void OnIntMapAndEntities()
        {
            //NPC init
            for (var i = 0; i < _networkList.Length; i++)
            {
                _networkList[i] = new List<Vector3Int>();
            }
            
            //Circulation sur la Map
            foreach (var hex in mapData)
            {
                hexTiles[hex.HexCoords] = hex;
                if (hex.LocalNetwork == NetworkType.None)
                    continue;
                
                _networkList[(int)hex.LocalNetwork].Add(hex.HexCoords);
                hex.EnableGlowBaseNet();
            }
            
            //Depose des Sockets pour les merges de network
            foreach (var i in _networkList)
            {
                if (i.Count == 0) { EmptySockets++; }
            }

            EntityInit();
        }
        
        #endregion
        
        #region Enity On Map Init

        /// <summary>
        /// Initialise la grille pour detecter les pos des joueurs en début de partie pour rendre
        /// impossible les dep sur ces tiles
        /// </summary>
        private void EntityInit()
        {
            foreach (var unit in UnitGenManager.gGm.TeamLists.heroPlayer0)
            {
                var uEnt = unit.GetComponent<IEntity>();
                var hex = GetTile(uEnt.CurrentHexPos);
                
                hex.HasEntityOnIt = true;
                var unitTemp = unit.GetComponent<IUnit>();
                
                hex.SetEntity(unitTemp);
                
                uEnt.OnInit();

                if (!uEnt.IsNetworkEmiter) continue;
                
                emiters.Add(uEnt);
                uEnt.OnGenerateNet(uEnt.NetworkRange);
            }
            foreach (var unit in UnitGenManager.gGm.TeamLists.heroPlayer1)
            {
                var uEnt = unit.GetComponent<IEntity>();
                var hex = GetTile(uEnt.CurrentHexPos);
                
                hex.HasEntityOnIt = true;
                var unitTemp = unit.GetComponent<IUnit>();
                
                hex.SetEntity(unitTemp);
                
                uEnt.OnInit();

                if (!uEnt.IsNetworkEmiter) continue;
                
                emiters.Add(uEnt);
                uEnt.OnGenerateNet(uEnt.NetworkRange);
            }
            
            InitNpcEntity(relayList);
            
            InitNpcEntity(computerList);
        }

        #region NpcEntity
        
        private void InitNpcEntity<T>(IEnumerable<T> list) where T : IEntity
        {
            foreach (var t in list)
            {
                var hex = GetTile(t.CurrentHexPos);
                t.OnInit();
                hex.HasEntityOnIt = true;
            }
        }
        
        #endregion
        
        #endregion
        
        #region Map Data Access

        public Hex GetTile(Vector3Int hexCoords)
        {
            hexTiles.TryGetValue(hexCoords, out var results);
                    return results;
        }
        
        public List<Vector3Int> GetNeighbourgs(Vector3Int coords)
        {
            if (!hexTiles.ContainsKey(coords))
            {
                return new List<Vector3Int>();
            }

            if (_neighbourgs.TryGetValue(coords, out var neighbourgs))
            {
                return neighbourgs;
            }
        
            _neighbourgs.Add(coords, new List<Vector3Int>());
        
            foreach (var i in Direction.GetDirectionList(coords.x))
            {
                if (!hexTiles.ContainsKey(coords + i)) continue;
                _neighbourgs[coords].Add(coords + i);
            }
        
            return _neighbourgs[coords];
        }

        public Hex GetDirectionTile(Vector3Int targetPos, KapaDir dir) => dir switch
        {
            KapaDir.North => GetTile(GetNeighbourgs(targetPos)[0]),
            KapaDir.NorthEast => GetTile(GetNeighbourgs(targetPos)[1]),
            KapaDir.SouthEast => GetTile(GetNeighbourgs(targetPos)[2]),
            KapaDir.South => GetTile(GetNeighbourgs(targetPos)[3]),
            KapaDir.SouthWest => GetTile(GetNeighbourgs(targetPos)[4]),
            KapaDir.NorthWest => GetTile(GetNeighbourgs(targetPos)[5]),
            KapaDir.Default => GetTile(targetPos),
            _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null)
        };
        
        #endregion

        #region network params

        public void OnAddEmiter(Entity.Entity ent) => emiters.Add(ent);

        public void OnDelEmiter(Entity.Entity ent)
        {
            if (emiters == null) return;
            emiters.Remove(ent);
        }

        public void OnAddToNetwork(NetworkType net, IEnumerable<Vector3Int> newNet) => _networkList[(int)net].AddRange(newNet);
        public void OnAddToNetwork(NetworkType net, Vector3Int newNet) => _networkList[(int)net].Add(newNet);

        public void OnDelFromNetwork(NetworkType net, List<Vector3Int> oldNet)
        {
            foreach (var i in oldNet) { _networkList[(int)net].Remove(i); }
        }
        public void OnDelFromNetwork(NetworkType net, Vector3Int oldNet) => _networkList[(int)net].Remove(oldNet);

        public List<Vector3Int> GetNetwork(Vector3Int pos)
        {
            List<Vector3Int> allNet = new();
            foreach (var net in _networkList)
            {
                if (net.Contains(pos)) { allNet.AddRange(net); }
            }
            return allNet;
        }

        public bool IsOnNetwork(Vector3Int pos, NetworkType net) => _networkList[(int)net].Contains(pos);
        public bool IsOnNetwork(Vector3Int pos)
        {
            foreach (var i in _networkList)
            {
                if (i.Contains(pos)) { return true; }
            }
            return false;
        }
        #endregion

        #region Computer

        public void HandlePCHacked(RelayTarget whichRelay)
        {
            if (relayList[(int)whichRelay].GotHacked) return;

            foreach (var i in GetNeighbourgs(relayList[(int)whichRelay].CurrentHexPos))
            {
                var tile = GetTile(i);
                
                tile.CurrentType = HexType.Walkable;
                tile.RelayTarget = RelayTarget.None;
            }
            relayList[(int)whichRelay].OnNetworkHack();
        }
        
        #endregion
        
        #endregion
    }
}