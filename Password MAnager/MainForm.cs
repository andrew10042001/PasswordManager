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

        bool[] txtcheck = { false, false, false, false, false };

        void ShowLabels(int x)
        {
            if (txtcheck.Contains(true))
            {
                int index = Array.IndexOf(txtcheck, true);
                if (index == 0 && x != 171)
                {
                    label4.Visible = true;
                    label5.Visible = true;
                }
                else if (index == 1 && x != 210)
                {
                    label13.Visible = true;
                    label6.Visible = true;
                }
                else if (index == 2 && x != 249)
                {
                    label12.Visible = true;
                    label7.Visible = true;
                }
                else if (index == 3 && x != 288)
                {
                    label11.Visible = true;
                    label8.Visible = true;
                }
                else if (index == 4 && x != 327)
                {
                    label10.Visible = true;
                    label9.Visible = true;
                }
                else
                {
                    return;
                }
                txtcheck[index] = false;
            }
            if (x == 171)
            {
                txtcheck[0] = true;

            }
            else if (x == 210)
            {
                txtcheck[1] = true;
            }
            else if (x == 249)
            {
                txtcheck[2] = true;
            }
            if (x == 288)
            {
                txtcheck[3] = true;
            }
            if (x == 327)
            {
                txtcheck[4] = true;
            }
        }
        public MainForm(StartForm form, User user)
        {


            this.form = form;
            this.user = user;
            context = new EFcontext();
            ExtraFieldList = new List<ExtraField>();

            InitializeComponent();
            AddVisibleFalse();
            showVisibleFalse();




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
            EditVisibleFalse();
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
            edit = false;
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
            if (!edit)
                return;
            if (ValueExtraField.Location.Y == 171)
            {
                label5.Text = ValueExtraField.Text;
                ShowLabels(171);

            }
            else if (ValueExtraField.Location.Y == 210)
            {
                label6.Text = ValueExtraField.Text;
                ShowLabels(210);
            }
            else if (ValueExtraField.Location.Y == 249)
            {
                label7.Text = ValueExtraField.Text;
                ShowLabels(249);
            }
            else if (ValueExtraField.Location.Y == 288)
            {
                label8.Text = ValueExtraField.Text;
                ShowLabels(288);
            }
            else if (ValueExtraField.Location.Y == 327)
            {
                label9.Text = ValueExtraField.Text;
                ShowLabels(327);
            }
        }

        void AddVisibleTrue()
        {
            EditVisibleFalse();
            ValueExtraField.Location = new Point(ValueExtraField.Location.X, 171);
            NameExtraField.Location = new Point(NameExtraField.Location.X, 171);
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
            PasswordtextBox2.Text = "";


        }

        void EditVisibleFalse()
        {
            toolStripLabel5.Text = "Edit";
        }

        void AddVisibleFalse()
        {
            NameExtraField.Text = "";
            ValueExtraField.Text = "";
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
            ExtraFieldList.Clear();
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
            if (File.Exists("\\design.txt"))
            {
                toolStripComboBox2.SelectedItem = toolStripComboBox2.Items[1];
            }
            else
            { 
                toolStripComboBox2.SelectedItem = toolStripComboBox2.Items[0];
            }
            UpdateTreeView();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            form.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddVisibleFalse();
            toolStripLabel5.Text = "Edit";
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
        bool edit = false;
        private void toolStripLabel5_Click(object sender, EventArgs e)
        {

            if ((sender as ToolStripLabel).Text == "Save")
            {
                List<Account> list = new List<Account>(context.Accounts.Where(o => o.Password == label17.Text && o.service.section.UserId == user.Id));
                Account account = list[0];
                List<ExtraField> eList = new List<ExtraField>(context.ExtraFields.Where(o => o.AccountId == account.Id));
                NameExtraField.Visible = false;
                ValueExtraField.Visible = false;
                checkLabels();
                if (label4.Visible == true)
                {

                    context.ExtraFields.RemoveRange(eList);
                    context.SaveChanges();


                    ExtraField tmp_ = new ExtraField();
                    tmp_.AccountId = account.Id;
                    tmp_.Name = label4.Text;
                    tmp_.Value = label5.Text;
                    eList[0] = tmp_;

                    if (label13.Visible == true)
                    {
                        ExtraField tmp = new ExtraField();
                        tmp.AccountId = account.Id;
                        tmp.Name = label13.Text;
                        tmp.Value = label16.Text;
                        eList[1] = tmp;
                    }
                    if (label12.Visible == true)
                    {
                        ExtraField tmp = new ExtraField();
                        tmp.AccountId = account.Id;
                        tmp.Name = label12.Text;
                        tmp.Value = label7.Text;
                        eList[2] = tmp;
                    }
                    if (label11.Visible == true)
                    {
                        ExtraField tmp = new ExtraField();
                        tmp.AccountId = account.Id;
                        tmp.Name = label11.Text;
                        tmp.Value = label8.Text;
                        eList[3] = tmp;
                    }
                    if (label10.Visible == true)
                    {
                        ExtraField tmp = new ExtraField();
                        tmp.AccountId = account.Id;
                        tmp.Name = label10.Text;
                        tmp.Value = label9.Text;
                        eList[4] = tmp;
                    }
                }

                context.Accounts.Remove(account);
                context.SaveChanges();
                account.Password = PasswordtextBox2.Text;
                account.time = DateTime.Now;

                foreach (var item in context.Services)
                {
                    if (ServiceComboBox2.SelectedItem.ToString() == item.Name && item.section.Name == SectionComboBox1.SelectedItem.ToString())
                    {
                        account.ServiceId = item.Id;
                        break;
                    }
                }
                context.Accounts.Add(account);
                context.SaveChanges();
                if (eList.Count != 0)
                {
                    for (int i = 0; i < eList.Count; i++)
                    {
                        context.ExtraFields.Add(new ExtraField
                        {
                            Name = eList[i].Name,
                            Value = eList[i].Value,
                            AccountId = account.Id
                        });
                        context.SaveChanges();
                    }
                    //context.ExtraFields.AddRange(eList);
                    //context.SaveChanges();
                }
                AddVisibleFalse();
                showVisibleFalse();
                edit = false;
                UpdateTreeView();
            }
            if (label14.Visible == false)
                return;
            edit = true;
            showVisibleFalse();
            AddVisibleTrue();

            (sender as ToolStripLabel).Text = "Save";

            button4.Visible = false;
            button3.Visible = false;
            button2.Visible = false;
            button1.Visible = false;
            PasswordtextBox2.Text = label17.Text;
            checkLabels();

        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (!edit && label4.Visible)
                return;
            ValueExtraField.Location = new Point(ValueExtraField.Location.X, 171);
            NameExtraField.Location = new Point(NameExtraField.Location.X, 171);
            NameExtraField.Text = label4.Text;
            ValueExtraField.Text = label5.Text;
            NameExtraField.Visible = true;
            ValueExtraField.Visible = true;
            label4.Visible = false;
            label5.Visible = false;
        }

        private void label13_Click(object sender, EventArgs e)
        {
            if (!edit && label13.Visible)
                return;
            ValueExtraField.Location = new Point(ValueExtraField.Location.X, 210);
            NameExtraField.Location = new Point(NameExtraField.Location.X, 210);
            NameExtraField.Text = label13.Text;
            ValueExtraField.Text = label6.Text;
            NameExtraField.Visible = true;
            ValueExtraField.Visible = true;
            label13.Visible = false;
            label6.Visible = false;
        }

        private void label12_Click(object sender, EventArgs e)
        {
            if (!edit && label12.Visible)
                return;
            ValueExtraField.Location = new Point(ValueExtraField.Location.X, 249);
            NameExtraField.Location = new Point(NameExtraField.Location.X, 249);
            NameExtraField.Text = label12.Text;
            ValueExtraField.Text = label7.Text;
            NameExtraField.Visible = true;
            ValueExtraField.Visible = true;
            label12.Visible = false;
            label7.Visible = false;
        }

        private void label11_Click(object sender, EventArgs e)
        {
            if (!edit && label11.Visible)
                return;
            ValueExtraField.Location = new Point(ValueExtraField.Location.X, 288);
            NameExtraField.Location = new Point(NameExtraField.Location.X, 288);
            NameExtraField.Text = label11.Text;
            ValueExtraField.Text = label8.Text;
            NameExtraField.Visible = true;
            ValueExtraField.Visible = true;
            label11.Visible = false;
            label8.Visible = false;
        }

        private void label10_Click(object sender, EventArgs e)
        {
            if (!edit && label10.Visible)
                return;
            ValueExtraField.Location = new Point(ValueExtraField.Location.X, 327);
            NameExtraField.Location = new Point(NameExtraField.Location.X, 327);
            NameExtraField.Text = label10.Text;
            ValueExtraField.Text = label9.Text;
            NameExtraField.Visible = true;
            ValueExtraField.Visible = true;
            label10.Visible = false;
            label9.Visible = false;
        }
        void checkLabels()
        {
            if (label4.Text != "")
            {
                label4.Visible = true;
                label5.Visible = true;
            }
            if (label13.Text != "")
            {
                label13.Visible = true;
                label6.Visible = true;
            }
            if (label12.Text != "")
            {
                label12.Visible = true;
                label7.Visible = true;
            }
            if (label11.Text != "")
            {
                label11.Visible = true;
                label8.Visible = true;
            }
            if (label10.Text != "")
            {
                label10.Visible = true;
                label9.Visible = true;
            }
        }

        bool checckName = false;
        private void NameExtraField_TextChanged(object sender, EventArgs e)
        {
            if (!edit)
                return;
            if (NameExtraField.Location.Y == 171)
            {
                label4.Text = NameExtraField.Text;
                ShowLabels(171);
            }
            else if (NameExtraField.Location.Y == 210)
            {
                label13.Text = NameExtraField.Text;
                ShowLabels(210);
            }
            else if (NameExtraField.Location.Y == 249)
            {
                label12.Text = NameExtraField.Text;
                ShowLabels(249);
            }
            else if (NameExtraField.Location.Y == 288)
            {
                label11.Text = NameExtraField.Text;
                ShowLabels(288);
            }
            else if (NameExtraField.Location.Y == 327)
            {
                label10.Text = NameExtraField.Text;
                ShowLabels(327);
            }


        }

        private void toolStripLabel7_Click(object sender, EventArgs e)
        {
            form.Show();
            this.Hide();
            if (File.Exists("\\pass.txt"))
            {
                File.Delete("\\pass.txt");
            }

        }

        private void toolStripComboBox2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBox2.SelectedIndex == 0)
            { 
                BackgroundImage = Image.FromFile(@"D:\Password Manager\PasswordManager\Password MAnager\images\2.jpg");
                treeView1.BackColor = Color.LightGray;
                foreach (var item in this.Controls)
                {
                    if (item is TextBox)
                    {
                        (item as TextBox).BackColor = Color.LightGray;
                    }
                    if (item is ComboBox)
                    {
                        (item as ComboBox).BackColor = Color.LightGray;
                    }
                }
                if (File.Exists("\\design.txt"))
                {
                    File.Delete("\\design.txt");
                }
            }
            else
            {
               
                treeView1.BackColor = SystemColors.ActiveCaption;
                BackgroundImage = Image.FromFile(@"D:\Password Manager\PasswordManager\Password MAnager\images\1.jpg");
                foreach (var item in this.Controls)
                {
                    if (item is TextBox)
                    {
                        (item as TextBox).BackColor = SystemColors.ActiveCaption;
                    }
                    if (item is ComboBox)
                    {
                        (item as ComboBox).BackColor = SystemColors.ActiveCaption;
                    }
                }
                if(!File.Exists("\\design.txt"))
                {
                    File.Create("\\design.txt");
                }
            }
        }
    }
}
