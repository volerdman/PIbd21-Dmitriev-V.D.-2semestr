using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace AbstractRepairServiceDAL.ViewModel
{
    [DataContract]
    public class StorageMaterialViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int StorageId { get; set; }

        [DataMember]
        public int MaterialId { get; set; }

        [DataMember]
        [DisplayName("Название материала")]
        public string MaterialName { get; set; }

        [DataMember]
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}
