using System.Collections.Generic;
//using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;
using System.Linq;
using System.Runtime.Caching;

namespace RecipiesModelNS
{
    public partial class RecipiesEntities : DbContext
    {
        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            var result = new DbEntityValidationResult(entityEntry, new List<DbValidationError>());
            YordanBaseEntity ybe = entityEntry.Entity as YordanBaseEntity;
            if (ybe != null)
            {
                var validationErrors = ybe.ValidateEntitiyBeforeSave(entityEntry);
                foreach (DbValidationError validationError in validationErrors)
                {
                    result.ValidationErrors.Add(validationError);
                }

            }
            if (result.ValidationErrors.Count > 0)
            {
                return result;
            }
            return base.ValidateEntity(entityEntry, items);
        }
        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            IEnumerable<DbEntityEntry> entries = ChangeTracker.Entries();

            List<YordanBaseEntity> addedEntities = new List<YordanBaseEntity>();
            List<YordanBaseEntity> modifiedEntities = new List<YordanBaseEntity>();
            List<YordanBaseEntity> deletedEntities = new List<YordanBaseEntity>();


            foreach (DbEntityEntry entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    YordanBaseEntity ybe = entry.Entity as YordanBaseEntity;
                    if (ybe != null)
                    {
                        ybe.Adding(entry);
                        addedEntities.Add(ybe);
                    }
                }
                else if (entry.State == EntityState.Deleted)
                {
                    YordanBaseEntity ybe = entry.Entity as YordanBaseEntity;
                    if (ybe != null)
                    {
                        ybe.Removing(entry);
                        deletedEntities.Add(ybe);
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    YordanBaseEntity ybe = entry.Entity as YordanBaseEntity;
                    if (ybe != null)
                    {
                        ybe.Changing(entry);
                        modifiedEntities.Add(ybe);
                    }
                }
            }
            int result = base.SaveChanges();

            if (result != 0)
            {
                //MemoryCache.Default.Dispose(); NEVER CALL DISPOSE. YOU WILL NE LONGER CAN USE IT.
                List<string> cacheKeys = MemoryCache.Default.Select(kvp => kvp.Key).ToList();

                foreach (string cacheKey in cacheKeys)
                {
                    MemoryCache.Default.Remove(cacheKey);
                }

            }

            // THIS HERE IS REALLY PROBLEMATIC AND REALLY SLOW BECAUSE OF THE RECURSION -> SAVE CHANGES IS CALLED MANY MANY TIMES

            //foreach (YordanBaseEntity ybe in addedEntities)
            //{
            //    ybe.Added();
            //}
            //foreach (YordanBaseEntity ybe in modifiedEntities)
            //{
            //    ybe.Changed();
            //}
            foreach (YordanBaseEntity ybe in deletedEntities)
            {
                ybe.Removed();
            }

            // Mega test
            //ContextFactory.RemoveFromCache();

            return result;
        }
    }
}