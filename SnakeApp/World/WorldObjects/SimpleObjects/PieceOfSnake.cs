using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeApp
{
    class PieceOfSnake : SimpleObject
    {
        public PieceOfSnake(Map map, int x, int y) : base(map, x, y)
        {
            Shape = 3;
        }

        public override bool Obstacle()
        {
            return true;
        }

    }
}
