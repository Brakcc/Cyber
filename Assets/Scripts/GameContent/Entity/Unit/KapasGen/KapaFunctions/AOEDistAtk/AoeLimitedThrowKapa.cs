using Interfaces.Kapas;
using UnityEngine;

namespace GameContent.Entity.Unit.KapasGen.KapaFunctions.AOEDistAtk
{
    public static class AoeLimitedThrowKapa
    {
        public static Vector3Int[] ConcatOddPattern(IKapa kapa)
        {
            var newPat = new Vector3Int[kapa.OddNTiles.Length * 6];
            
            for (var i = 0; i < kapa.OddNTiles.Length; i++)
            {
                newPat[i] = kapa.OddNTiles[i];
            }
            for (var i = 0; i < kapa.OddENTiles.Length; i++)
            {
                newPat[i] = kapa.OddENTiles[i];
            }
            for (var i = 0; i < kapa.OddESTiles.Length; i++)
            {
                newPat[i] = kapa.OddESTiles[i];
            }
            for (var i = 0; i < kapa.OddSTiles.Length; i++)
            {
                newPat[i] = kapa.OddSTiles[i];
            }
            for (var i = 0; i < kapa.OddWSTiles.Length; i++)
            {
                newPat[i] = kapa.OddWSTiles[i];
            }
            for (var i = 0; i < kapa.OddWNTiles.Length; i++)
            {
                newPat[i] = kapa.OddWNTiles[i];
            }
            return newPat;
        }
        
        public static Vector3Int[] ConcatEvenPattern(IKapa kapa)
        {
            var newPat = new Vector3Int[kapa.EvenNTiles.Length * 6];
            
            for (var i = 0; i < kapa.EvenNTiles.Length; i++)
            {
                newPat[i] = kapa.EvenNTiles[i];
            }
            for (var i = 0; i < kapa.EvenENTiles.Length; i++)
            {
                newPat[i] = kapa.EvenENTiles[i];
            }
            for (var i = 0; i < kapa.EvenESTiles.Length; i++)
            {
                newPat[i] = kapa.EvenESTiles[i];
            }
            for (var i = 0; i < kapa.EvenSTiles.Length; i++)
            {
                newPat[i] = kapa.EvenSTiles[i];
            }
            for (var i = 0; i < kapa.EvenWSTiles.Length; i++)
            {
                newPat[i] = kapa.EvenWSTiles[i];
            }
            for (var i = 0; i < kapa.EvenWNTiles.Length; i++)
            {
                newPat[i] = kapa.EvenWNTiles[i];
            }
            return newPat;
        }
    }
}