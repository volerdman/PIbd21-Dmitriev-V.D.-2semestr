using AbstractRepairServiceDAL.Attributies;
using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.ViewModel;
using System.Collections.Generic;

namespace AbstractRepairServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с отчетами")]
    public interface IReportService
    {
        [CustomMethod("Метод сохранения цены услуги")]
        void SaveRepairPrice(ReportBindingModel model);

        [CustomMethod("Метод получения списка загруженных складов")]
        List<StorageLoadViewModel> GetStorageLoad();

        [CustomMethod("Метод сохранения загруженных складов")]
        void SaveStorageLoad(ReportBindingModel model);

        [CustomMethod("Метод получения списка заказов")]
        List<CustomerBookingModel> GetCustomerBooking(ReportBindingModel model);

        [CustomMethod("Метод сохранения заказа")]
        void SaveCustomerBooking(ReportBindingModel model);
    }
}
