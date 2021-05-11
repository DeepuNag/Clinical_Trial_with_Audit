using Clinical_Trial_with_Audit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clinical_Trial_with_Audit.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        PPDEntities1 entity = new PPDEntities1();
        public ActionResult Index()
        {
            return View(entity.VW_Audit.ToList());
        }
        public ActionResult Index(int id)
        {
            List<VW_Audit> trial = entity.VW_Audit.Where(x => x.Reg_No == id).ToList();
            return View(trial);
        }
    }
}