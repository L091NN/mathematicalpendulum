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
        PointPairList list1 = new PointPairList();
        PointPairList list2 = new PointPairList();
        List<double> x = new List<double>();
        List<double> u = new List<double>();
        List<double> us = new List<double>();
        List<double> uss = new List<double>();
        List<double> er = new List<double>();

        void clearList()
        {
            x.Clear();
            u.Clear();
            us.Clear();
            uss.Clear();
            er.Clear();
        }

        public Form1()
        {
            InitializeComponent();
            pane1 = zedGraphControl1.GraphPane;
            pane2 = zedGraphControl2.GraphPane;
            this.ClientSize = new System.Drawing.Size(1600, 1000);
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

        void InitInfoLabel()
        {
            label17.Text =
            "Начальные условия: Xo = " + textBoxX0.Text + ", Uo = " + textBoxU0.Text + ", U'o = " + textBoxdU0.Text + ", ho = " + textBoxH.Text + ", L = " + textBoxL.Text +
            ", g = " + textBoxG.Text + ", ε = " + textBoxE.Text + "\n" +
            "max |S| = " + (method.maxDiffV / 15.0) + " при x = " + (method.xMaxDiffV);
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
            clearList();
            
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
            x.Add(method.x);
            u.Add(method.v1);
            us.Add(method.v2);
            uss.Add(method.v3);
            er.Add(method.diffV);
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
                x.Add(method.x);
                u.Add(method.v1);
                us.Add(method.v2);
                uss.Add(method.v3);
                er.Add(method.diffV);

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

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {


            if (RUN())
            {
                DrawPane1();
                DrawPane2();
            }

        }
        void DrawPane1()
        {
            Draw(zedGraphControl1, pane1, GetPoints(GetAbciss(domainUpDown1.SelectedIndex), GetOrdinate(domainUpDown1.SelectedIndex)),
            checkBox1, comboBox1, abciss[domainUpDown1.SelectedIndex], ordinat[domainUpDown1.SelectedIndex]);
        }

        void DrawPane2()
        {
            Draw(zedGraphControl2, pane2, GetPoints(GetAbciss(domainUpDown2.SelectedIndex), GetOrdinate(domainUpDown2.SelectedIndex)),
            checkBox2, comboBox2, abciss[domainUpDown2.SelectedIndex], ordinat[domainUpDown2.SelectedIndex]);
        }
        void Draw( ZedGraphControl zgc, GraphPane gp, PointPairList _ppl,  CheckBox cb,  ComboBox cob, string XAxis, string YAxis)
        {
            if (cb.Checked)
            {
                gp.CurveList.Clear();
            }
            gp.XAxis.Title = XAxis;
            gp.YAxis.Title = YAxis;
            gp.AddCurve("", _ppl, color[cob.SelectedIndex], SymbolType.None);
            zgc.AxisChange();
            zgc.Invalidate();

        }
        string[] abciss = { "" , "X" , "X" , "X" , "U(x)" , "Номер шага" };
        string[] ordinat = { "" , "U(x)", "U'(x)", "U''(x)", "U'(x)", "Погрешность" };
        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            DrawPane1();
        }
        private void domainUpDown2_SelectedItemChanged(object sender, EventArgs e)
        {
            DrawPane2();
        }
        List<double> GetAbciss(int numCB)
        {
            switch(numCB)
            {
                case 1: 
                case 2:
                case 3: return x;
                case 4: return u;
                case 5: return x;
                default:
                    return new List<double>();
            }
            
        }
        List<double> GetOrdinate(int numCB)
        {
            switch (numCB)
            {
                case 1:return u;
                case 2:return us;
                case 3: return uss;
                case 4: return us;
                case 5: return er;
                default:
                    return new List<double>();
            }

        }
        PointPairList GetPoints(List<double> abciss, List<double> ordinat)
        {
            double[] abc = new double[abciss.Count];
            int i = 0;
            foreach (double abci in abciss)
            {
                abc[i] = abci;
                i++;
            }
            double[] ord = new double[ordinat.Count];
            i = 0;
            foreach(double ordi in ordinat)
            {
                ord[i] = ordi;
                i++;
            }
            if (abc.Length > 0) 
            return new PointPairList(abc, ord);
            return new PointPairList();
        }
    }
}
