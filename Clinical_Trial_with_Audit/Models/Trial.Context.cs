﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Clinical_Trial_with_Audit.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Text;
    using System.Web;

    public partial class Trial_Context : DbContext
    {
        public Trial_Context()
            : base("name=Trial_Context")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
        public int SaveChanges(int userId)
        {
            // Get all Added/Deleted/Modified entities (not Unmodified or Detached)
            foreach (var ent in this.ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Deleted || p.State == EntityState.Modified))
            {
                // For each changed record, get the audit record entries and add them
                foreach (AuditLog x in GetAuditRecordsForChange(ent, userId))
                {
                    this.AuditLogs.Add(x);
                }
            }

            // Call the original SaveChanges(), which will save both the changes made and the audit records
            return base.SaveChanges();
        }
        private List<AuditLog> GetAuditRecordsForChange(DbEntityEntry dbEntry, int userId)
        {
            List<AuditLog> result = new List<AuditLog>();
            string guid = HttpContext.Current.Session["UserName"].ToString();

            TableAttribute tableAttr = dbEntry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as TableAttribute;

            string tableName = tableAttr != null ? tableAttr.Name : dbEntry.Entity.GetType().Name;

            string keyName = dbEntry.Entity.GetType().GetProperties().FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Count() > 0).Name;

            if (dbEntry.State == EntityState.Added)
            {
                var newValues = new StringBuilder();
                SetAddedProperties(dbEntry, newValues);
                foreach (var propertyName in dbEntry.CurrentValues.PropertyNames)
                {
                    result.Add(new AuditLog()
                    {

                        Id = Guid.NewGuid(),
                        AuditType = "Inserted",
                        TableName = tableName,
                        Reg_No = Convert.ToInt32(dbEntry.CurrentValues.GetValue<object>(keyName).ToString()),
                        OldValue = string.Empty,
                        //NewValue= Convert.ToString(dbEntry.CurrentValues[prop]),
                        NewValue = newValues.ToString(),
                        Date = DateTime.Now,
                        UserName = guid,
                        column_name = propertyName

                    }
                    );
                }
            }
            else if (dbEntry.State == EntityState.Deleted)
            {
                var oldValues = new StringBuilder();
                SetDeletedProperties(dbEntry, oldValues);

                foreach (var propertyName in dbEntry.CurrentValues.PropertyNames)
                {
                    // Same with deletes, do the whole record, and use either the description from Describe() or ToString()
                    result.Add(new AuditLog()
                    {
                        Id = Guid.NewGuid(),
                        AuditType = "Deleted",
                        TableName = tableName,
                        Reg_No = userId,
                        //OldValue = Convert.ToString(dbEntry.OriginalValues(Convert.ToString([dbEntry.OriginalValues.PropertyNames])),
                        OldValue = oldValues.ToString(),
                        NewValue = string.Empty,
                        Date = DateTime.Now,
                        UserName = guid,
                        column_name = propertyName
                    }
                    );
                }
            }
            else if (dbEntry.State == EntityState.Modified)
            {
                foreach (string propertyName in dbEntry.OriginalValues.PropertyNames)
                {
                    object currentValue = dbEntry.CurrentValues[propertyName];
                    object originalValue = dbEntry.OriginalValues[propertyName];
                    // For updates, we only want to capture the columns that actually changed
                    if (!object.Equals(dbEntry.OriginalValues.GetValue<object>(propertyName), dbEntry.CurrentValues.GetValue<object>(propertyName)))
                    {
                        var oldValues = new StringBuilder();
                        var newValues = new StringBuilder();
                        SetModifiedProperties(dbEntry, oldValues, newValues);
                        result.Add(new AuditLog()
                        {
                            Id = Guid.NewGuid(),
                            AuditType = "Modified",
                            TableName = tableName,
                            Reg_No = userId,
                            OldValue = Convert.ToString(originalValue),
                            NewValue = Convert.ToString(currentValue),
                            Date = DateTime.Now,
                            UserName = guid,
                            column_name=propertyName
                        }
                            );
                    }
                }
            }

            return result;
        }
        private void SetAddedProperties(DbEntityEntry entry, StringBuilder newData)
        {
            foreach (var propertyName in entry.CurrentValues.PropertyNames)
            {
                var newVal = entry.CurrentValues[propertyName];
                if (newVal != null)
                {
                    newData.AppendFormat("{0}||", newVal);
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
                    oldData.AppendFormat("{0}|| ", oldVal);
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
                    newData.AppendFormat("{0}|| ",newVal);
                    oldData.AppendFormat("{0} || ",oldVal);
                }
            }
            if (oldData.Length > 0)
                oldData = oldData.Remove(oldData.Length - 3, 3);
            if (newData.Length > 0)
                newData = newData.Remove(newData.Length - 3, 3);
        }

        public virtual DbSet<AuditLog> AuditLogs { get; set; }
        public virtual DbSet<Tbl_Audit_Register_user> Tbl_Audit_Register_user { get; set; }
        public virtual DbSet<Tbl_Audit_user_Registration> Tbl_Audit_user_Registration { get; set; }
        public virtual DbSet<Tbl_Clinical_Trials> Tbl_Clinical_Trials { get; set; }
        public virtual DbSet<Tbl_Register_user> Tbl_Register_user { get; set; }
        public virtual DbSet<Tbl_Trial_Type> Tbl_Trial_Type { get; set; }
        public virtual DbSet<Tbl_User> Tbl_User { get; set; }
        public virtual DbSet<Tbl_User_Registration> Tbl_User_Registration { get; set; }
    }
}
