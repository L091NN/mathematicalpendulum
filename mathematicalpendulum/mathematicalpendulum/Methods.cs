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
    class Methods
    {
        public double x { get; set; }
        public double v1 { get; set; }
        public double v2 { get; set; }
        public double h { get; set; }
        public double eps { get; set; }
        public double N { get; set; }

        Functions functions = new Functions();

        public double[] k = new double[6];
        public double[] m = new double[6];

        public double vsh { get; set; }
        public double sv1 { get; set; }

        public double sv2 { get; set; }
        public double vshMax { get; set; }
        public double sMax { get; set; }
        public double hMax { get; set; }
        public double hMin { get; set; }
        public int doubles { get; set; }
        public int divides { get; set; }

        PointPairList v1x = new PointPairList();
        PointPairList v2x = new PointPairList();
        PointPairList v2v1 = new PointPairList();

        void Run()
        {
            double curX = x;
            double curV1 = v1;
            double curV2 = v2;
            for (int i = 1; ; i++) 
            {
                curX = x + h;
                k[0] = functions.f1(curX, curV1, curV2);
                m[0] = functions.f2(curX, curV1, curV2);
                k[1] = functions.f1(curX, curV1 + (h / 4.0) * k[0], curV2 + (h / 4.0) * m[0]);
                m[1] = functions.f2(curX, curV1 + (h / 4.0) * k[0], curV2 + (h / 4.0) * m[0]);
                k[2] = functions.f1(curX, curV1 + (h / 32.0) * (3.0 * k[0]) + (9.0 * k[1]), curV2 + (h / 32.0) * (3.0 * m[0]) + (9.0 * m[1]));
                m[2] = functions.f2(curX, curV1 + (h / 32.0) * (3.0 * k[0]) + (9.0 * k[1]), curV2 + (h / 32.0) * (3.0 * m[0]) + (9.0 * m[1]));
                k[3] = functions.f1(curX, curV1 + h * ((1932.0 / 2197.0) * k[0] - (7200.0 / 2197.0) * k[1] + (7296.0 / 2197.0) * k[2]),
                curV2 + h * ((1932.0 / 2197.0) * m[0] - (7200.0 / 2197.0) * m[1] + (7296.0 / 2197.0) * m[2]));
                m[3] = functions.f2(curX, curV1 + h * ((1932.0 / 2197.0) * k[0] - (7200.0 / 2197.0) * k[1] + (7296.0 / 2197.0) * k[2]),
                curV2 + h * ((1932.0 / 2197.0) * m[0] - (7200.0 / 2197.0) * m[1] + (7296.0 / 2197.0) * m[2]));
                k[4] = functions.f1(curX, curV1 + h * ((439.0 / 216.0) * k[0] - 8.0 * k[1] + (3680.0 / 513.0) * k[2] - (845.0 / 4104.0) * k[3]),
                curV2 + h * ((439.0 / 216.0) * m[0] - 8.0 * m[1] + (3680.0 / 513.0) * m[2] - (845.0 / 4104.0) * m[3]));
                m[4] = functions.f2(curX, curV1 + h * ((439.0 / 216.0) * k[0] - 8.0 * k[1] + (3680.0 / 513.0) * k[2] - (845.0 / 4104.0) * k[3]),
                curV2 + h * ((439.0 / 216.0) * m[0] - 8.0 * m[1] + (3680.0 / 513.0) * m[2] - (845.0 / 4104.0) * m[3]));
                k[5] = functions.f1(curX, curV1 + h * (-(8.0 / 27.0) * k[0] + 2.0 * k[1] - (3544.0 / 2565.0) * k[2] + (1859.0 / 4104.0) * k[3] - (11.0 / 40.0) * k[4]),
                curV2 + h * (-(8.0 / 27.0) * m[0] + 2.0 * m[1] - (3544.0 / 2565.0) * m[2] + (1859.0 / 4104.0) * m[3] - (11.0 / 40.0) * m[4]));
                m[5] = functions.f2(curX, curV1 + h * (-(8.0 / 27.0) * k[0] + 2.0 * k[1] - (3544.0 / 2565.0) * k[2] + (1859.0 / 4104.0) * k[3] - (11.0 / 40.0) * k[4]),
                curV2 + h * (-(8.0 / 27.0) * m[0] + 2.0 * m[1] - (3544.0 / 2565.0) * m[2] + (1859.0 / 4104.0) * m[3] - (11.0 / 40.0) * m[4]));

                sv1 = h * ((1 / 360.0) * k[0] - (128.0 / 4275.0) * k[2] + (127.0 / 6840.0) * k[3] + (1.0 / 50.0) * k[4] + (2.0 / 55.0) * k[5]);
                sv2 = h * ((1 / 360.0) * m[0] - (128.0 / 4275.0) * m[2] + (127.0 / 6840.0) * m[3] + (1.0 / 50.0) * m[4] + (2.0 / 55.0) * m[5]);

                if (sv1 > eps)
                {
                    h /= 2;
                    continue;
                }
                else
                {
                    
                }
            }
        }


    }
}
