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

namespace mathpen
{
    public partial class Form1 : Form
    {
        public Color[] color = { Color.Red, Color.Yellow, Color.Green, Color.Blue, Color.Black};
        GraphPane pane1;
        GraphPane pane2;
        int steps;
        public Method method = new Method();
        public Form1()
        {
            InitializeComponent();
            pane1 = zedGraphControl1.GraphPane;
            pane2 = zedGraphControl2.GraphPane;
            this.ClientSize = new System.Drawing.Size(1600, 900);
            this.tabControl1.Size = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height);
            this.panel1.Hide();
            this.progressBar1.Hide();
            this.checkBox1.Text = "Очищать график";
            this.checkBox2.Text = "Очищать график";
            this.comboBox1.Text = this.comboBox1.Items[0].ToString();
            this.comboBox2.Text = this.comboBox2.Items[0].ToString();
            this.domainUpDown1.SelectedIndex = 1;
            this.domainUpDown2.SelectedIndex = 2;
            this.textBoxU0.Text = (Math.PI / 10.0).ToString();
            this.textBoxdU0.Text = (0.0).ToString();
            this.textBoxX0.Text = (0.0).ToString();
            this.textBoxH.Text = (0.001).ToString();
            this.textBoxL.Text = (0.1).ToString();
            this.textBoxG.Text = (9.8).ToString();
            this.textBoxE.Text = (0.0001).ToString();
            this.textBoxMaxH.Text = (1000).ToString();
            this.textBoxGrX.Text = (1.0).ToString();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_SIZEBOX = 0x40000;

                var cp = base.CreateParams;
                cp.Style |= WS_SIZEBOX;

                return cp;
            }
        }

        public PointPair WhatNeed1(double h)
        {   
            if(this.domainUpDown1.SelectedIndex == 1)
                return new PointPair(method.x, method.v1);
            if (this.domainUpDown1.SelectedIndex == 2)
                return new PointPair(method.x, method.v2);
            if (this.domainUpDown1.SelectedIndex == 3)
                return new PointPair(method.x, method.v3);
            if (this.domainUpDown1.SelectedIndex == 4)
                return new PointPair(method.v1, method.v2);
            if (this.domainUpDown1.SelectedIndex == 5)
                return new PointPair(h, method.diffV);
            return new PointPair(0, 0);
        }
        public PointPair WhatNeed2(double h)
        {
            if (this.domainUpDown2.SelectedIndex == 1)
                return new PointPair(method.x, method.v1);
            if (this.domainUpDown2.SelectedIndex == 2)
                return new PointPair(method.x, method.v2);
            if (this.domainUpDown2.SelectedIndex == 3)
                return new PointPair(method.x, method.v3);
            if (this.domainUpDown2.SelectedIndex == 4)
                return new PointPair(method.v1, method.v2);
            if (this.domainUpDown2.SelectedIndex == 5)
                return new PointPair(h, method.diffV);
            return new PointPair(0, 0);
        }

        public bool RUN()
        {
            //чек нужных графиков
            //обнуление инфы
            //запуск чм
            //прогресс прогрессбара
            //запись элементов в массивы
            //чек цвета
            //рисовка
            if (checkBox1.Checked)
            pane1.CurveList.Clear();
            if (checkBox2.Checked)
                pane2.CurveList.Clear();
            PointPairList list1 = new PointPairList();
            PointPairList list2 = new PointPairList();
            method.v1 = double.Parse(textBoxU0.Text);
            method.v2 = double.Parse(this.textBoxdU0.Text);
            method.x = double.Parse(this.textBoxX0.Text);
            method.h = double.Parse(this.textBoxH.Text);
            method.functions.L = double.Parse(this.textBoxL.Text);
            method.functions.g = double.Parse(this.textBoxG.Text);
            method.eps = double.Parse(this.textBoxE.Text);
            steps = int.Parse(this.textBoxMaxH.Text);
            method.xMax = double.Parse(this.textBoxGrX.Text);
            progressBar1.Value++;

            for (int curH = 0; curH < steps; curH++)
            {
                method.OptimizationStep();
                method.Step();
                list1.Add(WhatNeed1(curH));
                list2.Add(WhatNeed2(curH));
            }

            LineItem li1 = pane1.AddCurve("", list1, color[comboBox1.SelectedIndex], SymbolType.None);
            LineItem li2 = pane2.AddCurve("", list2, color[comboBox2.SelectedIndex], SymbolType.None);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            zedGraphControl2.AxisChange();
            zedGraphControl2.Invalidate();
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //запуск прогресбара
            //конец
            this.panel1.Visible = true;
            this.progressBar1.Value = 0;
            this.progressBar1.Visible = true;

            RUN();

            this.progressBar1.Visible = false;
            this.panel1.Visible = false;
        }
    }
}
