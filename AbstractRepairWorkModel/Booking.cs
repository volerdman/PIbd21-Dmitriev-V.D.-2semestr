using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairWorkModel
{
    /// <summary>
    /// Заказ клиента
    /// </summary>
    public class Booking
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int RepairId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public BookingStatus Status { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ImplementDate { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Repair Repair { get; set; }
    }
}
