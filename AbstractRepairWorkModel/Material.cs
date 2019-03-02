using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairWorkModel
{
    /// <summary>
    /// Материал, используемый для ремонтных работ
    /// </summary>
    public class Material
    {
        public int Id { get; set; }

        [Required]
        public string MaterialName { get; set; }
    }
}
