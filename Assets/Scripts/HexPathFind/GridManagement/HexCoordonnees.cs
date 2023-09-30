using UnityEngine;

public struct HexCoordonnees : IHexCoord
{
    #region fields
    private static float xOffset = 1;
    private static float yOffset = 1;
    private static float zOffset = 1;

    public Vector3Int offsetCoordonnees { get; set; }
    #endregion

    #region constructeur
    /// <summary>
    /// Donne les coordonn�es en int d'une tuile dans la grid 
    /// </summary>
    /// <param name="gameObject"></param>
    public HexCoordonnees (GameObject gameObject)
    {
        int x = Mathf.CeilToInt(gameObject.transform.position.x / xOffset);
        int y = Mathf.CeilToInt(gameObject.transform.position.y / yOffset);
        int z = Mathf.CeilToInt(gameObject.transform.position.z / zOffset);
        offsetCoordonnees = new Vector3Int(x, y, z);
    }
    #endregion
}