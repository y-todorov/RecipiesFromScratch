using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Web;

namespace RecipiesModelNS
{
    public class YordanBaseEntity
    {
        public virtual void Adding(DbEntityEntry e = null)
        {
            SetModifiedDateAndModifiedByUserFields();
        }

        public virtual void Added(DbEntityEntry e = null)
        {
        }

        public virtual void Changing(DbEntityEntry e = null)
        {
            SetModifiedDateAndModifiedByUserFields();
        }

        public virtual void Changed(DbEntityEntry e = null)
        {
        }

        public virtual void Removing(DbEntityEntry e = null)
        {
        }

        public virtual void Removed(DbEntityEntry e = null)
        {
        }

        private void SetModifiedDateAndModifiedByUserFields()
        {
            string userName = null;
            if (HttpContext.Current != null && HttpContext.Current.User != null)
            {
                userName = HttpContext.Current.User.Identity.Name;
            }
            Type type = GetType();
            PropertyInfo piModifiedDate = type.GetProperties().FirstOrDefault(p => p.Name.Equals("ModifiedDate"));
            if (piModifiedDate != null)
            {
                DateTime modifiedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,
                    TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"));
                piModifiedDate.SetValue(this, modifiedDate);
            }
            PropertyInfo piModifiedByUser = type.GetProperties().FirstOrDefault(p => p.Name.Equals("ModifiedByUser"));
            if (piModifiedByUser != null)
            {
                if (!string.IsNullOrEmpty(userName))
                {
                    piModifiedByUser.SetValue(this, userName);
                }
            }
        }

    }
}