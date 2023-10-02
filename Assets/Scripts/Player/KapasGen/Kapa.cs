public abstract class Kapa : IKapa
{
    public abstract void SelectTiles();

    public abstract void Execute(ScriptableDataKapa dataKapa);

    public abstract void DeselectTiles();
}
