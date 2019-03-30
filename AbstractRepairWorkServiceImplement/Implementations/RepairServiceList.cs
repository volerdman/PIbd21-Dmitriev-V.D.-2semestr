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
            List<RepairViewModel> result = new List<RepairViewModel>();
            for (int i = 0; i < source.Repairs.Count; ++i)
            {
                // требуется дополнительно получить список материалов для услуги и их количество
                List<MaterialRepairViewModel> productMaterials = new List<MaterialRepairViewModel>();
                for (int j = 0; j < source.MaterialRepairs.Count; ++j)
                {
                    if (source.MaterialRepairs[j].RepairId == source.Repairs[i].Id)
                    {
                        string materialName = string.Empty;
                        for (int k = 0; k < source.Materials.Count; ++k)
                        {
                            if (source.MaterialRepairs[j].MaterialId ==
                           source.Materials[k].Id)
                            {
                                materialName = source.Materials[k].MaterialName;
                                break;
                            }
                        }
                        productMaterials.Add(new MaterialRepairViewModel
                        {
                            Id = source.MaterialRepairs[j].Id,
                            RepairId = source.MaterialRepairs[j].RepairId,
                            MaterialId = source.MaterialRepairs[j].MaterialId,
                            MaterialName = materialName,
                            Count = source.MaterialRepairs[j].Count
                        });
                    }
                }
                result.Add(new RepairViewModel
                {
                    Id = source.Repairs[i].Id,
                    RepairName = source.Repairs[i].RepairName,
                    Cost = source.Repairs[i].Cost,
                    MaterialRepair = productMaterials
                });
            }
            return result;
        }
        public RepairViewModel ElementGet(int id)
        {
            for (int i = 0; i < source.Repairs.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
                List<MaterialRepairViewModel> productMaterials = new List<MaterialRepairViewModel>();
                for (int j = 0; j < source.MaterialRepairs.Count; ++j)
                {
                    if (source.MaterialRepairs[j].RepairId == source.Repairs[i].Id)
                    {
                        string materialName = string.Empty;
                        for (int k = 0; k < source.Materials.Count; ++k)
                        {
                            if (source.MaterialRepairs[j].MaterialId ==
                           source.Materials[k].Id)
                            {
                                materialName = source.Materials[k].MaterialName;
                                break;
                            }
                        }
                        productMaterials.Add(new MaterialRepairViewModel
                        {
                            Id = source.MaterialRepairs[j].Id,
                            RepairId = source.MaterialRepairs[j].RepairId,
                            MaterialId = source.MaterialRepairs[j].MaterialId,
                            MaterialName = materialName,
                            Count = source.MaterialRepairs[j].Count
                        });
                    }
                }
                if (source.Repairs[i].Id == id)
                {
                    return new RepairViewModel
                    {
                        Id = source.Repairs[i].Id,
                        RepairName = source.Repairs[i].RepairName,
                        Cost = source.Repairs[i].Cost,
                        MaterialRepair = productMaterials
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(RepairBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Repairs.Count; ++i)
            {
                if (source.Repairs[i].Id > maxId)
                {
                    maxId = source.Repairs[i].Id;
                }
                if (source.Repairs[i].RepairName == model.RepairName)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            source.Repairs.Add(new Repair
            {
                Id = maxId + 1,
                RepairName = model.RepairName,
                Cost = model.Cost
            });
            // компоненты для изделия
            int maxPCId = 0;
            for (int i = 0; i < source.MaterialRepairs.Count; ++i)
            {
                if (source.MaterialRepairs[i].Id > maxPCId)
                {
                    maxPCId = source.MaterialRepairs[i].Id;
                }
            }
            // убираем дубли по компонентам
            for (int i = 0; i < model.MaterialRepair.Count; ++i)
            {
                for (int j = 1; j < model.MaterialRepair.Count; ++j)
                {
                    if (model.MaterialRepair[i].MaterialId ==
                    model.MaterialRepair[j].MaterialId)
                    {
                        model.MaterialRepair[i].Count +=
                        model.MaterialRepair[j].Count;
                        model.MaterialRepair.RemoveAt(j--);
                    }
                }
            }
            // добавляем компоненты
            for (int i = 0; i < model.MaterialRepair.Count; ++i)
            {
                source.MaterialRepairs.Add(new MaterialRepair
                {
                    Id = ++maxPCId,
                    RepairId = maxId + 1,
                    MaterialId = model.MaterialRepair[i].MaterialId,
                    Count = model.MaterialRepair[i].Count
                });
            }
        }
        public void UpdateElement(RepairBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Repairs.Count; ++i)
            {
                if (source.Repairs[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Repairs[i].RepairName == model.RepairName &&
                source.Repairs[i].Id != model.Id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Repairs[index].RepairName = model.RepairName;
            source.Repairs[index].Cost = model.Cost;
            int maxPCId = 0;
            for (int i = 0; i < source.MaterialRepairs.Count; ++i)
            {
                if (source.MaterialRepairs[i].Id > maxPCId)
                {
                    maxPCId = source.MaterialRepairs[i].Id;
                }
            }
            // обновляем существуюущие материалы
            for (int i = 0; i < source.MaterialRepairs.Count; ++i)
            {
                if (source.MaterialRepairs[i].RepairId == model.Id)
                {
                    bool flag = true;
                    for (int j = 0; j < model.MaterialRepair.Count; ++j)
                    {
                        // если встретили, то изменяем количество
                        if (source.MaterialRepairs[i].Id == model.MaterialRepair[j].Id)
                        {
                            source.MaterialRepairs[i].Count = model.MaterialRepair[j].Count;
                            flag = false;
                            break;
                        }
                    }
                    // если не встретили, то удаляем
                    if (flag)
                    {
                        source.MaterialRepairs.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            for (int i = 0; i < model.MaterialRepair.Count; ++i)
            {
                if (model.MaterialRepair[i].Id == 0)
                {
                    // ищем дубли
                    for (int j = 0; j < source.MaterialRepairs.Count; ++j)
                    {
                        if (source.MaterialRepairs[j].RepairId == model.Id &&
                        source.MaterialRepairs[j].MaterialId ==
                       model.MaterialRepair[i].MaterialId)
                        {
                           source.MaterialRepairs[j].Count +=
                           model.MaterialRepair[i].Count;
                           model.MaterialRepair[i].Id =
                           source.MaterialRepairs[j].Id;
                           break;
                        }
                    }
                    // если не нашли дубли, то новая запись
                    if (model.MaterialRepair[i].Id == 0)
                    {
                        source.MaterialRepairs.Add(new MaterialRepair
                        {
                            Id = ++maxPCId,
                            RepairId = model.Id,
                            MaterialId = model.MaterialRepair[i].MaterialId,
                            Count = model.MaterialRepair[i].Count
                        });
                    }
                }
            }
        }
        public void DeleteElement(int id)
        {
            for (int i = 0; i < source.MaterialRepairs.Count; ++i)
            {
                if (source.MaterialRepairs[i].RepairId == id)
                {
                    source.MaterialRepairs.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Repairs.Count; ++i)
            {
                if (source.Repairs[i].Id == id)
                {
                    source.Repairs.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}