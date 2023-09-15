using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Range
{
    public List<Hovering> GetRangeTile(Hovering startTile, int range)
    {
        var inRange = new List<Hovering>();
        int count = 0;

        inRange.Add(startTile);

        var tileForPreviousStep = new List<Hovering>();
        tileForPreviousStep.Add(startTile);

        while (count < range)
        {
            var adjTile = new List<Hovering>();

            foreach (var i in tileForPreviousStep)
            {
                adjTile.AddRange(MapManager.Map.GetAdjTiles(i));
            }

            inRange.AddRange(adjTile);
            tileForPreviousStep = adjTile.Distinct().ToList();
            count++;
        }

        return inRange.Distinct().ToList();
    }
}
