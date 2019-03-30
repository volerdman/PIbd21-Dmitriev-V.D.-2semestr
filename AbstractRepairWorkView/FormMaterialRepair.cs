using AbstractRepairServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using AbstractRepairServiceDAL.Interfaces;
using Unity.Attributes;

namespace AbstractRepairWorkView
{
    public partial class FormMaterialRepair : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public MaterialRepairViewModel Model
        {
            set { model = value; }
            get
            {
                return model;
            }
        }

        private readonly IMaterialService service;

        private MaterialRepairViewModel model;

        public FormMaterialRepair(IMaterialService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormMaterialRepair_Load(object sender, EventArgs e)
        {
            try
            {
                List<MaterialViewModel> list = service.ListGet();
                if (list != null)
                {
                    comboBoxMaterial.DisplayMember = "MaterialName";
                    comboBoxMaterial.ValueMember = "Id";
                    comboBoxMaterial.DataSource = list;
                    comboBoxMaterial.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
            if (model != null)
            {
                comboBoxMaterial.Enabled = false;
                comboBoxMaterial.SelectedValue = model.MaterialId;
                textBoxAmount.Text = model.Count.ToString();
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxAmount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxMaterial.SelectedValue == null)
            {
                MessageBox.Show("Выберите материал", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new MaterialRepairViewModel
                    {
                        MaterialId = Convert.ToInt32(comboBoxMaterial.SelectedValue),
                        MaterialName = comboBoxMaterial.Text,
                        Count = Convert.ToInt32(textBoxAmount.Text)
                    };
                }
                else
                {
                    model.Count = Convert.ToInt32(textBoxAmount.Text);
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
