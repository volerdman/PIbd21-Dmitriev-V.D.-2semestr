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
    public class RepairServiceList:IRepairService
    {
        private DataSingletonList source;

        public RepairServiceList()
        {
            source = DataSingletonList.GetInstance();
        }

        public List<RepairViewModel> ListGet()
        {
            List<RepairViewModel> result = source.Repairs.Select(rec => new RepairViewModel
            {
                Id = rec.Id,
                RepairName = rec.RepairName,
                Cost = rec.Cost,
                MaterialRepair = source.MaterialRepairs
                    .Where(recMR => recMR.RepairId == rec.Id)
                    .Select(recMR => new MaterialRepairViewModel
                    {
                        Id = recMR.Id,
                        RepairId = recMR.RepairId,
                        MaterialId = recMR.MaterialId,
                        MaterialName = source.Materials.FirstOrDefault(recM =>
                        recM.Id == recMR.MaterialId)?.MaterialName,
                        Count = recMR.Count
                    }).ToList()
            }).ToList();
            return result;
        }

        public RepairViewModel ElementGet(int id)
        {
            Repair element = source.Repairs.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new RepairViewModel
                {
                    Id = element.Id,
                    RepairName = element.RepairName,
                    Cost = element.Cost,
                    MaterialRepair = source.MaterialRepairs
                .Where(recMR => recMR.RepairId == element.Id)
                .Select(recMR => new MaterialRepairViewModel
                {
                    Id = recMR.Id,
                    RepairId = recMR.RepairId,
                    MaterialId = recMR.MaterialId,
                    MaterialName = source.Materials.FirstOrDefault(recC => recC.Id == recMR.MaterialId)?.MaterialName,
                    Count = recMR.Count
                })
               .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(RepairBindingModel model)
        {
            Repair element = source.Repairs.FirstOrDefault(rec => rec.RepairName == model.RepairName);
            if (element != null)
            {
                throw new Exception("Уже есть услуга с таким названием");
            }
            int maxId = source.Repairs.Count > 0 ? source.Repairs.Max(rec => rec.Id) :
           0;
            source.Repairs.Add(new Repair
            {
                Id = maxId + 1,
                RepairName = model.RepairName,
                Cost = model.Cost
            });
            // компоненты для изделия
            int maxPCId = source.MaterialRepairs.Count > 0 ?
           source.MaterialRepairs.Max(rec => rec.Id) : 0;
            // убираем дубли по компонентам
            var groupMaterials = model.MaterialRepair
            .GroupBy(rec => rec.MaterialId)
           .Select(rec => new
           {
               MaterialId = rec.Key,
               Count = rec.Sum(r => r.Count)
           });
            // добавляем компоненты
            foreach (var groupMaterial in groupMaterials)
            {
                source.MaterialRepairs.Add(new MaterialRepair
                {
                    Id = ++maxPCId,
                    RepairId = maxId + 1,
                    MaterialId = groupMaterial.MaterialId,
                    Count = groupMaterial.Count
                });
            }
        }

        public void UpdateElement(RepairBindingModel model)
        {
            Repair element = source.Repairs.FirstOrDefault(rec => rec.RepairName ==
           model.RepairName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            element = source.Repairs.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.RepairName = model.RepairName;
            element.Cost = model.Cost;
            int maxMRId = source.MaterialRepairs.Count > 0 ?
           source.MaterialRepairs.Max(rec => rec.Id) : 0;
            // обновляем существуюущие компоненты
            var matIds = model.MaterialRepair.Select(rec =>
           rec.MaterialId).Distinct();
            var updateMaterials = source.MaterialRepairs.Where(rec => rec.RepairId ==
           model.Id && matIds.Contains(rec.MaterialId));
            foreach (var updateMaterial in updateMaterials)
            {
                updateMaterial.Count = model.MaterialRepair.FirstOrDefault(rec =>
               rec.Id == updateMaterial.Id).Count;
            }
            source.MaterialRepairs.RemoveAll(rec => rec.RepairId == model.Id &&
           !matIds.Contains(rec.MaterialId));
            // новые записи
            var groupMaterials = model.MaterialRepair
            .Where(rec => rec.Id == 0)
           .GroupBy(rec => rec.MaterialId)
           .Select(rec => new
           {
               MaterialId = rec.Key,
               Count = rec.Sum(r => r.Count)
           });
            foreach (var groupMaterial in groupMaterials)
            {
                MaterialRepair elementMR = source.MaterialRepairs.FirstOrDefault(rec
               => rec.RepairId == model.Id && rec.MaterialId == groupMaterial.MaterialId);
                if (elementMR != null)
                {
                    elementMR.Count += groupMaterial.Count;
                }
                else
                {
                    source.MaterialRepairs.Add(new MaterialRepair
                    {
                        Id = ++maxMRId,
                        RepairId = model.Id,
                        MaterialId = groupMaterial.MaterialId,
                        Count = groupMaterial.Count
                    });
                }
            }
        }

        public void DeleteElement(int id)
        {
            Repair element = source.Repairs.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                // удаяем записи по компонентам при удалении изделия
                source.MaterialRepairs.RemoveAll(rec => rec.RepairId == id);
                source.Repairs.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}