using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.DataBaseStructures;

namespace MvcApplication1.Controllers
{
    public class AjaxController : Controller
    {
        //
        // GET: /Ajax/

        public ActionResult Index()
        {
            return View();
        }

        public bool BoardExists(string newboardName)
        {
            var repo = MvcApplication.Repo;
            var res = repo.GetBoards().FirstOrDefault(b => b.Name == newboardName);
            return res == null;
        }

        public ActionResult SearchHelper(string sbinput)
        {
            var repo = MvcApplication.Repo;
            var boards = repo.GetBoardsLikeString(sbinput);
            List<Suggestion> suggestionList = new List<Suggestion>();
            Suggestion toAdd = new Suggestion();
            foreach (Board aux in boards)
            {
                toAdd.name = aux.Name;
                string link = Url.Action("Show", "Board", null, "http") + "/" + aux.BoardId;

                string[] parts = link.Split(':');
                if (parts[1] != "//localhost")
                {
                    string[] parts2 = parts[2].Split('/');

                    string urlfinal = parts[0] + ':' + parts[1];
                    for (int i = 1; i < parts2.Length; i++)
                    {
                        urlfinal += '/' + parts2[i];
                    }
                    toAdd.link = urlfinal;
                }
                toAdd.link = link;
                suggestionList.Add(toAdd);
            }
            return Json(suggestionList.ToArray(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ChangeDescription(string description, int cardId) {
            var repo = MvcApplication.Repo;
            BoardCard toEdit = repo.GetEntryFromBoard(repo.GetBoardThatContainsCard(cardId), cardId);
            toEdit.Description = description;
            return Json(toEdit, JsonRequestBehavior.AllowGet);
        }

    }

    public class Suggestion {
        public String name;
        public String link;

    }
}
