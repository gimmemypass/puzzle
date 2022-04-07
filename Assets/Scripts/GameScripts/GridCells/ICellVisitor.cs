namespace GameScripts.GridCells
{
    public interface ICellVisitor
    {
        void Visit(Wall wall);
        void Visit(EmptyCell cell);
        void Visit(Tile tile);
        void Visit(FillTile fillTile);
    }
}