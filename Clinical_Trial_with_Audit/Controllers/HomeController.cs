using Clinical_Trial.Models;
using Clinical_Trial_with_Audit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Clinical_Trial_with_Audit.Controllers
{
    
    public class HomeController : Controller
    {
        Trial_Context CTC = new Trial_Context();

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CheckValidUser(string username, string password)
        {
            LoginStatus status = new LoginStatus();
            status.Success = false;
            Tbl_User user = CTC.Tbl_User.SingleOrDefault(x => x.Username == username && x.password == password);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(username, false);
                Session["UserID"] = user.Userid.ToString();
                Session["UserName"] = user.Username.ToString();
                Session["Role"] = user.role.ToString();
                status.TargetURL = FormsAuthentication.DefaultUrl;
                status.Success = true;
            }

            return Json(status);
        }

        [Authorize]
        public ActionResult GetAllTrials()
        {
            TrialInfo_Context context = new TrialInfo_Context();
            List<VW_Trial> trial = context.VW_Trial.ToList();
            return View(trial);
        }

        [Authorize]
        public ActionResult Trial_Details(int id)
        {
            TrialInfo_Context context = new TrialInfo_Context();
            var result = context.VW_Trial.Where(x => x.Trial_No == id).FirstOrDefault();
            return View(result);
        }

        [Authorize]
        public ActionResult Register(int id)
        {
            ViewBag.id = id;
            return View();

        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel rvm)

        {
            Tbl_User_Registration reg = new Tbl_User_Registration();
            if (ModelState.IsValid)
            {

                reg.Reg_No = rvm.Reg_No;
                reg.Trial_No = rvm.Trial_No;
                reg.FullName = rvm.FullName;
                reg.Dob = rvm.Dob;
                reg.Mob = rvm.Mob;
                reg.Address1 = rvm.Address1;
                reg.Existing_Condition = rvm.Existing_Condition;
                reg.Registration_date = rvm.Registration_date;
                reg.Approve_Status = "Pending";
                reg.Is_Active = 1;
               CTC.Tbl_User_Registration.Add(reg);
               CTC.SaveChanges(reg.Reg_No);

                ModelState.Clear();

                ViewBag.Message = "Successfully Added";

               
            }

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["UserName"] = null;
            //Session.Clear();
            //Session.Abandon();
            return RedirectToAction("Login");
        }

    }
}