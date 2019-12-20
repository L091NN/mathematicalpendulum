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
    partial class MainForm
    {
        private bool sidebarOpen = false;
        private bool isStart = true;
        private bool isVisibleParamPanel = false;
        private Methods met = new Methods();
        private int numColors = 10;
        private int curColor = 0;
        private Style style = new Style();
        private GraphPane pane1, pane2, pane3;


        void Begin()
        {
            textBoxX0.Text = "0";
            textBoxU0.Text = "0,31415926535";
            textBoxUs0.Text = "0";
            textBoxH.Text = "0,001";
            textBoxE.Text = "0,001";
            textBoxN.Text = "100";
            textBoxGX.Text = "10";
            textBoxL.Text = "0,1";
            textBoxG.Text = "9,8";
            pane1 = zedGraphControl1.GraphPane;
            pane2 = zedGraphControl2.GraphPane;
            pane3 = zedGraphControl3.GraphPane;
        }

        void Start()
        {
            panelParam.Visible = false;
            buttonTable.Visible = false;
            buttonInfo.Visible = false;
            panelInfo.Visible = false;
            buttonClear.Visible = false;
            isStart = true;
        }

        void Running()
        {
            buttonClear.Visible = true;
            buttonInfo.Visible = true;
            buttonTable.Visible = true;
            isStart = false;
            RefreshSidebar();
        }

        void RefreshSidebar()
        {
            if (sidebarOpen)
            {
                OpenSidebar();
            }
            else
            {
                CloseSidebar();
            }
        }

        void OpenSidebar()
        {
            buttonMenu.Image = global::mathematicalpendulum.Properties.Resources.undo;
            panelSb.Width = 300;
            buttonSolve.Text = "Решить";
            buttonSolve.ImageAlign = ContentAlignment.MiddleRight;
            buttonParam.Text = "Параметры";
            buttonParam.ImageAlign = ContentAlignment.MiddleRight;
            buttonTask.Text = "Задача";
            buttonTask.ImageAlign = ContentAlignment.MiddleRight;
            if (!isStart)
            {
                buttonTable.Text = "Таблица";
                buttonTable.ImageAlign = ContentAlignment.MiddleRight;
                buttonInfo.Text = "Справка";
                buttonInfo.ImageAlign = ContentAlignment.MiddleRight;
                buttonClear.Text = "Очистить";
                buttonClear.ImageAlign = ContentAlignment.MiddleRight;
            }
            sidebarOpen = true;
        }

        void CloseSidebar()
        {
            buttonSolve.Text = "";
            buttonSolve.ImageAlign = ContentAlignment.MiddleCenter;
            buttonParam.Text = "";
            buttonParam.ImageAlign = ContentAlignment.MiddleCenter;
            buttonTask.Text = "";
            buttonTask.ImageAlign = ContentAlignment.MiddleCenter;
            if (!isStart)
            {
                buttonTable.Text = "";
                buttonTable.ImageAlign = ContentAlignment.MiddleCenter;
                buttonInfo.Text = "";
                buttonInfo.ImageAlign = ContentAlignment.MiddleCenter;
                buttonClear.Text = "";
                buttonClear.ImageAlign = ContentAlignment.MiddleCenter;
            }
            buttonMenu.Image = global::mathematicalpendulum.Properties.Resources.menu;
            panelSb.Width = 50;
            HidePanel(panelParam, ref isVisibleParamPanel);
            sidebarOpen = false;
        }

        void HidePanel(Panel panel,ref bool isVisible)
        {
            panel.Visible = false;
            isVisible = false;
        }

        void Draw()
        {
            pane1.AddCurve("v1(x)", met.v1x, style.colors[curColor % numColors], ZedGraph.SymbolType.None);
            pane2.AddCurve("v2(x)", met.v2x, style.colors[curColor % numColors], ZedGraph.SymbolType.None);
            pane3.AddCurve("v2(v1)", met.v2v1, style.colors[curColor % numColors], ZedGraph.SymbolType.None);

            zedGraphControl1.AxisChange();
            zedGraphControl2.AxisChange();
            zedGraphControl3.AxisChange();

            zedGraphControl1.Invalidate();
            zedGraphControl2.Invalidate();
            zedGraphControl3.Invalidate();

            curColor++;

        }

        void FillFields()
        {
            met.x = double.Parse(textBoxX0.Text);
            met.v1 = double.Parse(textBoxU0.Text);
            met.v2 = double.Parse(textBoxUs0.Text);
            met.h = double.Parse(textBoxH.Text);
            met.eps = double.Parse(textBoxE.Text);
            met.N = int.Parse(textBoxN.Text);
            met.xRight = double.Parse(textBoxGX.Text);
            met.functions.L = double.Parse(textBoxL.Text);
            met.functions.g = double.Parse(textBoxG.Text);
        }

        void Clear()
        {
            pane1.CurveList.Clear();
            pane2.CurveList.Clear();
            pane3.CurveList.Clear();
        }
    }
}
