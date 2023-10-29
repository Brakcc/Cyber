using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFind
{
    #region methodes
    /// <summary>
    /// Calcul en A* BFS pour determiner l'ensemble des tiles possibles pour une Unit à circuler dessus
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
            Vector3Int currentNode = nextNodes.Dequeue();
            foreach (Vector3Int adjPos in hexGrid.GetNeighbourgs(currentNode))
            {
                var h = hexGrid.GetTile(adjPos);
                if (h.IsObstacle() || h.HasPlayerOnIt) continue;

                int nodeCost = h.GetValue();
                int currentCost = totalCost[currentNode];
                int newCost = currentCost + nodeCost;

                if (newCost <= movePoints)
                {
                    if (!processedNodes.ContainsKey(adjPos))
                    {
                        processedNodes[adjPos] = currentNode;
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
        }
        return new PathResult { calculatedNodes = processedNodes };
    }

    /// <summary>
    /// oui je refais le pathfind pour juste changer le fait de pouvoir traverser l'ennemi et pas les murs ptn
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
            Vector3Int currentNode = nextNodes.Dequeue();
            foreach (Vector3Int adjPos in hexGrid.GetNeighbourgs(currentNode))
            {
                var h = hexGrid.GetTile(adjPos);
                //La seule dif avec la fonction d'avant est sur cette ligne ._.
                if (h.IsObstacle()) continue;

                int nodeCost = h.GetValue();
                int currentCost = totalCost[currentNode];
                int newCost = currentCost + nodeCost;

                if (newCost <= movePoints)
                {
                    if (!processedNodes.ContainsKey(adjPos))
                    {
                        processedNodes[adjPos] = currentNode;
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
        }
        return new PathResult { calculatedNodes = processedNodes };
    }

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
    #endregion
}

public struct PathResult
{
    public Dictionary<Vector3Int, Vector3Int?> calculatedNodes;

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

    public readonly bool IsHexPosInRange(Vector3Int pos) => calculatedNodes.ContainsKey(pos);

    public readonly IEnumerable<Vector3Int> GetRangePositions() => calculatedNodes.Keys;
}