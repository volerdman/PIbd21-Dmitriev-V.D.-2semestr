using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairServiceDAL.ViewModel
{
    public class StorageViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название склада")]

        public string StorageName { get; set; }

        public List<StorageMaterialViewModel> StorageMaterials { get; set; }
    }
}
