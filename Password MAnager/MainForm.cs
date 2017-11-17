using Password_MAnager.Entity;
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
        Service service;
        List<ExtraField> ExtraFieldList;

        public MainForm(StartForm form, User user)
        {


            this.form = form;
            this.user = user;
            InitializeComponent();
            AddVisibleFalse();

            context = new EFcontext();
            ExtraFieldList = new List<ExtraField>();

            for (int i = 0; i < 100; i++)
            {
                treeView1.Nodes.Add("test");
            }

            updateSectionComboBox();
            updateServiceComboBox();

            CreateServicetextBox1.Visible = false;
        }

        void updateSectionComboBox()
        {
            SectionComboBox1.Items.Clear();
            foreach (var item in context.Sections)
            {
                if (item.UserId == user.Id)
                {
                    SectionComboBox1.Items.Add(item.Name);
                }

            }
            if (SectionComboBox1.Items.Count != 0)
            {
                SectionComboBox1.SelectedItem = SectionComboBox1.Items[SectionComboBox1.Items.Count - 1];
            }
        }
        void updateServiceComboBox()
        {
            ServiceComboBox2.Items.Clear();
            foreach (var item in context.Services)
            {
                if (item.section.UserId == user.Id)
                {
                    ServiceComboBox2.Items.Add(item.Name);
                }
            }
            if (ServiceComboBox2.Items.Count != 0)
            {
                ServiceComboBox2.SelectedItem = ServiceComboBox2.Items[ServiceComboBox2.Items.Count - 1];
            }
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
            foreach (var item in context.Sections)
            {
                if(item.Name == toolStripTextBox1.Text && item.UserId == user.Id)
                {
                    MessageBox.Show("This Section exist!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            context.Sections.Add(
                new Section
                {
                    Name = toolStripTextBox1.Text,
                    UserId = user.Id
                });
            context.SaveChanges();

            updateSectionComboBox();
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
            button4.Visible = true;
            button5.Visible = true;

            button1.Enabled = true;
        }

        void AddVisibleFalse()
        {

            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            checkBox1.Visible = false;
            SectionComboBox1.Visible = false;
            ServiceComboBox2.Visible = false;
            CreateServicetextBox1.Visible = false;
            PasswordtextBox2.Visible = false;
            button4.Visible = false;
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
            button4.Visible = false;
            button5.Visible = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Save")
            {
                Account account = new Account
                {
                    Password = PasswordtextBox2.Text,
                    ServiceId = service.Id,
                    time = DateTime.Now
                };
                context.Accounts.Add(account);
                context.SaveChanges();
                for (int i = 0; i < ExtraFieldList.Count; i++)
                {
                    context.ExtraFields.Add(new ExtraField
                    {
                        Name = ExtraFieldList[i].Name,
                        Value = ExtraFieldList[i].Value,
                        AccountId = account.Id
                    });
                    context.SaveChanges();
                }
                return;
            }
            AddVisibleTrue();
            button1.Enabled = false;
        }


        private void button4_Click(object sender, EventArgs e)
        {
            if (NameExtraField.Visible == false)
            {
                NameExtraField.Visible = true;
                ValueExtraField.Visible = true;
            }
            if (NameExtraField.Text == "" || ValueExtraField.Text == "")
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
            else if (label13.Visible == false)
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

        private void CreateServicetextBox1_TextChanged(object sender, EventArgs e)
        {
            if (PasswordtextBox2.Text != "" &&
               SectionComboBox1.SelectedItem != null &&
               ServiceComboBox2.SelectedItem != null)
            {
                button1.Text = "Save";
                button1.Enabled = true;

            }
            else
            {

                button1.Text = "Add";
            }

            if (CreateServicetextBox1.Text != "")
            {
                label2.ForeColor = Color.DarkBlue;
            }
            else
            {
                label2.ForeColor = DefaultForeColor;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (CreateServicetextBox1.Text == "")
            {
                return;
            }

            Section tmp = new Section();
            bool check = false;
            foreach (var item in context.Sections)
            {
                if (item.Name == (SectionComboBox1.SelectedItem as string))
                {
                    tmp = item;
                    check = true;
                    break;
                }
            }
            if (!check)
            {
                return;
            }

            foreach (var item in context.Services)
            {
                if (item.Name == CreateServicetextBox1.Text && item.SectionId == tmp.Id)
                {
                    MessageBox.Show("This Service exist!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            context.Services.Add(new Service
            {
                SectionId = tmp.Id,
                Name = CreateServicetextBox1.Text
            });
            context.SaveChanges();
            updateServiceComboBox();
            CreateServicetextBox1.Text = "";
            checkBox1.Checked = false;
        }

        private void PasswordtextBox2_TextChanged(object sender, EventArgs e)
        {
            if (PasswordtextBox2.Text != "" &&
                SectionComboBox1.SelectedItem != null &&
                ServiceComboBox2.SelectedItem != null)
            {
                button1.Text = "Save";
                button1.Enabled = true;

            }
            else
            {

                button1.Text = "Add";
            }
        }

        private void SectionComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PasswordtextBox2.Text != "" &&
               SectionComboBox1.SelectedItem != null &&
               ServiceComboBox2.SelectedItem != null)
            {
                button1.Text = "Save";
                button1.Enabled = true;

            }
            else
            {

                button1.Text = "Add";
            }
        }

        private void ServiceComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var item in context.Services)
            {
                if (item.Name == (ServiceComboBox2.SelectedItem as string))
                {
                    service = item;
                    break;
                }
            }
        }
    }
}
