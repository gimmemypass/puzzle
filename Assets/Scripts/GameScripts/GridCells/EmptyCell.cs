namespace GameScripts.GridCells
{
    public class EmptyCell : Cell
    {
        public override void Accept(ICellVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return "e";
        }
    }
}