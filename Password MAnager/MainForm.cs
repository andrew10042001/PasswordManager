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

        public MainForm(StartForm form,User user)
        {
            this.form = form;
            this.user = user;
            InitializeComponent();
            context = new EFcontext();
            
            for (int i = 0; i < 100; i++)
            {
                treeView1.Nodes.Add("test");
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
