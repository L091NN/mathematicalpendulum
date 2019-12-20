using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;


namespace mathematicalpendulum
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Start();
            CloseSidebar();
            Begin();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonSolve_Click(object sender, EventArgs e)
        {
            if (isStart)
            {
                Running();
            }
            FillFields();
            met.Run(dataGridView1);
            Draw();
        }

        private void buttonParam_Click(object sender, EventArgs e)
        {
            sidebarOpen = true;
            OpenSidebar();
            if (isVisibleParamPanel)
            {
                panelParam.Visible = false;
                isVisibleParamPanel = false;
            }
            else
            {
                panelParam.Visible = true;
                isVisibleParamPanel = true;
            }
        }

        private void buttonTask_Click(object sender, EventArgs e)
        {

        }

        private void buttonTable_Click(object sender, EventArgs e)
        {

        }

        private void buttonInfo_Click(object sender, EventArgs e)
        {

        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Start();
            Clear();
        }

        private void buttonMenu_Click(object sender, EventArgs e)
        {
            if (sidebarOpen)
            {
                CloseSidebar();
            }
            else
            {
                OpenSidebar();
            }
        }
    }
}
