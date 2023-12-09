using System.Collections.Generic;
using Enums.GridEnums;
using GameContent.Entity.Network;
using GameContent.GridManagement.HexPathFind;
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
        public List<Vector3Int>[] ComputerToHack { get; } = new List<Vector3Int>[3];
        public Computer[] computerList;
        #endregion

        #region Network
        public List<Vector3Int>[] NetworkList => _networkList;
        readonly List<Vector3Int>[] _networkList = new List<Vector3Int>[(int)NetworkType.None];
        
        public int EmptySockets { get; set; }

        [Tooltip("Only for Hackers and Turrets")]
        public List<Entity.Entity> emiters = new();
        #endregion

        public static HexGridStore hGs;
        #endregion

        #region methodes
        void Awake() => hGs = this;

        void Start()
        {
            OnIntMap();
        }

        #region  Map Gen
        
        void OnIntMap()
        {
            //Network init
            for (int i = 0; i < _networkList.Length; i++)
            {
                _networkList[i] = new();
            }
            //Computer init
            for (int i = 0; i < ComputerToHack.Length; i++)
            {
                ComputerToHack[i] = new();
            }
            computerList = new Computer[3];
            
            //Circulation sur la Map
            foreach (var hex in mapData)
            {
                hexTiles[hex.HexCoords] = hex;
                if (hex.LocalNetwork != NetworkType.None)
                {
                    _networkList[(int)hex.LocalNetwork].Add(hex.HexCoords);
                    hex.EnableGlowBaseNet();
                }

                if (hex.IsComputer())
                {
                    ComputerToHack[(int)hex.ComputerTarget].Add(hex.HexCoords);
                }
            }
            
            //Depose des Sockets pour les merges de network
            foreach (var i in _networkList)
            {
                if (i.Count == 0) { EmptySockets++; }
            }
        }
        
        #endregion

        #region Map Data Access

        public Hex GetTile(Vector3Int hexCoords)
        {
            hexTiles.TryGetValue(hexCoords, out Hex results);
                    return results;
        }
        
        public List<Vector3Int> GetNeighbourgs(Vector3Int coords)
        {
            if (!hexTiles.ContainsKey(coords)) { return new List<Vector3Int>(); }
            if (_neighbourgs.ContainsKey(coords)) { return _neighbourgs[coords]; }
        
            _neighbourgs.Add(coords, new List<Vector3Int>());
        
            foreach (var i in Direction.GetDirectionList(coords.x))
            {
                if (!hexTiles.ContainsKey(coords + i)) continue;
                _neighbourgs[coords].Add(coords + i);
            }
        
            return _neighbourgs[coords];
        }

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

        public void HandlePCHacked(ComputerTarget whichPC)
        {
            if (computerList[(int)whichPC].GotHacked) return;

            foreach (var i in ComputerToHack[(int)whichPC])
            {
                GetTile(i).CurrentType = HexType.Walkable;
            }
            computerList[(int)whichPC].HandleComputerHack();
            computerList[(int)whichPC].GotHacked = true;
        }
        #endregion
        #endregion
    }
}