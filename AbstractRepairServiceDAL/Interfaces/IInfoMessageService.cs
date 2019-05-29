using AbstractRepairServiceDAL.Attributies;
using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.ViewModel;
using System.Collections.Generic;

namespace AbstractRepairServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с сообщениями")]
    public interface IInfoMessageService
    {
        [CustomMethod("Метод получения списка писем")]
        List<InfoMessageViewModel> GetList();

        [CustomMethod("Метод получения письма по id")]
        InfoMessageViewModel GetElement(int id);

        [CustomMethod("Метод добавления письма")]
        void AddElement(InfoMessageBindingModel model);
    }
}
