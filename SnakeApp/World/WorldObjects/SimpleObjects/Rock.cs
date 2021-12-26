using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeApp
{
    class Rock : SimpleObject
    {
        public Rock(Map map, int x, int y) : base(map, x, y)
        {
            Shape = 1;
        }

        public override bool Obstacle()
        {
            return true;
        }

    }
}
