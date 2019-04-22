using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairServiceDAL.BindingModel
{
    [DataContract]
    public class RepairBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string RepairName { get; set; }

        [DataMember]
        public decimal Cost { get; set; }

        [DataMember]
        public List<MaterialRepairBindingModel> MaterialRepair { get; set; }
    }
}
