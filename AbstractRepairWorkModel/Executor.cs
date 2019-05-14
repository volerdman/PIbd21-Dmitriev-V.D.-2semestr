using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractRepairWorkModel
{
    /// <summary>
    /// Исполнитель, выполняющий заказы клиентов
    /// </summary>
    public class Executor
    {
        public int Id { get; set; }

        [Required]
        public string ExecutorFIO { get; set; }

        [ForeignKey("ExecutorId")]
        public virtual List<Booking> Bookings { get; set; }
    }
}
