using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomGeneratorOpdracht
{
    internal interface IGenerator
    {
        List<int> GeefUniekeGetal(int aantal);
    }
}
