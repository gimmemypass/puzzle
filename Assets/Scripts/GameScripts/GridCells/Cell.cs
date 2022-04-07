namespace GameScripts.GridCells
{
    public abstract class Cell
    {
        public abstract void Accept(ICellVisitor visitor);
        public new abstract string ToString();
    }
}