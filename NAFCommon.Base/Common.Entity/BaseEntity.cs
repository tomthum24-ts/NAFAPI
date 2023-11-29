using NAFCommon.Base.Common.Entity;
using System;
using System.Collections.Generic;

namespace NAFCommon.Base
{
    public abstract class BaseEntity : EntityValidator
    {
        protected BaseEntity()
        {
            _errorMessages ??= new List<Result>();
        }

        public DateTime? DeletionDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
    }
}