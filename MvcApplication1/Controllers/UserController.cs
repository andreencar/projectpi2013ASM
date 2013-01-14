using System.Web.Mvc;
using MvcApplication1.Models;
using System.Web.Security;
using System.Net.Mail;
using System;
using MvcApplication1.Models.User;
using System.Web;
using System.IO;

namespace MvcApplication1.Controllers
{
    public class UserController : Controller
    {
        private const string DEFAULT_PICTURE = "http://upload.wikimedia.org/wikipedia/en/thumb/5/5c/Spongebob-squarepants.png/200px-Spongebob-squarepants.png";

        //
        // GET: /User/
        [Authorize]
        [HttpGet]
        public ActionResult Profile(string userID)
        {
            MembershipUser user;
            if (string.IsNullOrEmpty(userID))
                user = Membership.GetUser();
            else
                user = Membership.GetUser(userID);

            if (user == null) {
                Response.StatusCode = 400;
                return View("Error", new ErrorModel (400, "User Not Found, Try Again!"));
            }

            string[] info = user.Comment.Split('#');
            bool hasPhoto = false;
            string extension = "";
            string username =  Membership.GetUser().UserName;

            DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/App_Data/"));
            FileInfo[] rgFiles = di.GetFiles();
            foreach (var imageFile in rgFiles) {
                if (imageFile.Name.Split('.')[0] == username)
                {
                    extension = imageFile.Extension;
                    hasPhoto = true;
                    break;
                }
            }

            var userModel = new UserModel
            {
                Photo = Url.Content("~/App_Data/") + username + extension,
                IsPhoto = hasPhoto,
                Comment = info[2],
                UserName = user.UserName,
                Name = info[1],
                Email = user.Email
            };
            return View(userModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit()
        {
            var user = Membership.GetUser();

            string[] info = user.Comment.Split('#');
            var userEditModel = new UserEdit
                                    {
                                        Name = user.UserName,
                                        UserName = user.UserName,
                                        Email = user.Email,
                                        Comment = info[2]
                                    };
            return View(userEditModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(UserEdit model)
        {
            var user = Membership.GetUser();

            //check if the user is the self or the admin
            if(model.UserName == user.UserName || Roles.IsUserInRole("administrator"))
            {
                user.Comment = string.Format("{0}#{1}#{2}",model.Picture,model.Name,model.Comment);
                Membership.UpdateUser(user);

                return RedirectToAction("Profile", "User");
            }
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult UploadPicture(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string fileType = file.ContentType.Split('/')[1];
                if(fileType.Equals("png") || fileType.Equals("bmp") || fileType.Equals("jpg") ){
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/"), Membership.GetUser().UserName +"."+ fileType);
                    file.SaveAs(path);
                    Response.StatusCode = 200;
                    return RedirectToAction("Profile", "User");
                }
            }
            Response.StatusCode = 400;
            return View("Error", new ErrorModel(400, "Upload Error! Please, try again."));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Delete(string username)
        {
            if(username.Equals(Membership.GetUser().UserName) || Roles.IsUserInRole("administrator"))
            {
                Membership.DeleteUser(username);
                
                Response.StatusCode = 200;
                if(!Roles.IsUserInRole("administrator"))
                    FormsAuthentication.SignOut();
                return View("Index", "Home");
            }
            Response.StatusCode = 401;
            return View("Error", new ErrorModel(401,"You do not have the required permission to delete user " + username));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserCreate toAdd)
        {

            if (!string.IsNullOrEmpty(toAdd.Name) && !string.IsNullOrEmpty(toAdd.UserName) && !string.IsNullOrEmpty(toAdd.Password))
            {
                    MembershipCreateStatus creationStatus;
                    MembershipUser ptrToNewUser = Membership.CreateUser(toAdd.UserName,toAdd.Password,toAdd.Email,null,null,true, out creationStatus);
                   

                    switch(creationStatus){
                        case MembershipCreateStatus.Success:
                            try
                            {
                                sendEmailToValidateUser(ptrToNewUser);
                                ptrToNewUser.Comment = string.Format("{0}#{1}#", DEFAULT_PICTURE, toAdd.Name);
                                Membership.UpdateUser(ptrToNewUser);
                            }
                            catch(Exception e){
                                Membership.DeleteUser(ptrToNewUser.UserName);
                                Response.StatusCode = 503;
                                return View("Error", new ErrorModel(503, "Oups... Our e-mail service is currently down, please try again another time!")); 
                            }
                            return RedirectToAction("Profile", "User", new { userId = ptrToNewUser.UserName });
                        default:
                            Response.StatusCode = 500;
                            return View("Error", new ErrorModel(500, "For reasons yet to be discovered, we cannot register you at the moment. Please, try again later."));
                            
                    }
            }
            Response.StatusCode = 400;
            return View("Error", new ErrorModel(400, "Some of the details introduced in order to register you in our server are invalid. Please, try again."));
  
        }

        private void sendEmailToValidateUser(MembershipUser User) 
        {
            MailMessage email = new MailMessage("projectpinternet@gmail.com",User.Email);
     
            email.Subject = "Welcome " + User.UserName + "!";
            email.Body = "Activation link: "+ Url.Action("Validate","User",null,"http") + "?email=" + User.Email + "&hash=" + HashCode(User.UserName + User.CreationDate);
            SmtpClient smtp = new SmtpClient();
            smtp.Send(email);
       }

        [HttpPost]
        public ActionResult LogIn(UserLogin model, string returnUrl)
        {
            if (Membership.ValidateUser(model.UserName, model.Password))
            {
               FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
               if (returnUrl != null)
                   return Redirect(returnUrl);
               else
                   return RedirectToAction("Profile", "User", new { userId = model.UserName});
            }
            Response.StatusCode = 400;
            model.errorDescription = "User not found or Password incorrect. Please, try again.";
            return View(model);
        
        }
        
        [HttpGet]
        public ActionResult LogIn(){
            return View();
        }

        [HttpGet]
        public ActionResult LogOut() {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Validate(string email, string hash) {
            string username = Membership.GetUserNameByEmail(email);
            var user = Membership.GetUser(username, false);
            string hashToVerify = HashCode(username + user.CreationDate);
            if (hashToVerify.Equals(hash))
            {
                if (!user.IsApproved)
                {
                    user.IsApproved = true;
                    Membership.UpdateUser(user);
                }
            }
            return RedirectToAction("profile", "user");
        }

        private static string HashCode(string str)
        {
            string rethash = "";
            try
            {

                System.Security.Cryptography.SHA1 hash = System.Security.Cryptography.SHA1.Create();
                System.Text.ASCIIEncoding encoder = new System.Text.ASCIIEncoding();
                byte[] combined = encoder.GetBytes(str);
                hash.ComputeHash(combined);
                rethash = Convert.ToBase64String(hash.Hash);
            }
            catch (Exception ex)
            {
                string strerr = "Error in HashCode : " + ex.Message;
            }
            return rethash;
        }
    }
}
