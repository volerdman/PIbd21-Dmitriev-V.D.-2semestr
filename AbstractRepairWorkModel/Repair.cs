using System;
using System.Collections.Generic;
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

        public string RepairName { get; set; }

        public decimal Cost { get; set; }
    }
}
