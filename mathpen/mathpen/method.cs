using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mathpen
{
    public class Method
    {
        public double x { set; get; }
        public double xMax { set; get; }
        public double v1 { set; get; }
        public double v2 { set; get; }
        public double v3 { set; get; }
        public double vud { set; get; }

        public double h { set; get; }
        public double hMax { set; get; }
        public double xHMax { set; get; }
        public double hMin { set; get; }
        public double xHMin { set; get; }
        public double eps { set; get; }

        public double s { set; get; }
        public double diffV { set; get; }
        public double maxDiffV { set; get; }
        public double xMaxDiffV { set; get; }
        public bool maxDiffChange{ set; get; }
        public int doubles { set; get; }
        public int divides { set; get; }

        public double[] k = new double[4];
        public double[] m = new double[4];

        public double v1_2 { set; get; }
        public Functions functions = new Functions();
        public void Ready()
        {
            maxDiffV = 0;
            doubles = 0;
            divides = 0;
        }
        public double getVud(double _h, int _it)
        {
            vud = TryStep(x, v1, v2, _h, _it);
            return vud;
        }
        public double TryStep(double _x, double _u1, double _u2, double _h, int _steps, bool save = false)
        {
            double curX = _x;
            double curU1 = _u1;
            double curU2 = _u2;
            while (_steps-- > 0)
            {
                k[0] = functions.f1(curX, curU1, curU2) * _h;
                m[0] = functions.f2(curX, curU1, curU2) * _h;
                k[1] = functions.f1(curX, curU1 + k[0] / 2.0, curU2 + m[0] / 2.0) * _h;
                m[1] = functions.f2(curX, curU1 + k[0] / 2.0, curU2 + m[0] / 2.0) * _h;
                k[2] = functions.f1(curX, curU1 + k[1] / 2.0, curU2 + m[1] / 2.0) * _h;
                m[2] = functions.f2(curX, curU1 + k[1] / 2.0, curU2 + m[1] / 2.0) * _h;
                k[3] = functions.f1(curX, curU1 + k[2], curU2 + m[2]) * _h;
                m[3] = functions.f2(curX, curU1 + k[2], curU2 + m[2]) * _h;

                curX += _h;
                curU1 += 1.0 / 6.0 * (k[0] + 2.0 * k[1] + 2.0 * k[2] + k[3]);
                curU2 += 1.0 / 6.0 * (m[0] + 2.0 * m[1] + 2.0 * m[2] + m[3]);
            }
            if (save)
            {
                x = curX;
                v1 = curU1;
                v2 = curU2;
                v3 = functions.f2(curX, curU1, curU2);
            }
            return curU1;
        }

        public void OptimizationStep()
        {
            double u = TryStep(x, v1, v2, h, 1);
            double _u = TryStep(x, v1, v2, h / 2.0, 2);
            diffV = Math.Abs(_u - u);
            s = diffV / 15.0;

            while (s > eps || s < (eps / 32.0))
            {
                if (s > eps)
                {
                    h /= 2;
                    u = TryStep(x, v1, v2, h, 1);
                    _u = TryStep(x, v1, v2, h / 2.0, 2);
                    diffV = Math.Abs(_u - u);
                    s = diffV / 15.0;
                    divides++;
                    continue;
                }
                if (s < (eps / 32.0))
                {
                    h *= 2;
                    u = TryStep(x, v1, v2, h, 1);
                    _u = TryStep(x, v1, v2, h / 2.0, 2);
                    diffV = Math.Abs(_u - u);
                    s = diffV / 15.0;
                    doubles++;
                }
            }
            if (h > hMax)
            {
                hMax = h;
            }
            if (h < hMin)
            {
                hMin = h;
            }
            if (diffV > maxDiffV)
            {
                maxDiffV = diffV;
                xMaxDiffV = x;
                maxDiffChange = true;
            }
            else
            {
                maxDiffChange = false;
            };
        }
        public void Step()
        {
            TryStep(x, v1, v2, h, 1, true);
        }
    }
}
