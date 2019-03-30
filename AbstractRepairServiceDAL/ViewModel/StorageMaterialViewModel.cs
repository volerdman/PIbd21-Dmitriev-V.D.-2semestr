using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AbstractRepairServiceDAL.ViewModel
{
    public class StorageMaterialViewModel
    {
        public int Id { get; set; }

        public int StorageId { get; set; }

        public int MaterialId { get; set; }

        [DisplayName("Название материала")]
        public string MaterialName { get; set; }

        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}
