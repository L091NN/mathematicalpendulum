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
            //this.panel2.Hide();
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
            pane1.Title = "";
            pane2.Title = "";
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
            if (this.domainUpDown1.SelectedIndex == 1)
            {
                pane1.XAxis.Title = "X";
                pane1.YAxis.Title = "U(x)";
                return new PointPair(method.x, method.v1);
            }
            if (this.domainUpDown1.SelectedIndex == 2)
            {
                pane1.XAxis.Title = "X";
                pane1.YAxis.Title = "U'(x)";
                return new PointPair(method.x, method.v2);
            } if (this.domainUpDown1.SelectedIndex == 3)
            {
                pane1.XAxis.Title = "X";
                pane1.YAxis.Title = "U''(x)";
                return new PointPair(method.x, method.v3);
            }
            if (this.domainUpDown1.SelectedIndex == 4)
            {
                pane1.XAxis.Title = "U(x)";
                pane1.YAxis.Title = "U'(x)";
                return new PointPair(method.v1, method.v2);
            } if (this.domainUpDown1.SelectedIndex == 5)
            {
                pane1.XAxis.Title = "Номер шага";
                pane1.YAxis.Title = "Погрешность";
                return new PointPair(h, method.diffV);
            }
            return new PointPair(0, 0);
        }
        public PointPair WhatNeed2(double h)
        {
            if (this.domainUpDown2.SelectedIndex == 1)
            {
                pane2.XAxis.Title = "X";
                pane2.YAxis.Title = "U(x)";
                return new PointPair(method.x, method.v1);
            }          
            if (this.domainUpDown2.SelectedIndex == 2)
            {
                pane2.XAxis.Title = "X";
                pane2.YAxis.Title = "U'(x)";
                return new PointPair(method.x, method.v2);
            }
            if (this.domainUpDown2.SelectedIndex == 3)
            {
                pane2.XAxis.Title = "X";
                pane2.YAxis.Title = "U''(x)";
                return new PointPair(method.x, method.v3);
            } 
            if (this.domainUpDown2.SelectedIndex == 4)
            {
                pane2.XAxis.Title = "U(x)";
                pane2.YAxis.Title = "U'(x)";
                return new PointPair(method.v1, method.v2);
            }
            if (this.domainUpDown2.SelectedIndex == 5)
            {
                pane2.XAxis.Title = "Номер шага";
                pane2.YAxis.Title = "Погрешность";
                return new PointPair(h, method.diffV);
            }
            return new PointPair(0, 0);
        }
        void InitInfoLabel()
        {
            label17.Text =
            "Начальные условия: Xo = " + textBoxX0.Text + ", Uo = " + textBoxU0.Text + ", U'o = " + textBoxdU0.Text + ", ho = " + textBoxH.Text + ", L = " + textBoxL.Text +
            ", g = " + textBoxG.Text + ", ε = " + textBoxE.Text + "\n" +
            "max |S| = " + (method.maxDiffV / 8.0) + " при x = " + (method.xMaxDiffV);
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
            Table.Rows.Clear();
            if (checkBox1.Checked)
            pane1.CurveList.Clear();
            if (checkBox2.Checked)
                pane2.CurveList.Clear();
            PointPairList list1 = new PointPairList();
            PointPairList list2 = new PointPairList();
            method.v1 = double.Parse(textBoxU0.Text);
            method.v2 = double.Parse(this.textBoxdU0.Text);
            method.x = double.Parse(this.textBoxX0.Text);
            method.xMax = method.x;
            method.h = double.Parse(this.textBoxH.Text);
            method.hMax = method.h;
            method.hMin = method.h;
            method.functions.L = double.Parse(this.textBoxL.Text);
            method.functions.g = double.Parse(this.textBoxG.Text);
            method.eps = double.Parse(this.textBoxE.Text);
            steps = int.Parse(this.textBoxMaxH.Text);
            method.xMax = double.Parse(this.textBoxGrX.Text);
            method.Ready();
            if (domainUpDown1.SelectedIndex != 0 || domainUpDown2.SelectedIndex != 0)
            for (int curH = 0; curH < steps; curH++)
            {
                List<double> row = new List<double>();
                row.Add(curH);
                row.Add(method.h);
                row.Add(method.x + method.h);
                row.Add(method.getVud(method.h, 1));
                row.Add(method.getVud(method.h / 2.0, 2));
                row.Add(Math.Abs(method.getVud(method.h, 1) - method.getVud(method.h / 2.0, 2)));
                row.Add(method.s);
                method.OptimizationStep();

                method.Step();
                row.Add(method.v1);
                row.Add(method.divides);
                row.Add(method.doubles);
                if (domainUpDown1.SelectedIndex != 0)
                {
                    list1.Add(WhatNeed1(curH));
                }

                if (domainUpDown2.SelectedIndex != 0)
                {
                    list2.Add(WhatNeed2(curH));
                }
                if (method.x > method.xMax - method.h)
                {
                    break;
                }
                Table.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8], row[9]);
            }
            InitInfoLabel();
            DoubNumLabel.Text = method.doubles.ToString();
            delNumLabel.Text = method.divides.ToString();
            maxStepLabel.Text = method.hMax.ToString();
            minStepLabel.Text = method.hMin.ToString();
            MMLabel.Text = method.maxDiffV.ToString();
            pointMMLabel.Text = method.xMaxDiffV.ToString();
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
            //this.panel1.Visible = true;
            //this.progressBar1.Value = 0;
            //this.progressBar1.Visible = true;

            RUN();
            //progressBar1.Value = progressBar1.Maximum;
            //this.panel2.Visible = false;
        }
    }
}
