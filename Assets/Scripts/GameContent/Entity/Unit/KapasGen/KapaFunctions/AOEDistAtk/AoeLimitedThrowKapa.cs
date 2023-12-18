using Interfaces.Kapas;
using UnityEngine;
using System.Collections.Generic;

namespace GameContent.Entity.Unit.KapasGen.KapaFunctions.AOEDistAtk
{
    public static class AoeLimitedThrowKapa
    {
        public static List<Vector3Int> ConcatOddPattern(Vector3Int pos, IKapa kapa)
        {
            var newPat = new List<Vector3Int>();
            
            newPat.AddRange(kapa.OddNTiles);
            newPat.AddRange(kapa.OddENTiles);
            newPat.AddRange(kapa.OddESTiles);
            newPat.AddRange(kapa.OddSTiles);
            newPat.AddRange(kapa.OddWSTiles);
            newPat.AddRange(kapa.OddWNTiles);

            for (var i = 0; i < newPat.Count; i++)
            {
                newPat[i] += pos;
            }
            
            return newPat;
        }
        
        public static List<Vector3Int> ConcatEvenPattern(Vector3Int pos, IKapa kapa)
        {
            var newPat = new List<Vector3Int>();
            
            newPat.AddRange(kapa.EvenNTiles);
            newPat.AddRange(kapa.EvenENTiles);
            newPat.AddRange(kapa.EvenESTiles);
            newPat.AddRange(kapa.EvenSTiles);
            newPat.AddRange(kapa.EvenWSTiles);
            newPat.AddRange(kapa.EvenWNTiles);
            
            for (var i = 0; i < newPat.Count; i++)
            {
                newPat[i] += pos;
            }
            
            return newPat;
        }
    }
}