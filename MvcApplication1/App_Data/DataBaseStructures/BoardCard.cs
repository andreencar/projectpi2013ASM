namespace MvcApplication1.DataBaseStructures
{
    public class BoardCard
    {
        public int Id { get; private set;  }
        public string Description { get;  set;  }

        public BoardCard(int id, string desc)
        {
            Description = desc;
            Id = id;
        }

        public new bool Equals(object bc)
        {
            if (bc == null) return false;
            var b = bc as BoardCard;
            if (b == null) return false;
            return Id == b.Id;
        }
    }
}
