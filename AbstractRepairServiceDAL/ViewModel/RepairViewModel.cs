using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairServiceDAL.ViewModel
{
    public class RepairViewModel
    {
        public int Id { get; set; }

        public string RepairName { get; set; }

        public decimal Cost { get; set; }

        public List<MaterialRepairViewModel> MaterialRepair { get; set; }
    }
}
