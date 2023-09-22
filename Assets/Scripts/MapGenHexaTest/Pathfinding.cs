using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Pathfinding 
{  
    public static List<NodeBase> FindPath(NodeBase startNode, NodeBase targetNode) {
        var toSearch = new List<NodeBase>() { startNode };
        var processed = new List<NodeBase>();

        while (toSearch.Any()) 
        {
            var current = toSearch[0];
            foreach (var i in toSearch) 
            { 
                if (i.F < current.F || i.F == current.F && i.H < current.H) { current = i; } 
            }

            processed.Add(current);
            toSearch.Remove(current);

            if (current == targetNode) 
            {
                var currentPathTile = targetNode;
                var path = new List<NodeBase>();
                var count = 100;
                while (currentPathTile != startNode) 
                {
                    path.Add(currentPathTile);
                    currentPathTile = currentPathTile.Connection;
                    count--;
                    if (count < 0) throw new Exception();
                }
                return path;
            }

            foreach (var neighbor in current.Neighbors.Where(t => t.Walkable && !processed.Contains(t))) 
            {
                var inSearch = toSearch.Contains(neighbor);

                var costToNeighbor = current.G + current.GetDistance(neighbor);

                if (!inSearch || costToNeighbor < neighbor.G) 
                {
                    neighbor.SetG(costToNeighbor);
                    neighbor.SetConnection(current);

                    if (!inSearch) {
                        neighbor.SetH(neighbor.GetDistance(targetNode));
                        toSearch.Add(neighbor);
                    }
                }
            }
        }
        return null;
    }
}