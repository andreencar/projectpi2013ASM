using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Index(int Code, string Description)
        {
            ErrorModel error;
            try{
                error = new ErrorModel(Code, Description);
            }
            catch(Exception e){
                error = new ErrorModel(500, "UhOh... Something Wrong Happened... Are you trying to break our Webserver?");
            }
            Response.StatusCode = error.Code;
            return View(error);            
        }

    }
}
