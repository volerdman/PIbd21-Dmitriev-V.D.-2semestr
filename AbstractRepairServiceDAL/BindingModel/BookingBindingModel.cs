using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairServiceDAL.BindingModel
{
    public class BookingBindingModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int RepairId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }
    }
}
