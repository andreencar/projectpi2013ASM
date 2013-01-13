using System.Collections.Generic;
using System.Linq;
using MvcApplication1.DataBaseStructures;

namespace MvcApplication1.App_Data.DataBaseStructures
{
    public class BoardDb
    {
        private int _listGen = 0;
        private int _cardGen = 0;
        private int _idGen = 0;
        private readonly Dictionary<int, Registery> _regist;
       

        public BoardDb(){
            _regist = new Dictionary<int, Registery>();
        }

        public void AddBoard(string username, string boardname, string description, bool isPublic)
        {
            var id = _idGen++;
            var b = new Board(boardname, description, id);
            var registery = new Registery(b, username, isPublic);
            _regist.Add(id, registery);
        }

        public Board GetBoard(string username, string boardname)
        {
            return GetRegistery(username,boardname).Board;
        }

        private Registery GetRegistery(string username, string boardname)
        {
            return _regist.Values.FirstOrDefault(reg => reg.Board.Name.Equals(boardname) && reg.Author.Equals(username));
        }

        public Board GetBoard(int id)
        {
            return _regist[id].Board;
        }

        public void AddUserToBoard(int id, string username, bool canEdit)
        {
            _regist[id].Users.Add(new UserProperty(username,canEdit));
        }

        public int GetBoardId(string username, string boardname)
        {
            var reg = GetRegistery(username, boardname);
            return reg.Board.BoardId;
        }

        public IEnumerable<Board> GetPublicBoards()
        {
            return _regist.Values.Where(regist => regist.IsPublic).Select(reg=>reg.Board);
        }

        public IEnumerable<Board> GetBoardsLikeString(string same)
        {
            return _regist.Values.Where(regist => regist.IsPublic).Where(regist => regist.Board.Name.Contains(same)).Select(reg => reg.Board);
        } 

        public IEnumerable<Board> GetBoardsFromUser(string username)
        {
            return _regist.Values.Where(reg => reg.Author.Equals(username)).Select(reg => reg.Board);
        }

        public IEnumerable<Board> GetBoardsUserCanContribute(string username)
        {
            return _regist.Values.Where(reg => reg.Users.FirstOrDefault(user => (user.UserName.Equals(username) && user.CanEdit)) != null).Select(reg => reg.Board);
        }

        public bool IsPublic(int id)
        {
            return _regist[id].IsPublic;
        }

        public void RemoveBoard(int id)
        {
            _regist.Remove(id);
        }

        public BoardList GetList(int id)
        {
            var board = GetBoardThatContainsList(id);
            return GetList(board, id);
        }

        private BoardList GetList(Board b, int id)
        {
            return b.Lists.Get(id);
        }

        public int AddListToBoard(int id, string listName)
        {
            int listId = _listGen++;
            GetBoard(id).AddList(listId,listName);
            return listId;
        }

        public Dictionary<int,IndexedList<BoardCard>> GetListFromBoard(Board b)
        {
            return b.Lists.ToDictionary(blist => blist.Id, blist => blist.Cards);
        }

        public BoardCard GetEntryFromBoard(Board board, int id)
        {
            BoardCard outValue;
            board.Cards.TryGetValue(id, out outValue);

            return outValue;
        }

        public IndexedList<BoardCard> GetListFromBoard(Board board, int listId)
        {
            return GetListFromBoard(board)[listId];
        }

        public BoardList GetBListFromBoard(Board board, string listname)
        {
            return board.Lists.FirstOrDefault(list => list.Name.Equals(listname));
        }

        public BoardCard GetEntryFromBoardListName(int listId, int pos)
        {
            var board = GetBoardThatContainsList(listId);
            return GetListFromBoard(board, listId).Get(pos);
        }


        private IEnumerable<Registery> GetRegistBoardsThatUserCanView(string username)
        {
            return _regist.Values.
                Where(regist => regist.Users.FirstOrDefault(user => user.UserName.Equals(username)) != null);
        } 

        private IEnumerable<Registery> GetBoardRegistsThatUserCanEdit(string username)
        {
            return _regist.Values.
                Where(regist => regist.Users.FirstOrDefault(user => user.UserName.Equals(username) && user.CanEdit) != null);
        }

        //returns if the user can see this board
        public bool CanSeeBoard(int boardId, string username)
        {
            return GetRegistBoardsThatUserCanView(username).FirstOrDefault(regist => regist.Board.BoardId == boardId) != null;
        }

        //returns all the boards that this user can edit
        public bool CanContributeToBoard(int boardId, string username)
        {
            return GetBoardRegistsThatUserCanEdit(username).FirstOrDefault(regist => regist.Board.BoardId == boardId) != null;
        }

        public IEnumerable<Board> GetBoardsThatUserCanEdit(string username)
        {
            return GetBoardRegistsThatUserCanEdit(username).Select(b => b.Board);
        }

        public IEnumerable<Board> GetBoardsThatUserCanView(string username)
        {
            return GetRegistBoardsThatUserCanView(username).Select(b => b.Board);
        }

        /*
         * Always adds to the tail
         */
        public int AddCardToBoardList(int listId, string cardDescription)
        {
            int cardId = _cardGen++;
            Board board = GetBoardThatContainsList(listId);
            BoardCard card = new BoardCard(cardId, cardDescription);

            board.Cards.Add(card.Id, card);
            GetListFromBoard(board, listId).AddToTail(card);

            return cardId;
        }

        public void RemoveCardFromBoard(int cardId)
        {
            var b = GetBoardThatContainsCard(cardId);
            RemoveCardFromBoard(b, cardId, GetIdOfListWhereCardIs(b, cardId));
        }

        public void RemoveCardFromBoard(Board board, int cardId, int listId)
        {
            var list = GetListFromBoard(board,listId);
            var card = GetEntryFromBoard(board, cardId);
            
            list.Remove(list.GetIndexFor(card));
            board.Archived.Add(card.Id,card);
        }

        public void SwapListsFromBoard(int listId1, int listId2)
        {
            Board board = GetBoardThatContainsList(listId1);
            int pos1 = board.Lists.GetIndexFor(new BoardList(listId1,""));
            int pos2 = board.Lists.GetIndexFor(new BoardList(listId2,""));

            board.Lists.Swap(pos1,pos2);
        }

        public Board GetBoardThatContainsList(int listId)
        {
            return _regist.Values.Select(reg => reg.Board).FirstOrDefault(board => board.Lists.FirstOrDefault(l => l.Id == listId) != null);
        }

        public Board GetBoardThatContainsCard(int cardId)
        {
            return _regist.Values.Select(reg => reg.Board).FirstOrDefault(board => board.Cards.ContainsKey(cardId));
        }

        public IEnumerable<Board> GetBoards()
        {
            return _regist.Values.Select(reg => reg.Board);
        } 

        //AUXILIARY METHOD
        public int GetIdOfListWhereCardIs(Board b, int cardId)
        {
            var lists = GetListFromBoard(b);

            return lists.FirstOrDefault(pair => pair.Value.Select(elements => elements.Id).Contains(cardId)).Key;
        }
    }
}
