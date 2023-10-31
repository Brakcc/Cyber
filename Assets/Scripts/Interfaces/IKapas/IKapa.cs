public interface IKapa
{
    //public void SelectTiles(Unit unit, HexGridStore hexGrid);
    public bool Execute(Unit unit);
    public void DeselectTiles(HexGridStore hexGrid);
}