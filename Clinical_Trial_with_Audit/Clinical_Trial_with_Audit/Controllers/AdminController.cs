using Clinical_Trial_with_Audit.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clinical_Trial_with_Audit.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        ClinicalTrial_Context CTC = new ClinicalTrial_Context();

        // GET: Admin
        public ActionResult Index()
        {
            return View(CTC.Tbl_User_Registration.ToList());
        }

        public ActionResult Approve(int id)
        {

            Tbl_User_Registration tbl_Register_user = CTC.Tbl_User_Registration.SingleOrDefault(m => m.Reg_No == id);
            if (ModelState.IsValid)
            {
                tbl_Register_user.Approve_Status = "Approved";
                CTC.Entry(tbl_Register_user).State = EntityState.Modified;
                CTC.SaveChanges();
               
            }
            return RedirectToAction("Index");
        }
        public ActionResult Reject(int id)
        {
            Tbl_User_Registration tbl_Register_user = CTC.Tbl_User_Registration.Find(id);
            if (ModelState.IsValid)
            {
                tbl_Register_user.Approve_Status = "Rejected";
                CTC.Entry(tbl_Register_user).State = EntityState.Modified;

                CTC.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            Tbl_User_Registration tbl_Register_user = CTC.Tbl_User_Registration.Find(id);
            if (ModelState.IsValid)
            {
                tbl_Register_user.Is_Active = 0;
                CTC.Entry(tbl_Register_user).State = EntityState.Modified;
                CTC.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}