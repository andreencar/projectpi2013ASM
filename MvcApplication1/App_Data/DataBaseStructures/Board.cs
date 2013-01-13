using System.Collections.Generic;

namespace MvcApplication1.DataBaseStructures
{
 
    public class Board
    {
        private int _currId;
        public int BoardId { private set; get; }
        public string Description;
        public string Name { private set; get; }

        public IndexedList<BoardList> Lists;
        public Dictionary<int, BoardCard> Cards;
        public Dictionary<int, BoardCard> Archived;

        public Board(string name, string desc, int id)
        {
            BoardId = id;
            Lists = new IndexedList<BoardList>();
            Cards = new Dictionary<int, BoardCard>();
            Archived = new Dictionary<int, BoardCard>();
            Description = desc;
            Name = name;
        }
        public void AddList(int id, string listName)
        {
            BoardList toAdd = new BoardList(id, listName);
            toAdd.Cards = new IndexedList<BoardCard>();
            Lists.AddToTail(toAdd);
        }
    }
}
