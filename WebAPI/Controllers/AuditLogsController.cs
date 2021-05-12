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
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class AuditLogsController : ApiController
    {
        private PPDEntities db = new PPDEntities();

        // GET: api/AuditLogs
        public IQueryable<AuditLog> GetAuditLogs()
        {
            return db.AuditLogs;
        }

        // GET: api/AuditLogs/5
        [ResponseType(typeof(AuditLog))]
        public IHttpActionResult GetAuditLog(Guid id)
        {
            AuditLog auditLog = db.AuditLogs.Find(id);
            if (auditLog == null)
            {
                return NotFound();
            }

            return Ok(auditLog);
        }

        // PUT: api/AuditLogs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAuditLog(Guid id, AuditLog auditLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != auditLog.Id)
            {
                return BadRequest();
            }

            db.Entry(auditLog).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuditLogExists(id))
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

        // POST: api/AuditLogs
        [ResponseType(typeof(AuditLog))]
        public IHttpActionResult PostAuditLog(AuditLog auditLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AuditLogs.Add(auditLog);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AuditLogExists(auditLog.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = auditLog.Id }, auditLog);
        }

        // DELETE: api/AuditLogs/5
        [ResponseType(typeof(AuditLog))]
        public IHttpActionResult DeleteAuditLog(Guid id)
        {
            AuditLog auditLog = db.AuditLogs.Find(id);
            if (auditLog == null)
            {
                return NotFound();
            }

            db.AuditLogs.Remove(auditLog);
            db.SaveChanges();

            return Ok(auditLog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AuditLogExists(Guid id)
        {
            return db.AuditLogs.Count(e => e.Id == id) > 0;
        }
    }
}