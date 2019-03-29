using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairWorkModel
{
    /// <summary>
    /// Хранилиище компонентов в магазине
    /// </summary>
    public class Storage
    {
        public int Id { get; set; }

        [Required]
        public string StorageName { get; set; }

        public virtual List<StorageMaterial> StorageMaterials { get; set; }
    }
}
