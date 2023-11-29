using System;
using System.Collections.Generic;

namespace NAFCommon.Base.Common.Entity
{
    public abstract class Entity<TKey> : BaseEntity where TKey : struct
    {
        public TKey Id { get; set; }

        public TKey? CreatedById { get; set; }

        public TKey? UpdatedById { get; set; }

        public TKey? DeletedById { get; set; }

        public virtual bool IsTransient()
        {
            if (EqualityComparer<TKey>.Default.Equals(Id, default))
            {
                return true;
            }

            //Workaround for EF Core since it sets int/long to min value when attaching to dbcontext
            if (typeof(TKey) == typeof(int))
            {
                return Convert.ToInt32(Id) <= 0;
            }

            if (typeof(TKey) == typeof(long))
            {
                return Convert.ToInt64(Id) <= 0;
            }

            return false;
        }
    }
}