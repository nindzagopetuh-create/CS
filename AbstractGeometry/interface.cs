using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGeometry
{
    interface IHaveDiagonal
    {
        double GetDiagonal();
        void PrintDiagonal();
    }

    interface IHaveDiameter
    {
        double GetDiameter();
        void PrintDiameter();
    }

    interface IHaveHeight
    {
        double GetHeight();
        void PrintHeight();
    }
}
