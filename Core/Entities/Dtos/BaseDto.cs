using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Dtos
{
    public abstract class BaseDto
    {
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public long? DeletedBy { get; set; }
    }
}
