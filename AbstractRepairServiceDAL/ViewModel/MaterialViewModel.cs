using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairServiceDAL.ViewModel
{
    [DataContract]
    public class MaterialViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string MaterialName { get; set; }
    }
}
