using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace RecipiesModelNS
{
    public partial class Employee : YordanBaseEntity
    {
        public override List<DbValidationError> ValidateEntitiyBeforeSave(DbEntityEntry entityEntry)
        {
            DbEntityEntry<Employee> employee = entityEntry.Cast<Employee>();
            var validationErrors = new List<DbValidationError>();

            if (string.IsNullOrEmpty(employee.Property(p => p.FirstName).CurrentValue))
            {
                validationErrors.Add(new DbValidationError(employee.Property(p => p.FirstName).Name,
                    "This field cannot be empty!"));
            }
            return validationErrors;
        }
    }
}