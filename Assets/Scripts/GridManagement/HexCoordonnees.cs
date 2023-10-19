using UnityEngine;

public struct HexCoordonnees : IHexCoord
{
    #region fields
    public Vector3Int offsetCoordonnees { get; set; }
    #endregion

    #region constructeur
    /// <summary>
    /// Donne les coordonnées en int d'une tuile dans la grid 
    /// </summary>
    /// <param name="gameObject"></param>
    public HexCoordonnees (GameObject gameObject)
    {
        offsetCoordonnees = GetClosestHex(gameObject.transform.position);
    }
    #endregion

    #region methodes
    public static Vector3Int GetClosestHex(Vector3 worldPos)
    {
        worldPos.z = 0;
        int x = Mathf.CeilToInt(worldPos.x);
        int y = Mathf.CeilToInt(worldPos.y);
        int z = Mathf.CeilToInt(worldPos.z);
        return new Vector3Int(x, y, z);
    }
    #endregion
}
