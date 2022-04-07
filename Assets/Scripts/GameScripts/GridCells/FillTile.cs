namespace GameScripts.GridCells
{
    public class FillTile : Cell
    {
        public override void Accept(ICellVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return "F";
        }
    }
}