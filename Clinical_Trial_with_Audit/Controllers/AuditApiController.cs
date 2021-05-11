using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Clinical_Trial_with_Audit.Models;

namespace Clinical_Trial_with_Audit.Controllers
{
    public class AuditApiController : ApiController
    {
        private Trial_Context db = new Trial_Context();

        // GET: api/AuditApi
        public IQueryable<Tbl_Audit_user_Registration> GetTbl_Audit_user_Registration()
        {
            return db.Tbl_Audit_user_Registration;
        }

        // GET: api/AuditApi/5
        [ResponseType(typeof(Tbl_Audit_user_Registration))]
        public IHttpActionResult GetTbl_Audit_user_Registration(int id)
        {
            List<Tbl_Audit_user_Registration> trial = db.Tbl_Audit_user_Registration.Where(x => x.Reg_No == id).ToList();
            if (trial == null)
            {
                return NotFound();
            }

            return Ok(trial);
        }

        // PUT: api/AuditApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTbl_Audit_user_Registration(int id, Tbl_Audit_user_Registration tbl_Audit_user_Registration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Audit_user_Registration.Audit_No)
            {
                return BadRequest();
            }
            //db.ObjectStateManager.ChangeObjectState(tbl_Audit_user_Registration, EntityState.Modified);
            db.Entry(tbl_Audit_user_Registration).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tbl_Audit_user_RegistrationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/AuditApi
        [ResponseType(typeof(Tbl_Audit_user_Registration))]
        public IHttpActionResult PostTbl_Audit_user_Registration(Tbl_Audit_user_Registration tbl_Audit_user_Registration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tbl_Audit_user_Registration.Add(tbl_Audit_user_Registration);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Audit_user_Registration.Audit_No }, tbl_Audit_user_Registration);
        }

        // DELETE: api/AuditApi/5
        [ResponseType(typeof(Tbl_Audit_user_Registration))]
        public IHttpActionResult DeleteTbl_Audit_user_Registration(int id)
        {
            Tbl_Audit_user_Registration tbl_Audit_user_Registration = db.Tbl_Audit_user_Registration.Find(id);
            if (tbl_Audit_user_Registration == null)
            {
                return NotFound();
            }

            db.Tbl_Audit_user_Registration.Remove(tbl_Audit_user_Registration);
            db.SaveChanges();

            return Ok(tbl_Audit_user_Registration);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Tbl_Audit_user_RegistrationExists(int id)
        {
            return db.Tbl_Audit_user_Registration.Count(e => e.Audit_No == id) > 0;
        }
    }
}