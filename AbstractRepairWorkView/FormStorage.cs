﻿using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.Interfaces;
using AbstractRepairServiceDAL.ViewModel;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace AbstractRepairWorkView
{
    public partial class FormStorage : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly IStorageService service;
        private int? id;
        public FormStorage(IStorageService service)
        {
            InitializeComponent();
            this.service = service;
        }
        private void FormStorage_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    StorageViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.StorageName;
                        dataGridView.DataSource = view.StorageMaterials;
                        dataGridView.Columns[0].Visible = true;
                        dataGridView.Columns[1].Visible = true;
                        dataGridView.Columns[2].Visible = true;
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
                    service.UpdateElement(new StorageBindingModel
                    {
                        Id = id.Value,
                        StorageName = textBoxName.Text
                    });
                }
                else
                {
                    service.AddElement(new StorageBindingModel
                    {
                        StorageName = textBoxName.Text
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
