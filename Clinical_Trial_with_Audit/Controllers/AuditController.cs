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
    public class AuditController : ApiController
    {
        private PPDEntities1 db = new PPDEntities1();

        // GET: api/Audit
        public IQueryable<VW_Audit> GetVW_Audit()
        {
            return db.VW_Audit;
        }

        // GET: api/Audit/5
        [ResponseType(typeof(VW_Audit))]
        public IHttpActionResult GetVW_Audit(int id)
        {
            List<VW_Audit> trial = db.VW_Audit.Where(x => x.Reg_No == id).ToList();
            if (trial == null)
            {
                return NotFound();
            }

            return Ok(trial);
        }

        // PUT: api/Audit/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVW_Audit(Guid id, VW_Audit vW_Audit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vW_Audit.Id)
            {
                return BadRequest();
            }

            db.Entry(vW_Audit).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VW_AuditExists(id))
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

        // POST: api/Audit
        [ResponseType(typeof(VW_Audit))]
        public IHttpActionResult PostVW_Audit(VW_Audit vW_Audit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VW_Audit.Add(vW_Audit);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (VW_AuditExists(vW_Audit.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = vW_Audit.Id }, vW_Audit);
        }

        // DELETE: api/Audit/5
        [ResponseType(typeof(VW_Audit))]
        public IHttpActionResult DeleteVW_Audit(Guid id)
        {
            VW_Audit vW_Audit = db.VW_Audit.Find(id);
            if (vW_Audit == null)
            {
                return NotFound();
            }

            db.VW_Audit.Remove(vW_Audit);
            db.SaveChanges();

            return Ok(vW_Audit);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VW_AuditExists(Guid id)
        {
            return db.VW_Audit.Count(e => e.Id == id) > 0;
        }
    }
}