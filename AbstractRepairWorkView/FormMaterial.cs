using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.ViewModel;
using System;
using System.Windows.Forms;

namespace AbstractRepairWorkView
{
    public partial class FormMaterial : Form
    {
        public int Id { set { id = value; } }
        private int? id;
        public FormMaterial()
        {
            InitializeComponent();
        }
        private void FormMaterial_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    MaterialViewModel material =
                   APICustomer.GetRequest<MaterialViewModel>("api/Material/Get/" + id.Value);
                    textBoxName.Text = material.MaterialName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    APICustomer.PostRequest<MaterialBindingModel,
                   bool>("api/Material/UpdElement", new MaterialBindingModel
                   {
                       Id = id.Value,
                       MaterialName = textBoxName.Text
                   });
                }
                else
                {
                    APICustomer.PostRequest<MaterialBindingModel,
                   bool>("api/Material/AddElement", new MaterialBindingModel
                   {
                       MaterialName = textBoxName.Text
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
