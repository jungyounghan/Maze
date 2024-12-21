using System;

namespace Maze
{
    class Block
    {
        public ConsoleColor Color
        {
            get; 
            private set;   
        }

        public Block(ConsoleColor color)
        {
            Color = color;
        }

        public static Block[,] GetRandomBlocks(int width, int height, ref Player player)
        {
            Block[,] blocks = new Block[height, width];
            Random random = new Random();
            int quarter = random.Next(0, 4);
            switch(quarter)
            {
                case 0:
                    player = new Player(width - 1, 0, Math.PI * 0.75);
                    break;
                case 1:
                    player = new Player(0, 0, Math.PI * 0.25);
                    break;
                case 2:
                    player = new Player(0, height - 1, Math.PI * -0.25);
                    break;
                case 3:
                    player = new Player(width - 1, height - 1, Math.PI * -0.75);
                    break;
            }
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (x % 2 != (int)player.x % 2 && y % 2 != (int)player.y % 2)
                    {
                        int color = random.Next((int)ConsoleColor.DarkBlue, (int)ConsoleColor.White + 1);
                        blocks[y, x] = new Block((ConsoleColor)color);
                    }
                    else if(x != (int)player.x || y != (int)player.y)
                    {
                        if(y > 0 && y + 1 < height)
                        {

                        }
                        else
                        {
                            //int color = random.Next((int)ConsoleColor.DarkBlue, (int)ConsoleColor.White + 1);
                            //blocks[y, x] = new Block((ConsoleColor)color);
                        }
                    }
                }
            }
            return blocks;
        }
    }

    static class ExtendedMethod
    {
        public static void Print(this Block block)
        {
            if(block != null)
            {
                Console.ForegroundColor = block.Color;
                Console.Write("■");
            }
            else
            {
                Console.Write("　");
            }
        }
    }
}