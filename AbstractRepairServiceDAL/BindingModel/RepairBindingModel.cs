using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairServiceDAL.BindingModel
{
    public class RepairBindingModel
    {
        public int Id { get; set; }

        public string RepairName { get; set; }

        public decimal Cost { get; set; }

        public List<MaterialRepairBindingModel> MaterialRepair { get; set; }
    }
}
