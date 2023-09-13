using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathFinder
{
    /*public List<Hovering> FindPath(Hovering start, Hovering end)
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
            }

            var adjTiles = GetAdjTiles(currentHovering);
        }
    }*/

    /*private object GetAdjTiles(Hovering current)
    {
        var map = MapManager.Map.dict;

        List<Hovering> adjs = new List<Hovering>();

        //top
        Vector2 locationToCheck = new Vector2(current.gridLoaction.x, current.gridLoaction.y + 1);
        if (map.ContainsKey(locationToCheck))
        {
            adjs.Add(map[locationToCheck]);
        }

        //bottom
        locationToCheck = new Vector2(current.gridLoaction.x, current.gridLoaction.y - 1);
        if (map.ContainsKey(locationToCheck))
        {
            adjs.Add(map[locationToCheck]);
        }
        
        //top left
        locationToCheck = new Vector2(current.gridLoaction.x - 0.75f, current.gridLoaction.y + 1);
        if (map.ContainsKey(locationToCheck))
        {
            adjs.Add(map[locationToCheck]);
        }
        
        //bottom left
        locationToCheck = new Vector2(current.gridLoaction.x, current.gridLoaction.y + 1);
        if (map.ContainsKey(locationToCheck))
        {
            adjs.Add(map[locationToCheck]);
        }
        
        //top right
        locationToCheck = new Vector2(current.gridLoaction.x, current.gridLoaction.y + 1);
        if (map.ContainsKey(locationToCheck))
        {
            adjs.Add(map[locationToCheck]);
        }
        
        //bottom right
        locationToCheck = new Vector2(current.gridLoaction.x, current.gridLoaction.y + 1);
        if (map.ContainsKey(locationToCheck))
        {
            adjs.Add(map[locationToCheck]);
        }

        return adjs;
    }*/
}