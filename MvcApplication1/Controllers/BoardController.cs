using System;
using System.Web.Mvc;
using MvcApplication1.App_Data.DBToModelConverters;
using MvcApplication1.DataBaseStructures;
using MvcApplication1.Models;
using System.Linq;
using System.Web.Security;

namespace MvcApplication1.Controllers
{
    public class BoardController : Controller
    {
        //
        // GET: /Board/
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(BoardCreate toAdd)
        {
            var user = Membership.GetUser();
            if(user == null)
            {
                Response.StatusCode = 401;
                return Redirect(null);
            }

            if(!string.IsNullOrEmpty(toAdd.Name))
            {
                int boardId;
                try{
                    var repo =   MvcApplication.Repo;
                    repo.AddBoard(Membership.GetUser().UserName, toAdd.Name, toAdd.Description, toAdd.IsPublic);
                    boardId = repo.GetBoardId(Membership.GetUser().UserName, toAdd.Name);
                } catch (ArgumentException e)
                {
                    Response.StatusCode = 400;
                    return View(toAdd);
                }

                return RedirectToAction("Show", "Board", new { id = boardId });
            }
            return RedirectToAction("Index", "Error", new { code = 400, description = "Need to insert Board Name" });
        }

        [HttpGet]
        public ActionResult Show(int id)
        {
      
            var repo = MvcApplication.Repo;
            var board = repo.GetBoard(id);

            if(board != null)
            {
                //Board exists

                var user = Membership.GetUser();
                bool canEdit = false;
                if(!repo.IsPublic(id))
                {
                    if(user == null)
                    {
                        return HttpNotFound();
                    }
                    if(!repo.CanSeeBoard(id,user.UserName))
                    {
                        return HttpNotFound();
                    }
                    canEdit = repo.CanContributeToBoard(id, user.UserName);
                    
                }
                var boardmodel = Converter.Convert(board, repo.IsPublic(id), canEdit);
                return View(boardmodel);
            }
            return HttpNotFound();
        }
    }
}


