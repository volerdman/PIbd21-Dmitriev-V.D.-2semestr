using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbstractRepairWorkView
{
    public partial class FormExecutor : Form
    {
        public int Id { set { id = value; } }
        private int? id;
        public FormExecutor()
        {
            InitializeComponent();
        }
        private void FormExecutor_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ExecutorViewModel client =
                   APICustomer.GetRequest<ExecutorViewModel>("api/Executor/Get/" + id.Value);
                    textBoxFIO.Text = client.ExecutorFIO;
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
            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    APICustomer.PostRequest<ExecutorBindingModel,
                   bool>("api/Executor/UpdElement", new ExecutorBindingModel
                   {
                       Id = id.Value,
                       ExecutorFIO = textBoxFIO.Text
                   });
                }
                else
                {
                    APICustomer.PostRequest<ExecutorBindingModel,
                   bool>("api/Executor/AddElement", new ExecutorBindingModel
                   {
                       ExecutorFIO = textBoxFIO.Text
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
