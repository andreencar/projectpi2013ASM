using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //TODO de onde vem o user name?
          //var userName = GetUserName();
            var userName = "someuser";
            if (userName == null)
            {
                Response.StatusCode = 401;
                return Redirect(/*uri de login*/null);
            }

            var repo = MvcApplication.Repo;
            Home home = new Home();
            home.Boards = repo.GetBoards().Select(b => new HomeBoardModel(){Name = b.Name, Id = b.BoardId});

            return View(home);
        }

    }
}
