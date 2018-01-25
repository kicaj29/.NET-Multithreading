using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultithreadingExamples
{
    public partial class Form1 : Form
    {
        private AsyncAwaitSample x = new AsyncAwaitSample();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAwaitWithOwnTask_Click(object sender, EventArgs e)
        {
            x.Go();
        }
    }
}
