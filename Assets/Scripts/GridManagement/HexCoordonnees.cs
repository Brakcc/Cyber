using UnityEngine;

public struct HexCoordonnees : IHexCoord
{
    #region fields
    public Vector3Int OffsetCoordonnees { get; set; }
    #endregion

    #region constructeur
    /// <summary>
    /// Donne les coordonnées en int d'une tuile dans la grid 
    /// </summary>
    /// <param name="gameObject"></param>
    public HexCoordonnees (GameObject gameObject)
    {
        OffsetCoordonnees = GetClosestHex(gameObject.transform.position);
    }
    #endregion

    #region methodes
    public static Vector3Int GetClosestHex(Vector3 worldPos)
    {
        float temp = ((worldPos.x / 0.05f) / 19);
        int x = Mathf.RoundToInt(temp);
        //Debug.Log(worldPos + " and " + temp + " and" + x);
        //int x = worldPos.x >= 0 ? Mathf.CeilToInt(worldPos.x) : Mathf.FloorToInt(worldPos.x);
        int y = worldPos.y >= 0 ? Mathf.CeilToInt(worldPos.y) : Mathf.FloorToInt(worldPos.y);
        worldPos.z = 0;
        int z = (int)worldPos.z;
        return new Vector3Int(x, y, z);
    }
    #endregion
}
