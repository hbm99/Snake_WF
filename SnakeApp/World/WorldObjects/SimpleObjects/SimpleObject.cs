using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeApp
{
    public abstract class SimpleObject : WorldObject, IObstacle 
    {
        public SimpleObject(Map map, int x, int y) : base(map, x, y)
        {
        }

        public abstract bool Obstacle();

    }
}
