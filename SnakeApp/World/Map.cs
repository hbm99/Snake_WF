using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeApp
{
    public class Cell
    {
        public Cell(WorldObject a)
        {
            if (a == null)
                Empty = true;
            else { Object = a; Empty = false; }
        }
        public WorldObject Object { get; set; }
        internal bool Empty { get; set; }
    }
    public class Map
    {
        Cell[,] map;
        public Map(int rows, int columns)
        {
            map = new Cell[rows, columns];
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++) 
                    map[i, j] = new Cell(null);
        }
        public int RowsCount { get { return map.GetLength(0); } }

        public int ColumnsCount { get { return map.GetLength(1); } }

        public Cell this[int i, int j]
        {
            get { return map[i, j]; }
            set { map[i, j] = value; }
        }

        public bool IsInside(int i, int j)
        {
            return i >= 0 && j >= 0 && i < map.GetLength(0) && j < map.GetLength(1);
        }

        internal int EggsCount()
        {
            int counter = 1;
            for (int i = 0; i < RowsCount; i++)
                for (int j = 0; j < ColumnsCount; j++)
                    if (this[i, j].Object != null && this[i, j].Object.Shape == 2) 
                        counter++;
            return counter;
        }
    }
}
