using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeApp
{
    public class Egg : SimpleObject
    {
        int q;
        public Egg(Map map, int x, int y, int q) : base(map, x, y)
        {
            Shape = 2;
            this.q = q;
        }
        public int Number { get { return q; } }

        public override bool Obstacle()
        {
            return false;
        }
    }
}
