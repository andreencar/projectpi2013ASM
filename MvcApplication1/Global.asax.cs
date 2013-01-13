
using System.Web.Mvc;
using System.Web.Routing;
using MvcApplication1.App_Data.DataBaseStructures;
using MvcApplication1.DataBaseStructures;
using System.Web.Security;

namespace MvcApplication1
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {

        public static BoardDb Repo;

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            Repo = new BoardDb();
            InitDB(Repo);

            AreaRegistration.RegisterAllAreas();

            // Use LocalDB for Entity Framework by default

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        private static void InitDB(BoardDb database)
        {/*
            foreach (MembershipUser user in Membership.GetAllUsers()) {
                Membership.DeleteUser(user.UserName,true);
            }*/

            const string u1 = "someuser";

            const string b1C1 = "this is a sample card 1";
            const string b1C2 = "this is a sample card 2";

            const string b1 = "boardName";
            const string b2 = "b2";

            const string b1L1 = "board 1 list 1";
            const string b1L2 = "board 1 list 2";
            const string b1L3 = "board 1 list 3";
            const string b2L1 = "board 2 list 1";

            database.AddBoard(u1,b1,"board 1",true);
            database.AddBoard(u1,b2,"board 2",false);

            int b1Id = database.GetBoardId(u1, b1);
            int b2Id = database.GetBoardId(u1, b2);

            int b1L1Id = database.AddListToBoard(b1Id, b1L1);
            database.AddListToBoard(b1Id, b1L2);
            database.AddListToBoard(b1Id, b1L3);

            database.AddListToBoard(b2Id, b2L1);

            database.AddCardToBoardList(b1L1Id, b1C1);
            database.AddCardToBoardList(b1L1Id, b1C2);
        }
    }
}