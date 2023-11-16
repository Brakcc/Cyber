using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public abstract class AKapaSO : ScriptableObject, IKapa, IKapasDatas
{
    #region fields to herit
    public abstract string KapaName { get; }
    public abstract int ID { get; }
    public abstract int Cost { get; }
    public abstract string Description { get; }
    public abstract int MaxPlayerPierce { get; }
    public abstract EffectType EffectType { get; }
    public abstract KapaType KapaType { get; }
    public abstract KapaFunctionType KapaFunctionType { get; }
    public abstract KapaUISO KapaUI { get; }
    public abstract Vector3Int[] Patern { get; }
    #endregion

    #region paterns to herit
    //North tiles
    public abstract Vector3Int[] OddNTiles { get; set; }
    public abstract Vector3Int[] EvenNTiles { get; set; }
    //EN tiles
    public abstract Vector3Int[] OddENTiles { get; set; }
    public abstract Vector3Int[] EvenENTiles { get; set; }
    //ES tiles
    public abstract Vector3Int[] OddESTiles { get; set; }
    public abstract Vector3Int[] EvenESTiles { get; set; }
    //S tiles
    public abstract Vector3Int[] OddSTiles { get; set; }
    public abstract Vector3Int[] EvenSTiles { get; set; }
    //WS tiles
    public abstract Vector3Int[] OddWSTiles { get; set; }
    public abstract Vector3Int[] EvenWSTiles { get; set; }
    //WN tiles
    public abstract Vector3Int[] OddWNTiles { get; set; }
    public abstract Vector3Int[] EvenWNTiles { get; set; }
    #endregion

    #region patern gen cache
    #region Ntiles
    /// <summary>
    /// génère l'array de coord Unity des Kapas vers le Nord, tuiles Impaires
    /// </summary>
    /// <param name="pat"></param>
    /// <returns></returns>
    async Task<Vector3Int[]> GetOddNtiles(Vector3Int[] pat)
    {
        Vector3Int[] w = new Vector3Int[pat.Length];
        for (int i = 0; i < pat.Length; i++)
        {
            Vector3Int j = new(pat[i].x, pat[i].y, pat[i].z);
            w[i] = await HexToOrthoCoords.GetOddOrthoCoord(j);
        }
        return w;
    }
    /// <summary>
    /// génère l'array de coord Unity des Kapas vers le Nord, tuiles Paires
    /// </summary>
    /// <param name="pat"></param>
    /// <returns></returns>
    async Task<Vector3Int[]> GetEvenNtiles(Vector3Int[] pat)
    {
        Vector3Int[] w = new Vector3Int[pat.Length];
        for (int i = 0; i < pat.Length; i++)
        {
            Vector3Int j = new(pat[i].x, pat[i].y, pat[i].z);
            w[i] = await HexToOrthoCoords.GetEvenOrthoCoord(j);
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
    async Task<Vector3Int[]> GetOddENtiles(Vector3Int[] pat)
    {
        Vector3Int[] w = new Vector3Int[pat.Length];
        for (int i = 0; i < pat.Length; i++)
        {
            Vector3Int j = new(-pat[i].z, -pat[i].x, -pat[i].y);
            w[i] = await HexToOrthoCoords.GetOddOrthoCoord(j);
        }
        return w;
    }
    /// <summary>
    /// génère l'array de coord Unity des Kapas vers le Nord Est, tuiles Paires
    /// </summary>
    /// <param name="pat"></param>
    /// <returns></returns>
    async Task<Vector3Int[]> GetEvenENtiles(Vector3Int[] pat)
    {
        Vector3Int[] w = new Vector3Int[pat.Length];
        for (int i = 0; i < pat.Length; i++)
        {
            Vector3Int j = new(-pat[i].z, -pat[i].x, -pat[i].y);
            w[i] = await HexToOrthoCoords.GetEvenOrthoCoord(j);
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
    async Task<Vector3Int[]> GetOddWNtiles(Vector3Int[] pat)
    {
        Vector3Int[] w = new Vector3Int[pat.Length];
        for (int i = 0; i < pat.Length; i++)
        {
            Vector3Int j = new(-pat[i].y, -pat[i].z, -pat[i].x);
            w[i] = await HexToOrthoCoords.GetOddOrthoCoord(j);
        }
        return w;
    }
    /// <summary>
    /// génère l'array de coord Unity des Kapas vers le Nord Ouest, tuiles Paires
    /// </summary>
    /// <param name="pat"></param>
    /// <returns></returns>
    async Task<Vector3Int[]> GetEvenWNtiles(Vector3Int[] pat)
    {
        Vector3Int[] w = new Vector3Int[pat.Length];
        for (int i = 0; i < pat.Length; i++)
        {
            Vector3Int j = new(-pat[i].y, -pat[i].z, -pat[i].x);
            w[i] = await HexToOrthoCoords.GetEvenOrthoCoord(j);
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
    async Task<Vector3Int[]> GetOddStiles(Vector3Int[] pat)
    {
        Vector3Int[] w = new Vector3Int[pat.Length];
        for (int i = 0; i < pat.Length; i++)
        {
            Vector3Int j = new(-pat[i].x, -pat[i].y, -pat[i].z);
            w[i] = await HexToOrthoCoords.GetOddOrthoCoord(j);
        }
        return w;
    }
    /// <summary>
    /// génère l'array de coord Unity des Kapas vers le Sud, tuiles Paires
    /// </summary>
    /// <param name="pat"></param>
    /// <returns></returns>
    async Task<Vector3Int[]> GetEvenStiles(Vector3Int[] pat)
    {
        Vector3Int[] w = new Vector3Int[pat.Length];
        for (int i = 0; i < pat.Length; i++)
        {
            Vector3Int j = new(-pat[i].x, -pat[i].y, -pat[i].z);
            w[i] = await HexToOrthoCoords.GetEvenOrthoCoord(j);
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
    async Task<Vector3Int[]> GetOddEStiles(Vector3Int[] pat)
    {
        Vector3Int[] w = new Vector3Int[pat.Length];
        for (int i = 0; i < pat.Length; i++)
        {
            Vector3Int j = new(pat[i].y, pat[i].z, pat[i].x);
            w[i] = await HexToOrthoCoords.GetOddOrthoCoord(j);
        }
        return w;
    }
    /// <summary>
    /// génère l'array de coord Unity des Kapas vers le Sud Est, tuiles Paires
    /// </summary>
    /// <param name="pat"></param>
    /// <returns></returns>
    async Task<Vector3Int[]> GetEvenEStiles(Vector3Int[] pat)
    {
        Vector3Int[] w = new Vector3Int[pat.Length];
        for (int i = 0; i < pat.Length; i++)
        {
            Vector3Int j = new(pat[i].y, pat[i].z, pat[i].x);
            w[i] = await HexToOrthoCoords.GetEvenOrthoCoord(j);
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
    async Task<Vector3Int[]> GetOddWStiles(Vector3Int[] pat)
    {
        Vector3Int[] w = new Vector3Int[pat.Length];
        for (int i = 0; i < pat.Length; i++)
        {
            Vector3Int j = new(pat[i].z, pat[i].x, pat[i].y);
            w[i] = await HexToOrthoCoords.GetOddOrthoCoord(j);
        }
        return w;
    }
    /// <summary>
    /// génère l'array de coord Unity des Kapas vers le Sud Ouest, tuiles Paires
    /// </summary>
    /// <param name="pat"></param>
    /// <returns></returns>
    async Task<Vector3Int[]> GetEvenWStiles(Vector3Int[] pat)
    {
        Vector3Int[] w = new Vector3Int[pat.Length];
        for (int i = 0; i < pat.Length; i++)
        {
            Vector3Int j = new(pat[i].z, pat[i].x, pat[i].y);
            w[i] = await HexToOrthoCoords.GetEvenOrthoCoord(j);
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
    public virtual async void InitPaterns(Vector3Int[] p)
    {
        //NTiles
        OddNTiles = await GetOddNtiles(p);
        EvenNTiles = await GetEvenNtiles(p);
        //WNTiles
        OddWNTiles = await GetOddWNtiles(p);
        EvenWNTiles = await GetEvenWNtiles(p);
        //ENTiles
        OddENTiles = await GetOddENtiles(p);
        EvenENTiles = await GetEvenENtiles(p);
        //STiles
        OddSTiles = await GetOddStiles(p);
        EvenSTiles = await GetEvenStiles(p);
        //ESTiles
        OddESTiles = await GetOddEStiles(p);
        EvenESTiles = await GetEvenEStiles(p);
        //WSTiles
        OddWSTiles = await GetOddWStiles(p);
        EvenWSTiles = await GetEvenWStiles(p);
    }

    public abstract bool OnCheckKapaPoints(Unit unit);

    /// <summary>
    /// Base Logique de l'execution de Kapa
    /// </summary>
    public abstract void OnExecute(Unit unit);

    /// <summary>
    /// Permet d'override les boutons necessaires selon les Kapa, du gerne retrun null sur le skip pour ne pas afficher de bouton
    /// </summary>
    /// <param name="hexGrid"></param>
    /// <param name="unit"></param>
    /// <returns></returns>
    public virtual List<Vector3Int> OnGenerateButton(HexGridStore hexGrid, Unit unit) 
    {
        var availableButton = new List<Vector3Int>();
        foreach (var i in hexGrid.GetNeighbourgs(unit.CurrentHexPos))
        {
            if (hexGrid.GetTile(i).IsObstacle()) continue;
            availableButton.Add(i);
        }
        return availableButton;
    }
    #endregion

    #region graph selection methodes (to herit)
    /// <summary>
    /// Sélectionne les Tuiles utilisées par la compétence, dans une direction donnée
    /// </summary>
    public virtual List<Vector3Int> OnSelectGraphTiles(Unit unit, HexGridStore hexGrid, Vector3Int[] tilesArray)
    {
        KapaSystem kapaSys = new();
        List<Vector3Int> v = new();
        foreach (var i in tilesArray)
        {
            if (hexGrid.hexTiles.ContainsKey(HexCoordonnees.GetClosestHex(unit.transform.position) + i))
            {
                Hex temp = hexGrid.GetTile(HexCoordonnees.GetClosestHex(unit.transform.position) + i);
                if (kapaSys.VerifyKapaRange(temp.HexCoords, unit, hexGrid, MaxPlayerPierce))
                {
                    temp.EnableGlowKapa();
                    v.Add(temp.HexCoords);
                }
            }
        }
        return v;
    }

    /// <summary>
    /// Retire la sélection de Tuiles utilisées par la compétence
    /// </summary>
    public virtual void OnDeselectTiles(HexGridStore hexGrid)
    {
        foreach (var i in hexGrid.hexTiles.Values)
        {
            i.DisableGlowKapa();
        }
    }
    #endregion
}