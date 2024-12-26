using System;
using System.Linq;
using System.Collections.Generic;

namespace Maze
{
    public struct Point
    {
        public int x
        {
            get;
            private set;
        }

        public int y
        {
            get;
            private set;
        }

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write('#');
        }
    }

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

        public static Block[,] GetRandomBlocks(int width, int height, ref Player player, out Point destination)
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
            int x = (int)player.x;
            int y = (int)player.y;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (j != x || i != y)
                    {
                        int color = random.Next((int)ConsoleColor.DarkBlue, (int)ConsoleColor.White);
                        blocks[i, j] = new Block((ConsoleColor)color);
                    }
                }
            }
            Point point = new Point(x, y);
            List<Point> list = new List<Point>();
            Stack<Point> stack = new Stack<Point>();
            Dictionary<Point, int> destinations = new Dictionary<Point, int>();
            list.Add(point);
            stack.Push(point);
            bool reversion = false;
            int depth = 0;
            while (true)
            {
                List<Point[]> temps = new List<Point[]>();
                //상
                if(point.y - 2 >= 0 && list.Contains(new Point(point.x, point.y - 1)) == false && list.Contains(new Point(point.x, point.y - 2)) == false)
                {
                    temps.Add(new Point[] { new Point(point.x, point.y - 1), new Point(point.x, point.y - 2) });
                }
                //하
                if (point.y + 2 <= height - 1 && list.Contains(new Point(point.x, point.y + 1)) == false && list.Contains(new Point(point.x, point.y + 2)) == false)
                {
                    temps.Add(new Point[] { new Point(point.x, point.y + 1), new Point(point.x, point.y + 2) });
                }
                //좌
                if (point.x - 2 >= 0 && list.Contains(new Point(point.x - 1, point.y)) == false && list.Contains(new Point(point.x - 2, point.y)) == false)
                {
                    temps.Add(new Point[] { new Point(point.x - 1, point.y), new Point(point.x - 2, point.y) });
                }
                //우
                if (point.x + 2 <= width - 1 && list.Contains(new Point(point.x + 1, point.y)) == false && list.Contains(new Point(point.x + 2, point.y)) == false)
                {
                    temps.Add(new Point[] { new Point(point.x + 1, point.y), new Point(point.x + 2, point.y) });
                }
                int count = temps.Count;
                if(count > 0)
                {
                    int index = random.Next(0, count);
                    Point[] points = temps[index];
                    int length = points.Length;
                    for(int i = 0; i < length; i++)
                    {
                        point = points[i];
                        list.Add(point);
                        blocks[point.y, point.x] = null;
                        if (i > 0)
                        {
                            depth += 2;
                            stack.Push(point);
                        }
                    }
                }
                else if(stack.Count > 0)
                {                  
                    point = stack.Pop();
                    destinations.Add(point, depth);
                    depth -= 2;
                }
                else
                {
                    if (reversion == false)
                    {
                        reversion = true;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            destination = destinations.Aggregate((a, b) => a.Value > b.Value ? a : b).Key;
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
                Console.Write('\u2588');
            }
            else
            {
                Console.Write(' ');
            }
        }

        public static void Print(this Block block, char shape)
        {
            if (block != null)
            {
                Console.ForegroundColor = block.Color;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.Write(shape);
        }
    }
}