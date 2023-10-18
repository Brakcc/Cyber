using UnityEngine;

public abstract class AKapaSO : ScriptableObject, IKapa, IKapasDatas
{
    #region fields to herit
    public abstract string KapaName { get; }
    public abstract int ID { get; }
    public abstract int Cost { get; }
    public abstract string Description { get; }
    public abstract EffectType EffectType { get; }
    public abstract KapaType KapaType { get; }
    public abstract KapaUISO KapaUI { get; }
    public abstract Vector3Int[] Patern { get; }
    #endregion

    #region paterns to herit
    //North tiles
    public abstract Vector2Int[] OddNTiles { get; set; }
    public abstract Vector2Int[] EvenNTiles { get; set; }
    //EN tiles
    public abstract Vector2Int[] OddENTiles { get; set; }
    public abstract Vector2Int[] EvenENTiles { get; set; }
    //ES tiles
    public abstract Vector2Int[] OddESTiles { get; set; }
    public abstract Vector2Int[] EvenESTiles { get; set; }
    //S tiles
    public abstract Vector2Int[] OddSTiles { get; set; }
    public abstract Vector2Int[] EvenSTiles { get; set; }
    //WS tiles
    public abstract Vector2Int[] OddWSTiles { get; set; }
    public abstract Vector2Int[] EvenWSTiles { get; set; }
    //WN tiles
    public abstract Vector2Int[] OddWNTiles { get; set; }
    public abstract Vector2Int[] EvenWNTiles { get; set; }
    #endregion

    #region patern gen (to herit)
    #region Ntiles
    /// <summary>
    /// génère l'array de coord Unity des Kapas vers le Nord, tuiles Impaires
    /// </summary>
    /// <param name="pat"></param>
    /// <returns></returns>
    public virtual Vector2Int[] GetOddNtiles(Vector3Int[] pat)
    {
        Vector2Int[] w = new Vector2Int[pat.Length];
        for (int i = 0; i < pat.Length; i++)
        {
            Vector3Int j = new(pat[i].x, pat[i].y, pat[i].z);
            w[i] = HexToOrthoCoords.GetOddOrthoCoord(j);
        }
        return w;
    }
    /// <summary>
    /// génère l'array de coord Unity des Kapas vers le Nord, tuiles Paires
    /// </summary>
    /// <param name="pat"></param>
    /// <returns></returns>
    public virtual Vector2Int[] GetEvenNtiles(Vector3Int[] pat)
    {
        Vector2Int[] w = new Vector2Int[pat.Length];
        for (int i = 0; i < pat.Length; i++)
        {
            Vector3Int j = new(pat[i].x, pat[i].y, pat[i].z);
            w[i] = HexToOrthoCoords.GetEvenOrthoCoord(j);
        }
        return w;
    }
    #endregion
    #region ENtiles
    /// <summary>
    /// génère l'array de coord Unity des Kapas vers le Nord Est, tuiles Impaires
    /// </summary>
    /// <param name="pat"></param>
    /// <returns></returns>
    public virtual Vector2Int[] GetOddENtiles(Vector3Int[] pat)
    {
        Vector2Int[] w = new Vector2Int[pat.Length];
        for (int i = 0; i < pat.Length; i++)
        {
            Vector3Int j = new(-pat[i].z, -pat[i].x, -pat[i].y);
            w[i] = HexToOrthoCoords.GetOddOrthoCoord(j);
        }
        return w;
    }
    /// <summary>
    /// génère l'array de coord Unity des Kapas vers le Nord Est, tuiles Paires
    /// </summary>
    /// <param name="pat"></param>
    /// <returns></returns>
    public virtual Vector2Int[] GetEvenENtiles(Vector3Int[] pat)
    {
        Vector2Int[] w = new Vector2Int[pat.Length];
        for (int i = 0; i < pat.Length; i++)
        {
            Vector3Int j = new(-pat[i].z, -pat[i].x, -pat[i].y);
            w[i] = HexToOrthoCoords.GetEvenOrthoCoord(j);
        }
        return w;
    }
    #endregion
    #region WNtiles
    /// <summary>
    /// génère l'array de coord Unity des Kapas vers le Nord Ouest, tuiles Impaires
    /// </summary>
    /// <param name="pat"></param>
    /// <returns></returns>
    public virtual Vector2Int[] GetOddWNtiles(Vector3Int[] pat)
    {
        Vector2Int[] w = new Vector2Int[pat.Length];
        for (int i = 0; i < pat.Length; i++)
        {
            Vector3Int j = new(-pat[i].y, -pat[i].z, -pat[i].x);
            w[i] = HexToOrthoCoords.GetOddOrthoCoord(j);
        }
        return w;
    }
    /// <summary>
    /// génère l'array de coord Unity des Kapas vers le Nord Ouest, tuiles Paires
    /// </summary>
    /// <param name="pat"></param>
    /// <returns></returns>
    public virtual Vector2Int[] GetEvenWNtiles(Vector3Int[] pat)
    {
        Vector2Int[] w = new Vector2Int[pat.Length];
        for (int i = 0; i < pat.Length; i++)
        {
            Vector3Int j = new(-pat[i].y, -pat[i].z, -pat[i].x);
            w[i] = HexToOrthoCoords.GetEvenOrthoCoord(j);
        }
        return w;
    }
    #endregion
    #region Stiles
    /// <summary>
    /// génère l'array de coord Unity des Kapas vers le Sud, tuiles Impaires
    /// </summary>
    /// <param name="pat"></param>
    /// <returns></returns>
    public virtual Vector2Int[] GetOddStiles(Vector3Int[] pat)
    {
        Vector2Int[] w = new Vector2Int[pat.Length];
        for (int i = 0; i < pat.Length; i++)
        {
            Vector3Int j = new(-pat[i].x, -pat[i].y, -pat[i].z);
            w[i] = HexToOrthoCoords.GetOddOrthoCoord(j);
        }
        return w;
    }
    /// <summary>
    /// génère l'array de coord Unity des Kapas vers le Sud, tuiles Paires
    /// </summary>
    /// <param name="pat"></param>
    /// <returns></returns>
    public virtual Vector2Int[] GetEvenStiles(Vector3Int[] pat)
    {
        Vector2Int[] w = new Vector2Int[pat.Length];
        for (int i = 0; i < pat.Length; i++)
        {
            Vector3Int j = new(-pat[i].x, -pat[i].y, -pat[i].z);
            w[i] = HexToOrthoCoords.GetEvenOrthoCoord(j);
        }
        return w;
    }
    #endregion
    #region EStiles
    /// <summary>
    /// génère l'array de coord Unity des Kapas vers le Sud Est, tuiles Impaires
    /// </summary>
    /// <param name="pat"></param>
    /// <returns></returns>
    public virtual Vector2Int[] GetOddEStiles(Vector3Int[] pat)
    {
        Vector2Int[] w = new Vector2Int[pat.Length];
        for (int i = 0; i < pat.Length; i++)
        {
            Vector3Int j = new(pat[i].y, pat[i].z, pat[i].x);
            w[i] = HexToOrthoCoords.GetOddOrthoCoord(j);
        }
        return w;
    }
    /// <summary>
    /// génère l'array de coord Unity des Kapas vers le Sud Est, tuiles Paires
    /// </summary>
    /// <param name="pat"></param>
    /// <returns></returns>
    public virtual Vector2Int[] GetEvenEStiles(Vector3Int[] pat)
    {
        Vector2Int[] w = new Vector2Int[pat.Length];
        for (int i = 0; i < pat.Length; i++)
        {
            Vector3Int j = new(pat[i].y, pat[i].z, pat[i].x);
            w[i] = HexToOrthoCoords.GetEvenOrthoCoord(j);
        }
        return w;
    }
    #endregion
    #region WStiles
    /// <summary>
    /// génère l'array de coord Unity des Kapas vers le Sud Ouest, tuiles Impaires
    /// </summary>
    /// <param name="pat"></param>
    /// <returns></returns>
    public virtual Vector2Int[] GetOddWStiles(Vector3Int[] pat)
    {
        Vector2Int[] w = new Vector2Int[pat.Length];
        for (int i = 0; i < pat.Length; i++)
        {
            Vector3Int j = new(pat[i].z, pat[i].x, pat[i].y);
            w[i] = HexToOrthoCoords.GetOddOrthoCoord(j);
        }
        return w;
    }
    /// <summary>
    /// génère l'array de coord Unity des Kapas vers le Sud Ouest, tuiles Paires
    /// </summary>
    /// <param name="pat"></param>
    /// <returns></returns>
    public virtual Vector2Int[] GetEvenWStiles(Vector3Int[] pat)
    {
        Vector2Int[] w = new Vector2Int[pat.Length];
        for (int i = 0; i < pat.Length; i++)
        {
            Vector3Int j = new(pat[i].z, pat[i].x, pat[i].y);
            w[i] = HexToOrthoCoords.GetEvenOrthoCoord(j);
        }
        return w;
    }
    #endregion
    #endregion

    #region methodes to herit
    /// <summary>
    /// initialise tous les paternes de Kapas dans toutes les directions
    /// </summary>
    /// <param name="p"></param>
    public virtual void Init(Vector3Int[] p)
    {
        //NTiles
        OddNTiles = GetOddNtiles(p);
        EvenNTiles = GetEvenNtiles(p);
        //WNTiles
        OddWNTiles = GetOddWNtiles(p);
        EvenWNTiles = GetEvenWNtiles(p);
        //ENTiles
        OddENTiles = GetOddENtiles(p);
        EvenENTiles = GetEvenENtiles(p);
        //STiles
        OddSTiles = GetOddStiles(p);
        EvenSTiles = GetEvenStiles(p);
        //ESTiles
        OddESTiles = GetOddEStiles(p);
        EvenESTiles = GetEvenEStiles(p);
        //WSTiles
        OddWSTiles = GetOddWStiles(p);
        EvenWSTiles = GetEvenWStiles(p);
    }
    /// <summary>
    /// Sélectionne les Tuiles utilisées par la compétence, dans une direction donnée
    /// </summary>
    public virtual void SelectTiles() => Debug.Log("KapaGraphSelect");
    /// <summary>
    /// Base Logique de l'execution de Kapa
    /// </summary>
    public abstract void Execute();
    /// <summary>
    /// Retire la sélection de Tuiles utilisées par la compétence
    /// </summary>
    public virtual void DeselectTiles() => Debug.Log("KapaGraphDeselect");
    #endregion
}