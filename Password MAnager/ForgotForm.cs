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
    public partial class ForgotForm : Form
    {
        EFcontext context;

        User user;
        public ForgotForm()
        {
            InitializeComponent();

            context = new EFcontext();

            textBox3.Enabled = false;
            textBox4.Enabled = false;
        }
        public ForgotForm(string login)
        {
            InitializeComponent();

            context = new EFcontext();

            textBox3.Enabled = false;
            textBox4.Enabled = false;

            textBox1.Text = login;
        }


        bool check = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if(check)
            {
                if(textBox3.Text == textBox4.Text)
                {
                    context.Users.Remove(user);
                    context.SaveChanges();
                    user.Password = textBox3.Text;
                    context.Users.Add(user);
                    context.SaveChanges();

                    this.Close();
                }
            }
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("enter your information!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            foreach (var item in context.Users)
            {
                if(textBox2.Text == item.Email)
                {
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    textBox3.Enabled = true;
                    textBox4.Enabled = true;
                    user = item;
                    button1.Text = "Save";
                    check = true;
                    break;
                }
            }
        }

        private void ForgotForm_Load(object sender, EventArgs e)
        {

        }
    }
}
