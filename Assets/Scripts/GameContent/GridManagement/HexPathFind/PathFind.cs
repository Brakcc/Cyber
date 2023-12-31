using System.Collections.Generic;
using System.Linq;
using Enums.GridEnums;
using UnityEngine;

namespace GameContent.GridManagement.HexPathFind
{
    public static class PathFind
    {
        #region methodes
        
        #region pathFind methodes
        /// <summary>
        /// Calcul en A* BFS pour determiner l'ensemble des tiles possibles pour une Unit � circuler dessus
        /// </summary>
        /// <param name="hexGrid"></param>
        /// <param name="startHex"></param>
        /// <param name="movePoints"></param>
        /// <returns></returns>
        public static PathResult PathGetRange(HexGridStore hexGrid, Vector3Int startHex, int movePoints)
        {
            Dictionary<Vector3Int, Vector3Int?> processedNodes = new();
            Dictionary<Vector3Int, int> totalCost = new();
            Queue<Vector3Int> nextNodes = new();

            nextNodes.Enqueue(startHex);
            totalCost.Add(startHex, 0);
            processedNodes.Add(startHex, null);

            while (nextNodes.Count > 0)
            {
                var currentNode = nextNodes.Dequeue();
                foreach (var adjPos in hexGrid.GetNeighbourgs(currentNode))
                {
                    var h = hexGrid.GetTile(adjPos);
                    if (h.IsObstacle() || h.HasEntityOnIt) continue;

                    var nodeCost = h.GetValue();
                    var currentCost = totalCost[currentNode];
                    var newCost = currentCost + nodeCost;

                    if (newCost > movePoints) continue;
                    
                    if (processedNodes.TryAdd(adjPos, currentNode))
                    {
                        totalCost[adjPos] = newCost;
                        nextNodes.Enqueue(adjPos);
                    }
                    else if (totalCost[adjPos] > newCost)
                    {
                        totalCost[adjPos] = newCost;
                        processedNodes[adjPos] = currentNode;
                    }
                }
            }
            return new PathResult { calculatedNodes = processedNodes };
        }

        /// <summary>
        /// PathFind prennant en compte seulement les obstacles et traverse un nombre limit� d'Unit
        /// </summary>
        /// <param name="hexGrid"></param>
        /// <param name="startHex"></param>
        /// <param name="movePoints"></param>
        /// <returns></returns>
        public static PathResult PathKapaVerif(HexGridStore hexGrid, Vector3Int startHex, int movePoints)
        {
            Dictionary<Vector3Int, Vector3Int?> processedNodes = new();
            Dictionary<Vector3Int, int> totalCost = new();
            Queue<Vector3Int> nextNodes = new();

            nextNodes.Enqueue(startHex);
            totalCost.Add(startHex, 0);
            processedNodes.Add(startHex, null);

            while (nextNodes.Count > 0)
            {
                var currentNode = nextNodes.Dequeue();
                foreach (var adjPos in hexGrid.GetNeighbourgs(currentNode))
                {
                    var h = hexGrid.GetTile(adjPos);
                    if (h.IsObstacle()) continue; //La seule dif avec la fonction d'avant est sur cette ligne ._.

                    var nodeCost = h.GetValue();
                    var currentCost = totalCost[currentNode];
                    var newCost = currentCost + nodeCost;

                    if (newCost > movePoints) continue;
                    
                    if (processedNodes.TryAdd(adjPos, currentNode))
                    {
                        totalCost[adjPos] = newCost;
                        nextNodes.Enqueue(adjPos);
                    }
                    else if (totalCost[adjPos] > newCost)
                    {
                        totalCost[adjPos] = newCost;
                        processedNodes[adjPos] = currentNode;
                    }
                }
            }
            return new PathResult { calculatedNodes = processedNodes };
        }

        /// <summary>
        /// pathfind parfait pour comparaison avec les tiles de Kapas
        /// </summary>
        /// <param name="hexGrid"></param>
        /// <param name="startHex"></param>
        /// <param name="movePoints"></param>
        /// <returns></returns>
        public static PathResult PerfectPath(HexGridStore hexGrid, Vector3Int startHex, int movePoints)
        {
            Dictionary<Vector3Int, Vector3Int?> processedNodes = new();
            Dictionary<Vector3Int, int> totalCost = new();
            Queue<Vector3Int> nextNodes = new();

            nextNodes.Enqueue(startHex);
            totalCost.Add(startHex, 0);
            processedNodes.Add(startHex, null);

            while (nextNodes.Count > 0)
            {
                var currentNode = nextNodes.Dequeue();
                foreach (var adjPos in hexGrid.GetNeighbourgs(currentNode))
                {
                    //a plus la verif :D
                    //on utilise la valeur d'un walkable pour comparer sans consid�rer d'obstacle
                    const int nodeCost = (int)HexType.Walkable;
                    var currentCost = totalCost[currentNode];
                    var newCost = currentCost + nodeCost;

                    if (newCost > movePoints) continue;
                    
                    if (processedNodes.TryAdd(adjPos, currentNode))
                    {
                        totalCost[adjPos] = newCost;
                        nextNodes.Enqueue(adjPos);
                    }
                    else if (totalCost[adjPos] > newCost)
                    {
                        totalCost[adjPos] = newCost;
                        processedNodes[adjPos] = currentNode;
                    }
                }
            }
            return new PathResult { calculatedNodes = processedNodes };
        }
        #endregion

        #region Path generation
        
        /// <summary>
        /// Generation du chemin le plus court avec le pathFind pour ensuite l'inverser afin de placer la liste dans le bon sens
        /// </summary>
        /// <param name="current"></param>
        /// <param name="proNodes"></param>
        /// <returns></returns>
        public static List<Vector3Int> GeneratePath(Vector3Int current, Dictionary<Vector3Int, Vector3Int?> proNodes)
        {
            List<Vector3Int> path = new() { current };
            while (proNodes[current] != null)
            {
                path.Add(proNodes[current].Value);
                current = proNodes[current].Value;
            }
            path.Reverse();
            return path.Skip(1).ToList();
        }

        /// <summary>
        /// Generation du chemin de verif pour les tiles selectionnees par un pattern de Kapa
        /// </summary>
        /// <param name="hG"></param>
        /// <param name="current"></param>
        /// <param name="proNodes"></param>
        /// <param name="maxPlayerPierce"></param>
        /// <returns></returns>
        public static List<Vector3Int> GenerateKapaPath(HexGridStore hG, Vector3Int current, Dictionary<Vector3Int, Vector3Int?> proNodes, int maxPlayerPierce/*, int teamNb*/)
        {
            int piercedUnit = 0;

            List<Vector3Int> path = new() { current };
            while (proNodes[current] != null)
            {
                if (piercedUnit > maxPlayerPierce)
                {
                    current = proNodes[current].Value;
                    continue;
                }
                if (!hG.GetTile(proNodes[current].Value).HasEntityOnIt)
                {
                    path.Add(proNodes[current].Value);
                }
                else
                {
                    piercedUnit++;
                    path.Add(proNodes[current].Value);
                    /*var tNb = hG.GetTile(proNodes[current].Value).GetUnit().TeamNumber;
                    if (tNb != teamNb)
                    {
                        piercedUnit++;
                        path.Add(proNodes[current].Value);
                    }*/
                }
                current = proNodes[current].Value;
            }
            path.Reverse();
            return path.Skip(1).ToList();
        }
        
        #endregion
        
        #endregion
    }

    public struct PathResult
    {
        public Dictionary<Vector3Int, Vector3Int?> calculatedNodes;

        #region methodes
        
        /// <summary>
        /// Renvoit une liste de Vector3Int qui est le chemin de l'Unit vers une tile en Vector3Int
        /// </summary>
        /// <param name="dest"></param>
        /// <returns></returns>
        public readonly List<Vector3Int> GetPathTo(Vector3Int dest)
        {
            if (!calculatedNodes.ContainsKey(dest)) { return new List<Vector3Int>(); }
            return PathFind.GeneratePath(dest, calculatedNodes);
        }

        /// <summary>
        /// Renvoit une liste de vector3Int qui permet de determiner selon un nombre Max d'Unit percee si une competence peut etre active sur une tile ou non
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="hG"></param>
        /// <param name="maxPierced">nombre max d'Unit traversee</param>
        /// <returns></returns>
        public readonly List<Vector3Int> GetKapaPathTo(Vector3Int dest, HexGridStore hG, int maxPierced/*, int teamNb*/)
        {
            if (!calculatedNodes.ContainsKey(dest)) { return new List<Vector3Int>(); }
            return PathFind.GenerateKapaPath(hG, dest, calculatedNodes, maxPierced/*, teamNb*/);
        }

        public readonly bool IsHexPosInRange(Vector3Int pos) => calculatedNodes.ContainsKey(pos);

        /// <summary>
        /// Enumerable des positions calculees dans la range d'un pathFind
        /// </summary>
        /// <returns></returns>
        public readonly IEnumerable<Vector3Int> GetRangePositions() => calculatedNodes.Keys;
        
        #endregion
    }
}