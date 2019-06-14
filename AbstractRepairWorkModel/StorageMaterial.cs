using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairWorkModel
{
    /// <summary>
    /// Сколько материалов хранится на складе
    /// </summary>
    public class StorageMaterial
    {
        public int Id { get; set; }

        public int StorageId { get; set; }

        public int MaterialId { get; set; }

        public int Count { get; set; }

        public virtual Storage Storage { get; set; }

        public virtual Material Material { get; set; }
    }
}
