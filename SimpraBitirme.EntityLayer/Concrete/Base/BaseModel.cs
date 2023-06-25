using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.EntityLayer.Concrete.Base
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsDelete { get; set; }
    }
}
