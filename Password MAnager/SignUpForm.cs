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
    public partial class SignUpForm : Form
    {
        public SignUpForm()
        {
            InitializeComponent();
            if (File.Exists("\\design.txt"))
            {
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
            }
            else
            {
                BackgroundImage = Image.FromFile(@"D:\Password Manager\PasswordManager\Password MAnager\images\2.jpg");
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
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text =="")
            {
                MessageBox.Show("enter your information!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(textBox3.Text != textBox4.Text)
            {
                MessageBox.Show("Check your password!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            EFcontext EFContext = new EFcontext();
            foreach (var item in EFContext.Users)
            {
                if(item.Login == textBox1.Text && item.Email == textBox2.Text)
                {
                    return;
                }
            }
           
            EFContext.Users.Add(new User
            {
                Login = textBox1.Text,
                Email = textBox2.Text,
                Password = textBox3.Text              
            });
            EFContext.SaveChanges();
            this.Close();
        }
    }
}
