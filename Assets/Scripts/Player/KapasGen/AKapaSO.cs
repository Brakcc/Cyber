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

    #region patern gen to herit
    public virtual Vector2Int[] GetOddNtiles()
    {
        Vector2Int[] w = new Vector2Int[Patern.Length];
        for (int i = 0; i < Patern.Length; i++)
        {
            Vector3Int j = new(Patern[i].x, Patern[i].y, Patern[i].z);
            w[i] = HexToOrthoCoords.GetOddOrtoCoord(j);
        }
        return w;
    }
    #endregion

    #region methodes to herit
    public virtual void SelectTiles() => Debug.Log("KapaGraphSelect");
    public abstract void Execute();
    public virtual void DeselectTiles() => Debug.Log("KapaGraphDeselect");
    #endregion
}