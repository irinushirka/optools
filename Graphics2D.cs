using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrynaSkurkoOptoolProgram
{
    class Graphics2D
    {
        public List<int> yValues { get; set; }
        public List<int> xValues { get; set; }

        public Graphics2D(List<int> yValues, List<int> xValues)
        {
            this.xValues = xValues;
            this.yValues = yValues;
        }
    }
}
