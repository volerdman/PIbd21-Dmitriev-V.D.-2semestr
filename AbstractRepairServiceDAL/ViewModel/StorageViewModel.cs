using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairServiceDAL.ViewModel
{
    [DataContract]
    public class StorageViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("Название склада")]

        public string StorageName { get; set; }

        [DataMember]
        public List<StorageMaterialViewModel> StorageMaterials { get; set; }
    }
}
