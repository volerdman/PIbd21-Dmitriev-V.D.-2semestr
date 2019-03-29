using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairWorkModel
{
    /// <summary>
    /// Сколько материалов требуется для предоставления услуги
    /// </summary>
    public class MaterialRepair
    {
        public int Id { get; set; }

        public int RepairId { get; set; }

        public int MaterialId { get; set; }

        public int Count { get; set; }

        public virtual Material Material { get; set; }
    }
}
