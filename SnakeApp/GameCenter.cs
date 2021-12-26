using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeApp
{
    public class GameCenter
    {
        Map world;
        Mediator mediator;
        Random random;
        int eggsCount;

        public GameCenter(Map world, int Q)
        {
            this.world = world;
            mediator = new Mediator(world);
            snake = new Snake(world, 0, 0, 1);
            mediator.RefreshSnake(snake, false);
            random = new Random(DateTime.Now.Millisecond);
            eggsCount = Q;
            GameOver = false;
        }
        public Snake snake { get; private set; }
        public Map World { get { return world; } set { world = value; } }
        public Mediator Mediator { get { return mediator; } }
        public bool GameOver { get; set; }
        public void Play()
        {
            if (!IsGameOver())
            {
                if (NoEggs())
                    SetEggs(eggsCount);
                snake.Move(snake.Direction);
            }
            else GameOver = true;
        }

        private void SetEggs(int eggsCount)
        {
            int _eggsCount = eggsCount;

          

            while (_eggsCount > 0) 
            {
                int randonRow = random.Next(0, world.RowsCount);
                int randomCol = random.Next(0, world.ColumnsCount);

                if (world[randonRow, randomCol].Empty)
                {
                    mediator.CreateObject(randonRow, randomCol, 2);
                    _eggsCount--;
                } 
            }
        }

        private bool NoEggs()
        {
            for (int i = 0; i < world.RowsCount; i++)
                for (int j = 0; j < world.ColumnsCount; j++)
                    if (world[i, j].Object != null && world[i, j].Object.Shape == 2) 
                        return false;
            return true;
        }

        private bool IsGameOver()
        {
            return !snake.IsAlive || NoPlaceForEgg();
        }

        private bool NoPlaceForEgg()
        {
            for (int i = 0; i < world.RowsCount; i++)
                for (int j = 0; j < world.ColumnsCount; j++)
                    if (world[i, j].Empty)
                        return false;
            return true;
        }
    }
}
