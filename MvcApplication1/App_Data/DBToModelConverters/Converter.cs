using System.Linq;
using MvcApplication1.DataBaseStructures;
using MvcApplication1.Models;

namespace MvcApplication1.App_Data.DBToModelConverters
{
    public class Converter
    {
        public static BoardShow Convert(Board b, bool isPublic, bool isEditable)
        {
            return new BoardShow
            {
                IsEditable = isEditable,
                List = b.Lists.Select(list => Convert(list,isEditable)),
                Name = b.Name,
                Description = b.Description,
                IsPublic = isPublic,
                Id = b.BoardId
            };
        }

        public static ListShow Convert(BoardList bl, bool isEditable)
        {
            return new ListShow
            {
                IsEditable = isEditable,
                Id = bl.Id,
                Name = bl.Name,
                Cards = bl.Cards.Select(card => Convert(card,isEditable))
            };
        }

        public static CardShow Convert(BoardCard bc, bool isEditable)
        {
            return new CardShow
            {
                IsEditable = isEditable,
                Id = bc.Id,
                Description = bc.Description
            };
        }
    }
}