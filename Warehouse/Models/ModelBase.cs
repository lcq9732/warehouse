using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Models
{
    public class ModelBase
    {
        public ModelBase()
        {
            CreatedBy = "";
            UpdatedBy = "";
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
            Id = 0;
        }
        public virtual int Id { get; set; }

        public virtual string CreatedBy { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual string UpdatedBy { get; set; }
        public virtual DateTime UpdatedOn { get; set; }
    }
}
