using System;
using System.Web.Mvc;
using System.Web.Security;
using MvcApplication1.App_Data.DBToModelConverters;
using MvcApplication1.DataBaseStructures;
using MvcApplication1.Models;
using System.Linq;
using MvcApplication1.Models.List;

namespace MvcApplication1.Controllers
{
    public class ListController : Controller
    {
        //
        // GET: /Board/

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var repo = MvcApplication.Repo;
            var board = repo.GetBoardThatContainsList(id);
            var list = repo.GetList(id);

            var listEditModel = new ListEdit
            {
                Name = list.Name,
                MovingOptions = board.Lists.Select(l => new ListShow()
                {
                    Name = l.Name,
                    Id = list.Id
                })

            };
            return View(listEditModel);
        }

        [HttpGet]
        public ActionResult Show(int id)
        {
            var repo = MvcApplication.Repo;
            var list = repo.GetList(id);
            if (list == null)
            {
                return HttpNotFound();
            }

            var board = repo.GetBoardThatContainsList(id);
            var boardId = board.BoardId;

            var user = Membership.GetUser();
            var name = user.UserName;

            var listmodel = Converter.Convert(list,repo.CanContributeToBoard(boardId,name));

            return View(listmodel);
        }
    }
}


