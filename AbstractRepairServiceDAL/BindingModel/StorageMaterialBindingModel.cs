using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairServiceDAL.BindingModel
{
    public class StorageMaterialBindingModel
    {
        public int Id { get; set; }

        public int StorageId { get; set; }

        public int MaterialId { get; set; }

        public int Count { get; set; }
    }
}
