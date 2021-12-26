using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeApp
{
    public abstract class ComplexObject : WorldObject, IMovable 
    {
        public ComplexObject(Map map, int x, int y, int direction) : base(map, x, y)
        {
        }

        public abstract void Move(int direction);
    }
}
