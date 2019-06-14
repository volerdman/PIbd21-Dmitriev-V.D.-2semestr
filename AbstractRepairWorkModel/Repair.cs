using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairWorkModel
{
    /// <summary>
    /// Услуга, проводимая в рамках ремонтных работ
    /// </summary>
    public class Repair
    {
        public int Id { get; set; }

        public int MaterialID { get; set; }

        [Required]
        public string RepairName { get; set; }

        public virtual List<Booking> Bookings { get; set; }

        [Required]
        public decimal Cost { get; set; }
    }
}
