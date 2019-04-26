using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairServiceDAL.ViewModel
{
    [DataContract]
    public class RepairViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string RepairName { get; set; }

        [DataMember]
        public decimal Cost { get; set; }

        [DataMember]
        public List<MaterialRepairViewModel> MaterialRepair { get; set; }
    }
}
