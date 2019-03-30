using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairServiceDAL.BindingModel
{
    public class MaterialRepairBindingModel
    {
        public int Id { get; set; }

        public int RepairId { get; set; }

        public int MaterialId { get; set; }

        public string RepairName { get; set; }

        public int Count { get; set; }
    }
}
