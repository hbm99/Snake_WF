using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeApp
{
    interface IObstacle
    {
        bool Obstacle();
    }
    interface IMovable
    {
        void Move(int direction);
    }
}
