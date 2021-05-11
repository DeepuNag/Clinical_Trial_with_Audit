using Clinical_Trial_with_Audit.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web;

namespace Clinical_Trial.Models
{
    public class AuditTrailFactory
    {
        private DbContext context;

        public AuditTrailFactory(DbContext context)
        {
            this.context = context;
        }
        public AuditLog GetAudit(DbEntityEntry entry)
        {
            AuditLog audit = new AuditLog();
            string guid = HttpContext.Current.Session["UserName"].ToString(); ;
            //Guid guid = new Guid(guidString);
            
            //entry is Added 
            if (entry.State == EntityState.Added)
            {
                audit.Id = Guid.NewGuid();
                // var user = (User)HttpContext.Current.Session[":user"];
                audit.UserName = guid;
                //var tableName = entry.Entity.GetType().Name;
                audit.TableName = GetTableName(entry);

                audit.Reg_No = GetKeyValue(entry);

                var newValues = new StringBuilder();
                SetAddedProperties(entry, newValues);
                audit.Date = DateTime.Now;
                audit.NewValue = newValues.ToString();
                audit.OldValue = null;
                audit.AuditType = AuditActions.I.ToString();
            }
            //entry in deleted
            else if (entry.State == EntityState.Deleted)
            {
                audit.Id = Guid.NewGuid();
                // var user = (User)HttpContext.Current.Session[":user"];
                audit.UserName = guid;
                //var tableName = entry.Entity.GetType().Name;
                audit.TableName = GetTableName(entry);
                audit.Date = DateTime.Now;
                audit.Reg_No = GetKeyValue(entry);
                audit.NewValue = null;
                var oldValues = new StringBuilder();
                SetDeletedProperties(entry, oldValues);
                audit.OldValue = oldValues.ToString();
                audit.AuditType = AuditActions.D.ToString();
            }
            //entry is modified
            else if (entry.State == EntityState.Modified)
            {
                audit.Id = Guid.NewGuid();
                // var user = (User)HttpContext.Current.Session[":user"];
                audit.UserName = guid;
                //var tableName = entry.Entity.GetType().Name;
                audit.TableName = GetTableName(entry);
                audit.Date = DateTime.Now;
                audit.Reg_No= GetKeyValue(entry);

                var oldValues = new StringBuilder();
                var newValues = new StringBuilder();
                SetModifiedProperties(entry, oldValues, newValues);
                audit.OldValue = oldValues.ToString();
                audit.NewValue = newValues.ToString();
                audit.AuditType = AuditActions.U.ToString();
            }

            return audit;
        }

        private void SetAddedProperties(DbEntityEntry entry, StringBuilder newData)
        {
            foreach (var propertyName in entry.CurrentValues.PropertyNames)
            {
                var newVal = entry.CurrentValues[propertyName];
                if (newVal != null)
                {
                    newData.AppendFormat("{0}={1} || ", propertyName, newVal);
                }
            }
            if (newData.Length > 0)
                newData = newData.Remove(newData.Length - 3, 3);
        }

        private void SetDeletedProperties(DbEntityEntry entry, StringBuilder oldData)
        {
            DbPropertyValues dbValues = entry.GetDatabaseValues();
            foreach (var propertyName in dbValues.PropertyNames)
            {
                var oldVal = dbValues[propertyName];
                if (oldVal != null)
                {
                    oldData.AppendFormat("{0}={1} || ", propertyName, oldVal);
                }
            }
            if (oldData.Length > 0)
                oldData = oldData.Remove(oldData.Length - 3, 3);
        }

        private void SetModifiedProperties(DbEntityEntry entry, StringBuilder oldData, StringBuilder newData)
        {
            DbPropertyValues dbValues = entry.GetDatabaseValues();
            foreach (var propertyName in entry.OriginalValues.PropertyNames)
            {
                var oldVal = dbValues[propertyName];
                var newVal = entry.CurrentValues[propertyName];
                if (oldVal != null && newVal != null && !Equals(oldVal, newVal))
                {
                    newData.AppendFormat("{0}={1} || ", propertyName, newVal);
                    oldData.AppendFormat("{0}={1} || ", propertyName, oldVal);
                }
            }
            if (oldData.Length > 0)
                oldData = oldData.Remove(oldData.Length - 3, 3);
            if (newData.Length > 0)
                newData = newData.Remove(newData.Length - 3, 3);
        }

        public int GetKeyValue(DbEntityEntry entry)
        {
            var objectStateEntry = ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            int id =0;
            if (objectStateEntry.EntityKey.EntityKeyValues != null)
                id = Convert.ToInt32(objectStateEntry.EntityKey.EntityKeyValues[0].Value);
                //id = objectStateEntry.EntityKey.EntityKeyValues[0].Value.ToString();
            return id;
        }

        private string GetTableName(DbEntityEntry dbEntry)
        {
            TableAttribute tableAttr = dbEntry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as TableAttribute;
            string tableName = tableAttr != null ? tableAttr.Name : dbEntry.Entity.GetType().Name;
            return tableName;
        }
    }

    public enum AuditActions
    {
        I,
        U,
        D
    }
}
    