using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeApp
{
    public static class Parser
    {
        public static void Parse(GameCenter gameCenter, int row, int column)
        {
            var place = gameCenter.World[row, column];
            place.Object = new Rock(gameCenter.World, row, column);
            place.Empty = false;
        }
    }
}
