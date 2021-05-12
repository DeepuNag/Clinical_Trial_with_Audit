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
    public class NewAudiApitController : ApiController
    {
        private PPDEntities2 db = new PPDEntities2();

        // GET: api/NewAudiApit
        public IQueryable<VM_New_Audit> GetVM_New_Audit()
        {
            return db.VM_New_Audit;
        }

        // GET: api/NewAudiApit/5
        [ResponseType(typeof(VM_New_Audit))]
        public IHttpActionResult GetVM_New_Audit(int id)
        {
            List<VM_New_Audit> vM_New_Audit = db.VM_New_Audit.Where(x => x.Reg_No==id).ToList();
            if (vM_New_Audit == null)
            {
                return NotFound();
            }

            return Ok(vM_New_Audit);
        }

        // PUT: api/NewAudiApit/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVM_New_Audit(Guid id, VM_New_Audit vM_New_Audit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vM_New_Audit.Id)
            {
                return BadRequest();
            }

            db.Entry(vM_New_Audit).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VM_New_AuditExists(id))
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

        // POST: api/NewAudiApit
        [ResponseType(typeof(VM_New_Audit))]
        public IHttpActionResult PostVM_New_Audit(VM_New_Audit vM_New_Audit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VM_New_Audit.Add(vM_New_Audit);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (VM_New_AuditExists(vM_New_Audit.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = vM_New_Audit.Id }, vM_New_Audit);
        }

        // DELETE: api/NewAudiApit/5
        [ResponseType(typeof(VM_New_Audit))]
        public IHttpActionResult DeleteVM_New_Audit(Guid id)
        {
            VM_New_Audit vM_New_Audit = db.VM_New_Audit.Find(id);
            if (vM_New_Audit == null)
            {
                return NotFound();
            }

            db.VM_New_Audit.Remove(vM_New_Audit);
            db.SaveChanges();

            return Ok(vM_New_Audit);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VM_New_AuditExists(Guid id)
        {
            return db.VM_New_Audit.Count(e => e.Id == id) > 0;
        }
    }
}