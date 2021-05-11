//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Data.Entity.Core.Objects;
//using System.Data.Entity.Infrastructure;
//using System.Linq;
//using System.Web;

//namespace Clinical_Trial_with_Audit.Models
//{
//    public class AuditTrail
//    {
//        public static List<AuditLog> GetLogEntries(DbEntityEntry entry)

//        {

//            List<AuditLog> listLogs = new List<AuditLog>();
//            string guidString = HttpContext.Current.Session["UserName"].ToString(); ;
//            Guid guid = new Guid(guidString);
//            var entries = entry.GetObjectStateEntries(EntityState.Added | EntityState.Modified | EntityState.Deleted);

//            foreach (var entry in entries)

//            {

//                var tableName = entry.Entity.GetType().Name;

//                var pk = GetPrimaryKeys(entry);

//                if (entry.State == EntityState.Added)

//                {

//                    var currentEntry = entities.GetObjectStateEntry(entry.EntityKey);

//                    var currentValues = currentEntry.CurrentValues;

//                    for (var i = 0; i < currentValues.FieldCount; i++)

//                    {

//                        var propName = currentValues.DataRecordInfo.FieldMetadata[i].FieldType.Name;

//                        var newValue = currentValues[propName].ToString();

//                        var log = new AuditLog()

//                        {

//                            Id = Guid.NewGuid(),

//                            AuditType = "I",

//                            TableName = tableName,

//                            Reg_No = int.Parse(pk),

//                            OldValue = null,

//                            NewValue = newValue,

//                            Date = DateTime.Now,

//                            UserName = guid

//                        };

//                        listLogs.Add(log);

//                    }

//                }

//                else if (entry.State == EntityState.Modified)

//                {

//                    var currentEntry = entities.GetObjectStateEntry(entry.EntityKey);

//                    var currentValues = currentEntry.CurrentValues;

//                    var originalValues = currentEntry.OriginalValues;

//                    var properties = currentEntry.GetModifiedProperties();

//                    foreach (var propName in properties)

//                    {

//                        var oldValue = originalValues[propName].ToString();

//                        var newValue = currentValues[propName].ToString();

//                        if (oldValue == newValue) continue;

//                        var log = new AuditLog()

//                        {

//                            Id = Guid.NewGuid(),

//                            AuditType= "M",

//                            TableName = tableName,

//                            Reg_No = int.Parse(pk),

//                            OldValue = oldValue,

//                            NewValue = newValue,

//                            Date = DateTime.Now,

//                            UserName = guid

//                        };

//                        listLogs.Add(log);

//                    }

//                }

//                else if (entry.State == EntityState.Deleted)

//                {

//                    var currentEntry = entities.GetObjectStateEntry(entry.EntityKey);

//                    var originalValues = currentEntry.OriginalValues;

//                    for (var i = 0; i < originalValues.FieldCount; i++)

//                    {

//                        var oldValue = originalValues[i].ToString();

//                        var log = new AuditLog()

//                        {

//                            Id = Guid.NewGuid(),

//                            AuditType = "D",

//                            TableName = tableName,

//                            Reg_No = int.Parse(pk),

//                            OldValue = oldValue,

//                            NewValue = null,

//                            Date = DateTime.Now,

//                            UserName = guid

//                        };

//                        listLogs.Add(log);

//                    }

//                }

//            }

//            return listLogs;

//        }

//        internal static List<AuditLog> GetLogEntries(object objectStateManager)
//        {
//            throw new NotImplementedException();
//        }

//        private static string GetPrimaryKeys(ObjectStateEntry entry)

//        {

//            string pk = string.Empty;

//            if (entry.EntityKey == null || entry.EntityKey.EntityKeyValues == null || entry.EntityKey.EntityKeyValues.Length == 0) return "N/A";

//            foreach (var keyValue in entry.EntityKey.EntityKeyValues)

//            {

//                pk += string.Format("{0}={1};", keyValue.Key, keyValue.Value);
               
//            }

//            return pk;


//        }
//    }
//}