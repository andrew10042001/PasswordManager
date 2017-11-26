using Password_MAnager.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            showVisibleFalse();

            context = new EFcontext();
            ExtraFieldList = new List<ExtraField>();


            updateSectionComboBox();
            updateServiceComboBox();
            toolStripComboBox1.SelectedItem = toolStripComboBox1.Items[0];
            UpdateTreeView();

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
                if (item.section.UserId == user.Id && item.section.Name == SectionComboBox1.SelectedItem.ToString())
                {
                    ServiceComboBox2.Items.Add(item.Name);
                }
            }
            if (ServiceComboBox2.Items.Count != 0)
            {
                ServiceComboBox2.SelectedItem = ServiceComboBox2.Items[ServiceComboBox2.Items.Count - 1];
            }
        }

        void showViSibleTrue()
        {
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label14.Visible = true;
            label15.Visible = true;
            label16.Visible = true;
            label17.Visible = true;
            label2.Text = "Service";
        }
        void showVisibleFalse()
        {
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            label16.Visible = false;
            label17.Visible = false;
            label2.Text = "Create new service?";
        }

        void ShowAccount(string passw)
        {
            Account account = new Account();
            bool check = false;
            foreach (var item in context.Accounts)
            {
                if (item.Password == passw && item.service.section.UserId == user.Id)
                {
                    check = true;
                    account = item;
                    break;
                }
            }
            if (check)
            {
                label14.Text = account.time.Day + ":" + account.time.Month + ":" + account.time.Year;
                label15.Text = account.service.section.Name;
                label16.Text = account.service.Name;

                label17.Text = account.Password;
                showViSibleTrue();

                foreach (var item in context.ExtraFields)
                {
                    if (item.account.Password == label17.Text && item.account.service.section.UserId == user.Id)
                    {
                        if (label4.Visible == false)
                        {
                            label4.Text = item.Name;
                            label4.Visible = true;
                            label5.Text = item.Value;
                            label5.Visible = true;
                        }
                        else if (label13.Visible == false)
                        {
                            label13.Text = item.Name;
                            label13.Visible = true;
                            label6.Text = item.Value;
                            label6.Visible = true;
                        }
                        else if (label12.Visible == false)
                        {
                            label12.Text = item.Name;
                            label12.Visible = true;
                            label7.Text = item.Value;
                            label7.Visible = true;
                        }
                        else if (label11.Visible == false)
                        {
                            label11.Text = item.Name;
                            label11.Visible = true;
                            label8.Text = item.Value;
                            label8.Visible = true;
                        }
                        else if (label10.Visible == false)
                        {
                            label10.Text = item.Name;
                            label10.Visible = true;
                            label9.Text = item.Value;
                            label9.Visible = true;
                        }
                    }
                }
            }
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            AddVisibleFalse();
            button1.Text = "Add";
            button1.Enabled = true;
            string selectedNodeText = e.Node.Text;
            if (toolStripComboBox1.SelectedIndex == 0)
            {
                if (e.Node.Parent != null)
                {
                    if (e.Node.Parent.Parent != null)
                    {
                        ShowAccount(e.Node.Text);
                    }
                }
            }
            else
            {
                ShowAccount(e.Node.Text);
            }
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text == "")
            {
                return;
            }
            foreach (var item in context.Sections)
            {
                if (item.Name == toolStripTextBox1.Text && item.UserId == user.Id)
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
            UpdateTreeView();
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
            updateSectionComboBox();
            updateServiceComboBox();
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
            label15.Visible = false;
            label16.Visible = false;
            label17.Visible = false;
            NameExtraField.Visible = false;
            ValueExtraField.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            SectionComboBox1.Items.Clear();
            ServiceComboBox2.Items.Clear();
            PasswordtextBox2.Text = "";
            label4.Text = "";
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
            label9.Text = "";
            label10.Text = "";
            label11.Text = "";
            label12.Text = "";
            label13.Text = "";
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


                UpdateTreeView();
                button4.Enabled = true;
                button5.Enabled = true;
                AddVisibleFalse();
                ExtraFieldList.Clear();
                return;
            }
            showVisibleFalse();
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
        void UpdateTreeView()
        {
            treeView1.Nodes.Clear();
            if (toolStripComboBox1.SelectedIndex == 0)
            {
                foreach (var item in context.Sections)
                {
                    if (item.UserId == user.Id)
                    {
                        treeView1.Nodes.Add(item.Name);
                    }
                }
                if (treeView1.Nodes.Count == 0)
                    return;
                for (int i = 0; i < treeView1.Nodes.Count; i++)
                {
                    foreach (var item in context.Services)
                    {
                        if (item.section.Name == treeView1.Nodes[i].Text && item.section.UserId == user.Id)
                        {
                            TreeNode treeNode = new TreeNode(item.Name);
                            treeNode.ImageIndex = 1;
                            treeView1.Nodes[i].Nodes.Add(treeNode);

                        }
                    }
                }
                for (int i = 0; i < treeView1.Nodes.Count; i++)
                {
                    for (int q = 0; q < treeView1.Nodes[i].Nodes.Count; q++)
                    {
                        foreach (var item in context.Accounts)
                        {
                            if (item.service.Name == treeView1.Nodes[i].Nodes[q].Text && item.service.section.UserId == user.Id)
                            {
                                TreeNode treeNode = new TreeNode(item.Password);
                                treeNode.ImageIndex = 2;
                                treeView1.Nodes[i].Nodes[q].Nodes.Add(treeNode);
                            }
                        }
                    }
                }
            }
            else if (toolStripComboBox1.SelectedIndex == 1)
            {
                List<Account> list = new List<Account>(context.Accounts.Where(o => o.service.section.UserId == user.Id));
                list = list.OrderBy(o => o.time).ToList();
                list.Reverse();
                foreach (var item in list)
                {
                    TreeNode treeNode = new TreeNode(item.Password);
                    treeNode.ImageIndex = 2;
                    treeView1.Nodes.Add(treeNode);
                }
            }
            else
            {
                foreach (var item in context.Accounts)
                {
                    if (item.service.section.UserId == user.Id)
                    {
                        TreeNode treeNode = new TreeNode(item.Password);
                        treeNode.ImageIndex = 2;
                        treeView1.Nodes.Add(treeNode);
                    }
                }
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateTreeView();
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
            UpdateTreeView();
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
            updateServiceComboBox();
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (treeView1.Nodes.Count == 0)
                return;

            if (treeView1.SelectedNode.Parent != null)
            {
                if (treeView1.SelectedNode.Parent.Parent != null)
                {
                    string pass = treeView1.SelectedNode.Text;
                    Account account = new Account();
                    foreach (var item in context.Accounts)
                    {
                        if (item.Password == pass && item.service.section.UserId == user.Id)
                        {
                            account = item;
                            break;
                        }
                    }
                    context.Accounts.Remove(account);
                    context.SaveChanges();
                    UpdateTreeView();
                    return;
                }
            }
            string name = treeView1.SelectedNode.Text;
            if (treeView1.SelectedNode.Parent != null)
            {

                List<Account> list_ = new List<Account>();
                foreach (var item in context.Accounts)
                {
                    if (item.service.Name == name && item.service.section.UserId == user.Id)
                    {
                        list_.Add(item);
                    }
                }
                context.Accounts.RemoveRange(list_.ToArray());
                context.SaveChanges();
                Service service = new Service();
                foreach (var item in context.Services)
                {
                    if (item.Name == name && item.section.UserId == user.Id)
                    {
                        service = item;
                        break;
                    }
                }
                context.Services.Remove(service);
                context.SaveChanges();
                UpdateTreeView();
                return;
            }
            List<Account> list = new List<Account>();
            foreach (var item in context.Accounts)
            {
                if (item.service.section.Name == name && item.service.section.UserId == user.Id)
                {
                    list.Add(item);
                }
            }
            context.Accounts.RemoveRange(list.ToArray());
            context.SaveChanges();
            List<Service> listS = new List<Service>();
            foreach (var item in context.Services)
            {
                if (item.section.Name == name && item.section.UserId == user.Id)
                {
                    listS.Add(item);
                }
            }
            context.Services.RemoveRange(listS.ToArray());
            context.SaveChanges();

            Section section = new Section();
            foreach (var item in context.Sections)
            {
                if (item.Name == name && item.UserId == user.Id)
                {
                    section = item;
                    break;
                }
            }
            context.Sections.Remove(section);
            context.SaveChanges();
            UpdateTreeView();
            return;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label17.Visible == false)
                return;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string text = "Service: " + label16.Text +
                    "\r\nPassword: " + label17.Text + "\r\n";
                if (label4.Visible == true)
                {
                    text += label4.Text + ": " + label5.Text + "\r\n";
                    if (label13.Visible == true)
                    {
                        text += label13.Text + ": " + label6.Text + "\r\n";
                        if (label12.Visible == true)
                        {
                            text += label12.Text + ": " + label7.Text + "\r\n";
                            if (label11.Visible == true)
                            {
                                text += label11.Text + ": " + label8.Text + "\r\n";
                                if (label10.Visible == true)
                                {
                                    text += label10.Text + ": " + label9.Text + "\r\n";
                                }
                            }
                        }
                    }

                }
                File.WriteAllText(folderBrowserDialog1.SelectedPath + "\\" + label16.Text + ".txt", text);
            };

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
            UpdateTreeView();
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = folderBrowserDialog1.SelectedPath + "\\" + "Passwords";
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                directoryInfo.Create();

                List<Section> SectionList = new List<Section>(context.Sections.Where(o => o.UserId == user.Id));
                if (SectionList.Count == 0)
                    return;
                foreach (var item in SectionList)
                {
                    Directory.CreateDirectory(path + "\\" + item.Name);
                }
                for (int i = 0; i < SectionList.Count; i++)
                {
                    int id = SectionList[i].Id;
                    List<Service> ServiceList = new List<Service>(context.Services.Where(o => o.SectionId == id));

                    foreach (var item in ServiceList)
                    {
                        Directory.CreateDirectory(path + "\\" + SectionList[i].Name + "\\" + item.Name);
                    }
                    for (int j = 0; j < ServiceList.Count; j++)
                    {
                        int ServiceId = ServiceList[j].Id;
                        List<Account> AccountList = new List<Account>(context.Accounts.Where(o => o.ServiceId == ServiceId));
                        if (AccountList.Count == 0)
                            return;
                        for (int k = 0; k < AccountList.Count; k++)
                        {
                            string text = "Section: " + SectionList[i].Name + "\r\n"
                                + "Service: " + ServiceList[j].Name + "\r\n"
                                + "Password: " + AccountList[k].Password + "\r\n"
                            + "add time: " + (AccountList[k].time.ToShortDateString()) + "\r\n";
                            int AccountId = AccountList[k].Id;
                            List<ExtraField> ExtraFieldlist = new List<ExtraField>(context.ExtraFields.Where(o => o.AccountId == AccountId));
                            foreach (var item in ExtraFieldlist)
                            {
                                text += item.Name + ": " + item.Value + "\r\n";
                            }
                            File.WriteAllText(path + "\\" + SectionList[i].Name + "\\" + ServiceList[j].Name + "\\" + AccountList[k].Password + ".txt", text);
                        }
                    }
                }


            };
        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (treeView1.Nodes.Count == 0)
                    return;
                List<Section> SectionList = new List<Section>(context.Sections.Where(o => o.UserId == user.Id));
                
                for (int i = 0; i < SectionList.Count; i++)
                {
                    int id = SectionList[i].Id;
                    List<Service> ServiceList = new List<Service>(context.Services.Where(o => o.SectionId == id));

                    for (int j = 0; j < ServiceList.Count; j++)
                    {
                        int ServiceId = ServiceList[j].Id;
                        List<Account> AccountList = new List<Account>(context.Accounts.Where(o => o.ServiceId == ServiceId));
                        if (AccountList.Count == 0)
                            return;
                        for (int k = 0; k < AccountList.Count; k++)
                        {
                            int AccountId = AccountList[k].Id;
                            List<ExtraField> ExtraFieldlist = new List<ExtraField>(context.ExtraFields.Where(o => o.AccountId == AccountId));
                            context.ExtraFields.RemoveRange(ExtraFieldlist);
                            context.SaveChanges();
                        }
                        context.Accounts.RemoveRange(AccountList);
                        context.SaveChanges();
                    }
                    context.Services.RemoveRange(ServiceList);
                    context.SaveChanges();
                }
                context.Sections.RemoveRange(SectionList);
                context.SaveChanges();
                UpdateTreeView();
            }
        }
    }


}
