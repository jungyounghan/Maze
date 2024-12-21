using System;

namespace Maze
{
    struct Player
    {
        public enum Direction
        {
            Up,
            Down, 
            Left, 
            Right
        }

        private static readonly double ForwardSpeed = 6.6666666666666666;//60;
        private static readonly double BackwardSpeed = 6.6666666666666666;//60;
        private static readonly double RotationSpeed = 1;//20;
        private static readonly double AngleValue = 0.5;

        public double x
        {
            get;
            private set;
        }

        public double y
        {
            get;
            private set;
        }

        public double angle
        {
            get; 
            private set;
        }

        public Player(double x, double y, double angle)
        {
            this.x = x;
            this.y = y;
            this.angle = angle;
        }

        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            double x = Math.Cos(angle);
            double y = Math.Sin(angle);
            if (x < -AngleValue)
            {
                if (y > AngleValue)
                {
                    Console.Write('\u2199');
                }
                else if (y < -AngleValue)
                {
                    Console.Write('\u2196');
                }
                else
                {
                    Console.Write('\u2190');
                }
            }
            else if (x > AngleValue)
            {
                if (y > AngleValue)
                {
                    Console.Write('\u2198');
                }
                else if (y < -AngleValue)
                {
                    Console.Write('\u2197');
                }
                else
                {
                    Console.Write('\u2192');
                }
            }
            else
            {
                if (y > AngleValue)
                {
                    Console.Write('\u2193');
                }
                else
                {
                    Console.Write('\u2191');
                }
            }
        }

        private void Intercept(double cos, double sin, int width, int height)
        {
            if (cos > 0)
            {
                if ((int)x > width)
                {
                    x = width;
                }
            }
            else
            {
                if ((int)x < 0)
                {
                    x = 0;
                }
            }
            if (sin > 0)
            {
                if ((int)y > height)
                {
                    y = height;
                }
            }
            else
            {
                if ((int)y < 0)
                {
                    y = 0;
                }
            }
        }

        private void Move(bool forward, Block[,] blocks)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            if (forward == true)
            {
                x += cos * ForwardSpeed * 0.15;
                y += sin * ForwardSpeed * 0.15;
                Intercept(cos, sin, blocks.GetLength(1) - 1, blocks.GetLength(0) - 1);
            }
            else
            {
                x -= cos * BackwardSpeed * 0.15;
                y -= sin * BackwardSpeed * 0.15;
                Intercept(-cos, -sin, blocks.GetLength(1) - 1, blocks.GetLength(0) - 1);
            }
        }

        public void Input(Direction direction, Block[,] blocks)
        {
            switch (direction)
            {
                case Direction.Up:
                    Move(true, blocks);
                    break;
                case Direction.Down:
                    Move(false, blocks);
                    break;
                case Direction.Left:
                    angle -= RotationSpeed * 0.15;
                    break;
                case Direction.Right:
                    angle += RotationSpeed * 0.15;
                    break;
            }
        }
    }
}