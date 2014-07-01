using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;

namespace RecipiesModelNS
{
    public partial class UnitMeasure : YordanBaseEntity
    {
        public override List<DbValidationError> ValidateEntitiyBeforeSave(DbEntityEntry entityEntry)
        {
            DbEntityEntry<UnitMeasure> unitMeasure = entityEntry.Cast<UnitMeasure>();
            List<DbValidationError> validationErrors = new List<DbValidationError>();

            if (string.IsNullOrEmpty(unitMeasure.Property(p => p.Name).CurrentValue))
            {
                validationErrors.Add(new DbValidationError(unitMeasure.Property(p => p.Name).Name,
                "Name of the unit measure cannot be empty!"));
            }

            if (unitMeasure.Property(p => p.BaseUnitFactor).CurrentValue.GetValueOrDefault() < 0 )
            {
                validationErrors.Add(new DbValidationError(unitMeasure.Property(p => p.BaseUnitFactor).Name,
                "BaseUnitFactor of the unit measure cannot be less than 0!"));
            }

            if (entityEntry.State == EntityState.Modified)
            {
                if (unitMeasure.Property(p => p.BaseUnitId).CurrentValue != unitMeasure.Property(p => p.BaseUnitId).OriginalValue)
                {
                    validationErrors.Add(new DbValidationError(unitMeasure.Property(p => p.BaseUnitId).Name,
                "Once set Base Unit of the unit measure cannot be changed!"));
                }

                if (unitMeasure.Property(p => p.IsBaseUnit).CurrentValue != unitMeasure.Property(p => p.IsBaseUnit).OriginalValue)
                {
                    validationErrors.Add(new DbValidationError(unitMeasure.Property(p => p.IsBaseUnit).Name,
                "Once set IsBaseUnit of the unit measure cannot be changed!"));
                }
            }

            return validationErrors;
        }

        public List<UnitMeasure> GetRelatedUnitMeasures()
        {
            int baseUnitMeasureId = UnitMeasureId;
            List<UnitMeasure> result =
                ContextFactory.Current
                    .UnitMeasures.Where(
                        um => um.BaseUnitId == baseUnitMeasureId || um.UnitMeasureId == baseUnitMeasureId)
                    .ToList();

            return result;
        }
    }
}