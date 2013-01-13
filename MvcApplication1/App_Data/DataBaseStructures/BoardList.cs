namespace MvcApplication1.DataBaseStructures
{
    public class BoardList
    {
        public int Id { private set; get; }
        public string Name;
        public IndexedList<BoardCard> Cards; 

        public BoardList(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override bool Equals(object bl)
        {
            if (bl == null) return false;
            var b = bl as BoardList;
            if (b == null) return false;
            return Name.Equals(b.Name);
        }
    }
}
