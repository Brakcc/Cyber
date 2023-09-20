using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public static class PathFinder
{
    public static List<Hovering> FindPath(Hovering start, Hovering end)
    {
        List<Hovering> openList = new List<Hovering>();
        List<Hovering> closedList = new List<Hovering>();

        openList.Add(start);

        while (openList.Count > 0)
        {
            Hovering currentHovering = openList.OrderBy(x => x.F).First();

            openList.Remove(currentHovering);
            closedList.Add(currentHovering);

            if (currentHovering == end)
            {
                // :)
                return GetFinishedList(start, end);
            }

            var adjTiles = MapManager.Map.GetAdjTiles(currentHovering);

            foreach (var adjTile in adjTiles) 
            {
                //1 = need to jump on the height
                if (adjTile.isBlocked || closedList.Contains(adjTile) || Mathf.Abs(currentHovering.gridLoaction.z - adjTile.gridLoaction.z) > 1)
                {
                    continue;
                }

                adjTile.G = GetManhattenDistance(start, adjTile);
                adjTile.H = GetManhattenDistance(end, adjTile);

                adjTile.previous = currentHovering;

                if (!openList.Contains(adjTile))
                {
                    openList.Add(adjTile);
                }
            }
        }

        return new List<Hovering>();
    }

    private static List<Hovering> GetFinishedList(Hovering start, Hovering end)
    {
        List<Hovering> finishedList = new List<Hovering>();

        Hovering current = end;

        while (current != start) 
        {
            finishedList.Add(current);
            current = current.previous;
        }

        finishedList.Reverse();

        return finishedList;
    }

    private static int GetManhattenDistance(Hovering start, Hovering adj) 
    {
        return Mathf.Abs(start.gridLoaction.x -  adj.gridLoaction.x) + Mathf.Abs(start.gridLoaction.y - adj.gridLoaction.y);
    }
}