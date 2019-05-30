using AbstractRepairServiceDAL.Attributies;
using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.ViewModel;
using System.Collections.Generic;

namespace AbstractRepairServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с клиентами")]
    public interface ICustomerService
    {
        [CustomMethod("Метод получения списка клиентов")]
        List<CustomerViewModel> ListGet();

        [CustomMethod("Метод получения клиента по id")]
        CustomerViewModel ElementGet(int id);

        [CustomMethod("Метод добавления клиента")]
        void AddElement(CustomerBindingModel model);

        [CustomMethod("Метод изменения данных по клиенту")]
        void UpdateElement(CustomerBindingModel model);

        [CustomMethod("Метод удаления клиента")]
        void DeleteElement(int id);
    }
}
