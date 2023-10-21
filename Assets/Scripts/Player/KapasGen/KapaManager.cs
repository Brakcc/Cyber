using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KapaManager
{
    #region methodes
    public static List<Hex> GenerateButtonPos(Unit unit, HexGridStore hexGrid)
    {
        List<Hex> buttonList = new();
        foreach (var bC in hexGrid.GetNeighbourgs(unit.CurrentHexPos)) 
        {
            buttonList.Add(hexGrid.GetTile(bC));
        }
        return buttonList;
    }

    public static void GetNorthButton()
    {

    }
    #endregion
}
