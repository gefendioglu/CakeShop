using Core.Entities.Abstract;
using Core.Entities.Concrete.Enum;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Core.Entities.Concrete
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            this.Status = Status.Active;
            this.CreatedDate = DateTime.Now;
            this.CreatedADUserName = WindowsIdentity.GetCurrent().Name;
            this.CreatedComputerName = Environment.MachineName;
            this.CreatedIp = "123";
            this.CreatedBy = 1;
            this.IsDeleted = false;
        }

        public int Id { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedComputerName { get; set; }
        public string CreatedIp { get; set; }
        public string CreatedADUserName { get; set; }
        public long? CreatedBy { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public long? DeletedBy { get; set; }

    }
}
