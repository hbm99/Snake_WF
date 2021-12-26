using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeApp
{
    public class Mediator
    {
        Map world;
        int numberOfEggs;

        public Mediator(Map world)
        {
            this.world = world;
            numberOfEggs = 1;
        }

        public bool CreateObject(int x, int y, int shape)
        {
            if (world.EggsCount() == 1) numberOfEggs = 1;
            if (!world.IsInside(x, y))
                return false;
            switch (shape)
            {
                case 0:
                    {
                        world[x, y] = new Cell(null);
                    }
                    return true;
                case 1:
                    {
                        world[x, y] = new Cell(new Rock(world, x, y));
                    }
                    return true;
                case 2:
                    {
                        world[x, y] = new Cell(new Egg(world, x, y, numberOfEggs));
                        numberOfEggs++;
                        
                    }
                    return true;
                case 3:
                    {
                        world[x, y] = new Cell(new PieceOfSnake(world, x, y));
                    }
                    return true;
            }
            return false;
        }

        public bool RemoveObject(int x, int y)
        {
            if (!world.IsInside(x, y))
                return false;

            world[x, y].Empty = true;
            world[x, y] = new Cell(null);
            return true;
        }
        public void RefreshSnake(Snake snake, bool grow)
        {
            if (!grow)
            {
                CreateObject(snake.Last.Row, snake.Last.Column, 0);
            }
            foreach (var item in snake.Queue)
                CreateObject(item.Row, item.Column, 3);
        }

        internal Egg BFS(Cell cellSnakeHead)
        {
            Egg result = null;

            int[] dirRow = { -1, 0, 1, 0 };
            int[] dirCol = { 0, 1, 0, -1 };

            Queue<Position> queue = new Queue<Position>();
            queue.Enqueue(new Position { Row = cellSnakeHead.Object.Row, Column = cellSnakeHead.Object.Column });

            bool[,] mask = new bool[world.RowsCount, world.ColumnsCount];
            bool found = false;
            int row = 0;
            int col = 0;

            mask[queue.Peek().Row, queue.Peek().Column] = true;

            while (!found && queue.Count != 0) 
            {
                Position temp = queue.Dequeue();
                for (int i = 0; i < dirRow.Length; i++)
                {
                    row = temp.Row + dirRow[i];
                    col = temp.Column + dirCol[i];

                    if (row < 0) row = world.RowsCount - 1;
                    if (col < 0) col = world.ColumnsCount - 1;

                    if (row >= world.RowsCount) row = row % world.RowsCount;
                    if (col >= world.ColumnsCount) col = col % world.ColumnsCount;

                    if (world[row, col].Object is Egg && !mask[row, col]) 
                    {
                        result = (Egg)world[row, col].Object;
                        found = true;
                        break;
                    }
                    if (!mask[row, col])
                    {
                        queue.Enqueue(new Position { Row = row, Column = col });
                        mask[row, col] = true;
                    }
                }
            }
            return result;
        }
    }
}
