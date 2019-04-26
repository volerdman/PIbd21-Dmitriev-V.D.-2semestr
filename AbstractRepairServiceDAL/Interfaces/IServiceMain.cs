using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.ViewModel;
using System.Collections.Generic;

namespace AbstractRepairServiceDAL.Interfaces
{
    public interface IServiceMain
    {
        List<BookingViewModel> ListGet();

        List<BookingViewModel> GetFreeBookings();

        void CreateBooking(BookingBindingModel model);

        void TakeBookingInWork(BookingBindingModel model);

        void FinishBooking(BookingBindingModel model);

        void PayBooking(BookingBindingModel model);

        void PutMaterialOnStorage(StorageMaterialBindingModel model);
    }
}
