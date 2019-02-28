using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Audio;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace snake_sfml_cs
{
    class World : Drawable
    {
        static protected uint brickSize;
        static protected uint X, Y;

        Sprite brick;
        Music music;

        float timer;
        readonly float delay;

        void Drawable.Draw(RenderTarget target, RenderStates states)
        {
            for (uint i = 0; i < X; i++)
                for (uint j = 0; j < Y; j++)
                {
                    brick.Position = new Vector2f(i*brickSize, j*brickSize);
                    target.Draw(brick, states);
                }
        }

        public World() {}
        public World(uint x, uint y, uint _brickSize)
        {
            X = x;
            Y = y;
            brickSize = _brickSize;

            timer = 0.0f;
            delay = 0.1f;

            brick = new Sprite(new Texture("res\\brick.png"));
            music = new Music("res\\true.ogg")
            {
                Loop = true, Volume = 30
            };
        }

        public void Start()
        {
            VideoMode mode = new VideoMode(X*brickSize, (Y+1)*brickSize);
            RenderWindow window = new RenderWindow(mode, "Snejk!");
            window.SetFramerateLimit(60);

            Fruit fruit = new Fruit();
            Snake snake = new Snake();
            StatusBar sbar = new StatusBar();

            music.Play();

            Clock clock = new Clock();
            while (window.IsOpen)
            {
                window.DispatchEvents();

                timer += clock.ElapsedTime.AsSeconds();
                clock.Restart();

                if ((Keyboard.IsKeyPressed(Keyboard.Key.Down))  && snake.GetDir() != 3) snake.SetDir(0);
                if ((Keyboard.IsKeyPressed(Keyboard.Key.Left))  && snake.GetDir() != 2) snake.SetDir(1);
                if ((Keyboard.IsKeyPressed(Keyboard.Key.Right)) && snake.GetDir() != 1) snake.SetDir(2);
                if ((Keyboard.IsKeyPressed(Keyboard.Key.Up))    && snake.GetDir() != 0) snake.SetDir(3);
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape)) window.Close();

                if (timer > delay)
                {
                    timer = 0.0f;
                    snake.Move();
                }

                if (snake.GetHead() == fruit.GetXY())
                {
                    fruit.Eat();
                    snake.Grow();
                }

                sbar.SetPoints(snake.GetLength());
                window.Clear();
                window.Draw(this);
                window.Draw(fruit);
                window.Draw(snake);
                window.Draw(sbar);
                window.Display();
            }
        }
    }
}
