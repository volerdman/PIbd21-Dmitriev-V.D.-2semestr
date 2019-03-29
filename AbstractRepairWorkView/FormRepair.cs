using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.Interfaces;
using AbstractRepairServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace AbstractRepairWorkView
{
    public partial class FormRepair : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IRepairService service;

        private int? id;

        private List<MaterialRepairViewModel> repairMaterial;

        public FormRepair(IRepairService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormRepair_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    RepairViewModel view = service.ElementGet(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.RepairName;
                        textBoxCost.Text = view.Cost.ToString();
                        repairMaterial = view.MaterialRepair;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
            else
            {
                repairMaterial = new List<MaterialRepairViewModel>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (repairMaterial != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = repairMaterial;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormMaterialRepair>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.RepairId = id.Value;
                    }
                    repairMaterial.Add(form.Model);
                }
                LoadData();
            }
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormMaterialRepair>();
                form.Model =
               repairMaterial[dataGridView.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    repairMaterial[dataGridView.SelectedRows[0].Cells[0].RowIndex] =
                   form.Model;
                    LoadData();
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        repairMaterial.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxCost.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (repairMaterial == null || repairMaterial.Count == 0)
            {
                MessageBox.Show("Заполните материалы", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
               List<MaterialRepairBindingModel> repairMaterialBM = new
               List<MaterialRepairBindingModel> ();
                for (int i = 0; i < repairMaterial.Count; ++i)
                {
                    repairMaterialBM.Add(new MaterialRepairBindingModel
                    {
                        Id = repairMaterial[i].Id,
                        RepairId = repairMaterial[i].RepairId,
                        MaterialId = repairMaterial[i].MaterialId,
                        Count = repairMaterial[i].Count
                    });
                }
                if (id.HasValue)
                {
                    service.UpdateElement(new RepairBindingModel
                    {
                        Id = id.Value,
                        RepairName = textBoxName.Text,
                        Cost = Convert.ToInt32(textBoxCost.Text),
                        MaterialRepair = repairMaterialBM
                    });
                }
                else
                {
                    service.AddElement(new RepairBindingModel
                    {
                        RepairName = textBoxName.Text,
                        Cost = Convert.ToInt32(textBoxCost.Text),
                        MaterialRepair = repairMaterialBM
                    });
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
