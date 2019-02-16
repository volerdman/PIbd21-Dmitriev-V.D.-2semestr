using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairServiceDAL.ViewModel
{
    public class MaterialRepairViewModel
    {
        public int Id { get; set; }

        public int RepairId { get; set; }

        public int MaterialId { get; set; }

        public string MaterialName { get; set; }

        public int Count { get; set; }
    }
}
