using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeApp
{
    public abstract class WorldObject
    {
        protected static int[] dirRow = { -1, 0, 1, 0 };
        protected static int[] dirCol = { 0, 1, 0, -1 };

        protected Map map;
        protected Mediator mediator;
        protected WorldObject(Map map, int x, int y)
        {
            this.map = map;
            mediator = new Mediator(map);
            if (map.IsInside(x, y)) 
            {
                Row = x;
                Column = y;
            }
        }
        internal int Row { get; set; }

        internal int Column { get; set; }

        public int Shape { get; set; }

        public override string ToString()
        {
            return Shape.ToString();
        }

    }
}
