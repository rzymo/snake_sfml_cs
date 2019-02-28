using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Audio;
using SFML.System;

namespace snake_sfml_cs
{
    class Fruit : World, Drawable
    {
        const uint NUM = 5;
        Vector2i pos;

        Sprite [] sprite = new Sprite[NUM+1];
        Sound sound;

        void Drawable.Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(sprite[0], states);
        }

        public Fruit()
        {
            for (uint i = 1; i <= NUM; i++)
            {
                sprite[i] = new Sprite(new Texture("res\\fruit" + i + ".png"));
            }

            NewXY();
            sound = new Sound(new SoundBuffer("res\\eat.wav")) { Volume = 80 };
        }

        public void Eat()
        {
            sound.Play();
            NewXY();
        }

        private void NewXY()
        {
            Random rnd = new Random();
            pos.X = rnd.Next(Convert.ToInt32(World.X));
            pos.Y = rnd.Next(Convert.ToInt32(World.Y));

            sprite[0] = new Sprite(new Texture(sprite[rnd.Next(1, Convert.ToInt32(NUM) + 1)].Texture));
            sprite[0].Position = new Vector2f(pos.X * brickSize, pos.Y * brickSize);
        }

        public Vector2i GetXY()
        {
            return pos;
        }
    }
}
