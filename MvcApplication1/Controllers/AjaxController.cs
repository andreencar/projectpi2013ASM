﻿using System;
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

        public ActionResult AjaxTest(string sbinput)
        {
            var repo = MvcApplication.Repo;
            var boards = repo.GetBoardsLikeString(sbinput);
            List<Suggestion> suggestionList = new List<Suggestion>();
            Suggestion toAdd = new Suggestion();
            foreach (Board aux in boards)
            {
                toAdd.name = aux.Name;
                toAdd.link = Url.Action("Show", "Board", null, "http") + aux.BoardId;
                suggestionList.Add(toAdd);
            }
            return Json(suggestionList.ToArray(), JsonRequestBehavior.AllowGet);
        }

    }

    public class Suggestion {
        public String name;
        public String link;

    }
}