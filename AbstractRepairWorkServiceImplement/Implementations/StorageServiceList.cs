using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.Interfaces;
using AbstractRepairServiceDAL.ViewModel;
using AbstractRepairWorkModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairWorkServiceImplement.Implementations
{
    public class StorageServiceList : IStorageService
    {
        private DataSingletonList source;

        public StorageServiceList()
        {
            source = DataSingletonList.GetInstance();
        }

        public List<StorageViewModel> GetList()
        {
            List<StorageViewModel> result = source.Storages.Select(rec => new StorageViewModel
            {
                Id = rec.Id,
                StorageName = rec.StorageName,
                StorageMaterials = source.StorageMaterials
                    .Where(recMR => recMR.StorageId == rec.Id)
                    .Select(recMR => new StorageMaterialViewModel
                    {
                        Id = recMR.Id,
                        StorageId = recMR.StorageId,
                        MaterialId = recMR.MaterialId,
                        MaterialName = source.Materials
                           .FirstOrDefault(recC => recC.Id == recMR.MaterialId)?.MaterialName,
                        Count = recMR.Count
                    }).ToList()
            }).ToList();
            return result;
        }

        public StorageViewModel GetElement(int id)
        {
            Storage element = source.Storages.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new StorageViewModel
                {
                    Id = element.Id,
                    StorageName = element.StorageName,
                    StorageMaterials = source.StorageMaterials
                        .Where(recMR => recMR.StorageId == element.Id)
                        .Select(recMR => new StorageMaterialViewModel
                        {
                            Id = recMR.Id,
                            StorageId = recMR.StorageId,
                            MaterialId = recMR.MaterialId,
                            MaterialName = source.Materials
                               .FirstOrDefault(recC => recC.Id == recMR.MaterialId)?.MaterialName,
                            Count = recMR.Count
                        }).ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(StorageBindingModel model)
        {
            Storage element = source.Storages.FirstOrDefault(rec => rec.StorageName == model.StorageName);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            int maxId = source.Storages.Count > 0 ? source.Storages.Max(rec => rec.Id) : 0;
            source.Storages.Add(new Storage
            {
                Id = maxId + 1,
                StorageName = model.StorageName
            });
        }

        public void UpdateElement(StorageBindingModel model)
        {
            Storage element = source.Storages.FirstOrDefault(rec =>
            rec.StorageName == model.StorageName && rec.Id !=
           model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            element = source.Storages.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.StorageName = model.StorageName;
        }

        public void DeleteElement(int id)
        {
            Storage element = source.Storages.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                // при удалении удаляем все записи о материалах на удаляемом складе
                source.StorageMaterials.RemoveAll(rec => rec.StorageId == id);
                source.Storages.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
