﻿using Password_MAnager.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Password_MAnager
{
    public partial class MainForm : Form
    {
        EFcontext context;
        User user;
        StartForm form;
        List<ExtraField> ExtraFieldList;

        public MainForm(StartForm form, User user)
        {
            Label label = new Label();
            label.Text = "Text";
            label.Location = new System.Drawing.Point(200, 25);
            label.Size = new System.Drawing.Size(70, 20);
            label.Show();

            this.form = form;
            this.user = user;
            InitializeComponent();
            context = new EFcontext();
            ExtraFieldList = new List<ExtraField>();

            for (int i = 0; i < 100; i++)
            {
                treeView1.Nodes.Add("test");
            }

            foreach (var item in context.Sections)
            {
                SectionComboBox1.Items.Add(item.Name);

            }
            if (SectionComboBox1.Items.Count != 0)
            {
                SectionComboBox1.SelectedItem = SectionComboBox1.Items[SectionComboBox1.Items.Count - 1];
            }
            foreach (var item in context.Services)
            {
                ServiceComboBox2.Items.Add(item.Name);
            }
            if (ServiceComboBox2.Items.Count != 0)
            {
                ServiceComboBox2.SelectedItem = ServiceComboBox2.Items[ServiceComboBox2.Items.Count - 1];
            }

            CreateServicetextBox1.Visible = false;

            AddVisibleTrue();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text == "")
            {
                return;
            }
            context.Sections.Add(
                new Section
                {
                    Name = toolStripTextBox1.Text,
                    UserId = user.Id
                });
            context.SaveChanges();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                ServiceComboBox2.Visible = false;
                CreateServicetextBox1.Visible = true;

            }
            else
            {
                ServiceComboBox2.Visible = true;
                CreateServicetextBox1.Visible = false;
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        void AddVisibleTrue()
        {
            checkAdd = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            checkBox1.Visible = true;
            SectionComboBox1.Visible = true;
            ServiceComboBox2.Visible = true;
            CreateServicetextBox1.Visible = false;
            PasswordtextBox2.Visible = true;
            button4.Visible = true;
            label4.Visible = false;
            label5.Visible = false;
            label13.Visible = false;
            label6.Visible = false;
            label12.Visible = false;
            label7.Visible = false;
            label11.Visible = false;
            label8.Visible = false;
            label10.Visible = false;
            label9.Visible = false;
            NameExtraField.Visible = false;
            ValueExtraField.Visible = false;

        }

        bool checkAdd = false;
        private void button1_Click(object sender, EventArgs e)
        {
            AddVisibleTrue();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            if(NameExtraField.Visible == false)
            {
                NameExtraField.Visible = true;
                ValueExtraField.Visible = true;
            }
            if(NameExtraField.Text == "" || ValueExtraField.Text == "")
            {
                return;
            }
            ExtraFieldList.Add(new ExtraField
            {
                Name = NameExtraField.Text,
                Value = ValueExtraField.Text
            });
           
            if (label4.Visible == false)
            {
                label4.Text = NameExtraField.Text;
                label5.Text = ValueExtraField.Text;
                label4.Visible = true;
                label5.Visible = true;
            }
            else if(label13.Visible == false)
            {
                label13.Text = NameExtraField.Text;
                label6.Text = ValueExtraField.Text;
                label13.Visible = true;
                label6.Visible = true;
            }
            else if (label12.Visible == false)
            {
                label12.Text = NameExtraField.Text;
                label7.Text = ValueExtraField.Text;
                label12.Visible = true;
                label7.Visible = true;
            }
            else if (label11.Visible == false)
            {
                label11.Text = NameExtraField.Text;
                label8.Text = ValueExtraField.Text;
                label11.Visible = true;
                label8.Visible = true;
            }
            else if (label10.Visible == false)
            {
                label10.Text = NameExtraField.Text;
                label9.Text = ValueExtraField.Text;
                label10.Visible = true;
                label9.Visible = true;
                button4.Enabled = false;
                button5.Enabled = false;
                NameExtraField.Text = "";
                ValueExtraField.Text = "";
                NameExtraField.Visible = false;
                ValueExtraField.Visible = false;
                return;
            }
            NameExtraField.Text = "";
            ValueExtraField.Text = "";
            ValueExtraField.Location = new Point(ValueExtraField.Location.X, ValueExtraField.Location.Y + 39);
            NameExtraField.Location = new Point(NameExtraField.Location.X, NameExtraField.Location.Y + 39);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            form.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            NameExtraField.Text = "";
            ValueExtraField.Text = "";
            NameExtraField.Visible = false;
            ValueExtraField.Visible = false;
        }
    }
}
