using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mathematicalpendulum
{
    class Style
    {
        public Color[] colors = {
            Color.FromArgb (237,29,37),
            Color.FromArgb (242,102,35),
            Color.FromArgb (255,148,0),
            Color.FromArgb (255,197,0),
            Color.FromArgb (254,242,0),
            Color.FromArgb (141,199,1),
            Color.FromArgb (1,166,82),
            Color.FromArgb (1,168,158),
            Color.FromArgb (0,84,166),
            Color.FromArgb (46,49,146),
            Color.FromArgb (255,255,255)
        };
        public Color[] purple = {
            Color.FromArgb (53,16,91),    ///PURPLE///
            Color.FromArgb (81,25,140),     ///|///
            Color.FromArgb (105,40,175),    ///|///
            Color.FromArgb (129,57,206),    ///|///
            Color.FromArgb (151,82,224),    ///|///
            Color.FromArgb (172,109,236),   ///|/// change font
            Color.FromArgb (188,144,235),   ///|///
            Color.FromArgb (208,178,241),   ///|///
            Color.FromArgb (231,215,247),   ///|///
            Color.FromArgb (255,255,255), ///WHITE///
        };
        public List<Color> allColors = new List<Color>();

        public void PanelSetColor(Panel panel, Color color)
        {

        }
    }
}
