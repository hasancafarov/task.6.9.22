using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTask.Core.Entities
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime? CreateDate { get; set; }=DateTime.Now;
        //public virtual int State { get; set; }
    }
}
