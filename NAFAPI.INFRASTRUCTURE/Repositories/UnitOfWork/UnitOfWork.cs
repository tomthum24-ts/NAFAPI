using Microsoft.EntityFrameworkCore;
using NAF.INFRASTRUCTURE.Interface.UnitOfWork;
using NAFCommon.Base.Common.ClaimUser;
using NAFCommon.Base.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NAF.INFRASTRUCTURE.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IUserSessionInfo _userSessionInfo;
        private readonly DbContext _context;
        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            StandardizeEntities();
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result;
        }
        protected void StandardizeEntities()
        {
            var dateTimeNow = DateTime.UtcNow;

            var listState = new List<EntityState>
            {
                EntityState.Added,
                EntityState.Modified
            };

            var listEntryAddUpdate = _context.ChangeTracker.Entries()
                .Where(x => listState.Contains(x.State))
                .Select(x => x)
                .ToList();

            foreach (var entry in listEntryAddUpdate)
            {
                //if (entry.Entity is BaseEntity baseEntity)
                //{
                //    if (entry.State == EntityState.Added)
                //    {
                //        baseEntity.DeleteDate = null;

                //        baseEntity.UpdateDate = null;

                //        baseEntity.CreatedDate = dateTimeNow;
                //    }
                //    else
                //    {
                //        if (baseEntity.IsDelete != null && baseEntity.IsDelete == true)
                //        {
                //            baseEntity.DeleteDate = dateTimeNow;
                //        }
                //        else
                //        {
                //            baseEntity.UpdateDate = dateTimeNow;
                //        }
                //    }
                //}

                if (entry.Entity is APIEntity entity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedById = _userSessionInfo.ID;
                        entity.CreationDate = dateTimeNow;
                    }
                    else
                    {
                        if (entity.IsDelete != null && entity.IsDelete == true)
                        {
                            entity.DeletedById = _userSessionInfo.ID;
                            entity.DeletionDate = dateTimeNow;
                        }
                        else
                        {
                            entity.UpdatedById = _userSessionInfo.ID;
                            entity.UpdateDate = dateTimeNow;
                        }
                    }
                }
            }
        }
    }
}
