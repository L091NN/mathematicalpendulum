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
    }
}
