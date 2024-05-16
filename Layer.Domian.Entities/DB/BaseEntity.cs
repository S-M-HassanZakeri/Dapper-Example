using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Domian.Entities.DB
{
    public class BaseEntity 
    {
        [Key]
        public int ID { get; set; }
        public DateTime RegesterDate { get; set; } = DateTime.Now;
        public bool IsDelete { get; set; } = false;
    }
}
