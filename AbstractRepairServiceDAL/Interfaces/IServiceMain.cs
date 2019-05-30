using AbstractRepairServiceDAL.Attributies;
using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.ViewModel;
using System.Collections.Generic;

namespace AbstractRepairServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с заказами")]
    public interface IServiceMain
    {
        [CustomMethod("Метод получения списка заказов")]
        List<BookingViewModel> ListGet();

        [CustomMethod("Метод получения списка свободных заказов")]
        List<BookingViewModel> GetFreeBookings();

        [CustomMethod("Метод создания заказа")]
        void CreateBooking(BookingBindingModel model);

        [CustomMethod("Метод отправки заказа в работу")]
        void TakeBookingInWork(BookingBindingModel model);

        [CustomMethod("Метод завершения работы над заказом")]
        void FinishBooking(BookingBindingModel model);

        [CustomMethod("Метод оплаты заказа")]
        void PayBooking(BookingBindingModel model);

        [CustomMethod("Метод добавления материалов на склад")]
        void PutMaterialOnStorage(StorageMaterialBindingModel model);
    }
}
