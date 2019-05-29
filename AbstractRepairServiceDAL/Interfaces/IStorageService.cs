using AbstractRepairServiceDAL.Attributies;
using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.ViewModel;
using System.Collections.Generic;

namespace AbstractRepairServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы со складами")]
    public interface IStorageService
    {
        [CustomMethod("Метод получения списка складов")]
        List<StorageViewModel> GetList();

        [CustomMethod("Метод получения склада по id")]
        StorageViewModel GetElement(int id);

        [CustomMethod("Метод добавления склада")]
        void AddElement(StorageBindingModel model);

        [CustomMethod("Метод обновления информации по складу")]
        void UpdateElement(StorageBindingModel model);

        [CustomMethod("Метод удаления склада")]
        void DeleteElement(int id);
    }
}
