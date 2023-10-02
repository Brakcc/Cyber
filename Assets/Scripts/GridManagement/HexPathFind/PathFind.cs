using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFind
{
    #region methodes
    public static PathResult PathGetRange(HexGridStore hexGrid, Vector3Int startHex, int movePoints)
    {
        Dictionary<Vector3Int, Vector3Int?> processedNodes = new Dictionary<Vector3Int, Vector3Int?>();
        Dictionary<Vector3Int, int> totalCost = new Dictionary<Vector3Int, int>();
        Queue<Vector3Int> nextNodes = new Queue<Vector3Int>();

        nextNodes.Enqueue(startHex);
        totalCost.Add(startHex, 0);
        processedNodes.Add(startHex, null);

        while (nextNodes.Count > 0)
        {
            Vector3Int currentNode = nextNodes.Dequeue();
            foreach (Vector3Int adjPos in hexGrid.GetNeighbourgs(currentNode))
            {
                if (hexGrid.GetTile(adjPos).IsObstacle()) { continue; }

                int nodeCost = hexGrid.GetTile(adjPos).GetValue();
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

    public static List<Vector3Int> GeneratePath(Vector3Int current, Dictionary<Vector3Int, Vector3Int?> proNodes)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        path.Add(current);
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

    public List<Vector3Int> GetPathTo(Vector3Int dest)
    {
        if (!calculatedNodes.ContainsKey(dest)) { return new List<Vector3Int>(); }
        return PathFind.GeneratePath(dest, calculatedNodes);
    }

    public bool IsHexPosInRange(Vector3Int pos) => calculatedNodes.ContainsKey(pos);

    public IEnumerable<Vector3Int> GetRangePositions() => calculatedNodes.Keys;
}