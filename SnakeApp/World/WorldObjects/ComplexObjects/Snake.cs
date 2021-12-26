using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeApp
{
    public struct Position
    {
        public int Row { get; set; }
        public int Column { get; set; }
    }
    public class Snake : ComplexObject
    {
        int direction;
        public int gameScore;
        Egg closerEgg;
    
        public Snake(Map map, int x, int y, int direction) : base(map, x, y, direction)
        {
            Direction = direction;
            Shape = 3;
            gameScore = 0;
            IsAlive = true;
            Queue = new Queue<Position>();
            Queue.Enqueue(new Position() { Row = x, Column = y });
        }

        public Queue<Position> Queue { get; set; }

        public Position Last { get; private set; }

        public bool IsAlive { get; private set; }

        public int Direction { get { return direction; } set { direction = value; } }
        public int GameScore { get { return gameScore; } }
        Egg CloserEgg { get { return closerEgg; } set { closerEgg = value; } }

        public override void Move(int direction)
        {
            int newRow = Row + dirRow[direction];
            int newCol = Column + dirCol[direction];
            bool grow = false;

            if (newRow < 0) newRow = map.RowsCount - 1;
            if (newCol < 0) newCol = map.ColumnsCount - 1;

            if (newRow >= map.RowsCount) newRow = newRow % map.RowsCount;
            if (newCol >= map.ColumnsCount) newCol = newCol % map.ColumnsCount;

            if (map[newRow, newCol].Object != null &&
               (map[newRow, newCol].Object.Shape == 1
             || map[newRow, newCol].Object.Shape == 3))
                IsAlive = false;

            if (IsEgg(map[newRow, newCol]))
            {
                CloserEgg = mediator.BFS(map[newRow, newCol]);
                grow = true;
                if (CloserEgg != null) gameScore += CloserEgg.Number * 100;
            }

            if (!grow)
                Last = Queue.Dequeue();

            if (IsEgg(map[newRow, newCol]))
            {
                Egg temp = map[newRow, newCol].Object as Egg;
                for (int i = temp.Number; i > 0; i--)
                {
                    Queue.Enqueue(new Position() { Row = newRow, Column = newCol });
                    Row = newRow; Column = newCol;
                    mediator.RefreshSnake(this, grow);
                }
            }
            else
            {
                Queue.Enqueue(new Position() { Row = newRow, Column = newCol });
                Row = newRow; Column = newCol;
                mediator.RefreshSnake(this, grow);
            }
            
        }

        private bool NoEggs()
        {
            for (int i = 0; i < map.RowsCount; i++)
                for (int j = 0; j < map.ColumnsCount; j++)
                    if (map[i, j].Object != null && map[i, j].Object.Shape == 2)
                        return false;
            return true;
        }

        private bool IsEgg(Cell cell)
        {
            return cell.Object is Egg;
        }
    }
}
