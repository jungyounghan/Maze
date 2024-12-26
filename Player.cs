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

        private static readonly double ForwardSpeed = 3;
        private static readonly double BackwardSpeed = 1;
        private static readonly double RotationSpeed = 1;
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
                    //Console.Write('\u2199');
                    Console.Write('\u2B69');
                }
                else if (y < -AngleValue)
                {
                    //Console.Write('\u2196');
                    Console.Write('\u2B66');
                }
                else
                {
                    //Console.Write('\u2190');
                    Console.Write('\u2B60');
                }
            }
            else if (x > AngleValue)
            {
                if (y > AngleValue)
                {
                    //Console.Write('\u2198');
                    Console.Write('\u2B68');
                }
                else if (y < -AngleValue)
                {
                    //Console.Write('\u2197');
                    Console.Write('\u2B67');
                }
                else
                {
                    //Console.Write('\u2192');
                    Console.Write('\u2B62');
                }
            }
            else
            {
                if (y > AngleValue)
                {
                    //Console.Write('\u2193');
                    Console.Write('\u2B63');
                }
                else
                {
                    //Console.Write('\u2191');
                    Console.Write('\u2B61');
                }
            }
        }

        private void Move(bool forward, Block[,] blocks, double elapsedTime)
        {
            int width = blocks.GetLength(1);
            int height = blocks.GetLength(0);
            double cos = forward == true ? Math.Cos(angle) : -Math.Cos(angle);
            double sin = forward == true ? Math.Sin(angle) : -Math.Sin(angle);
            double speed = forward == true ? ForwardSpeed : BackwardSpeed;
            double x = this.x + (cos * speed * elapsedTime);
            double y = this.y + (sin * speed * elapsedTime);
            if (x < 0)
            {
                x = 0;
            }
            else if(x > width - 1)
            {
                x = width - 1;
            }
            if(y < 0)
            {
                y = 0;
            }
            else if(y > height - 1)
            {
                y = height - 1;
            }
            if (blocks[(int)y, (int)x] == null)
            {
                this.x = x;
                this.y = y;
            }
            else if (blocks[(int)this.y, (int)x] == null)
            {
                this.x = x;
            }
            else if (blocks[(int)y, (int)this.x] == null)
            {
                this.y = y;
            }
        }

        public void Input(Direction direction, Block[,] blocks, double elapsedTime)
        {
            switch (direction)
            {
                case Direction.Up:
                    Move(true, blocks, elapsedTime);
                    break;
                case Direction.Down:
                    Move(false, blocks, elapsedTime);
                    break;
                case Direction.Left:
                    angle -= elapsedTime * RotationSpeed;
                    break;
                case Direction.Right:
                    angle += elapsedTime * RotationSpeed;
                    break;
            }
        }
    }
}