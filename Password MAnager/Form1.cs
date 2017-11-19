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
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EFcontext eFcontext = new EFcontext();
            foreach (var item in eFcontext.Users)
            {
                if(item.Login == textBox1.Text && item.Password == textBox2.Text)
                {
                    MainForm mainForm = new MainForm(this,item);
                    mainForm.Show();
                    this.Hide();
                    break;
                }
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SignUpForm signUpForm = new SignUpForm();
            signUpForm.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
            {
                ForgotForm Form__ = new ForgotForm(textBox1.Text);
                Form__.Show();
                return;
            }
            ForgotForm Form_ = new ForgotForm();
            Form_.Show();
        }
    }
}
