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
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
            if (File.Exists("\\pass.txt"))
            {
                File.ReadLines("\\pass.txt");
                string[] text = File.ReadAllLines("\\pass.txt");
                textBox1.Text = text[0];
                textBox2.Text = text[1];
               
            }
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            EFcontext eFcontext = new EFcontext();
            
            foreach (var item in eFcontext.Users)
            {
                if(item.Login == textBox1.Text && item.Password == textBox2.Text)
                {
                    MainForm mainForm = new MainForm(this,item);
                    if(checkBox1.Checked == true)
                    {
                        File.WriteAllText("\\pass.txt",item.Login+"\r\n"+item.Password);
                    }
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
