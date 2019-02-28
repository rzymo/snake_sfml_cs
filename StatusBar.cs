using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace snake_sfml_cs
{
    class StatusBar : World, Drawable
    {
        Text textPts;
        Sprite sprite;

        void Drawable.Draw(RenderTarget target, RenderStates states)
        {
            for (int i = 0; i < World.X; i++)
            {
                sprite.Position = new Vector2f(i * brickSize, World.Y * brickSize);
                target.Draw(sprite, states);
            }
            target.Draw(textPts, states);
        }

        public StatusBar()
        {
            sprite = new Sprite(new Texture("res\\status.png"));

            textPts = new Text()
            {
                Font = new Font("res\\arial.ttf"),
                CharacterSize = 13,
                FillColor = Color.Black,
                Position = new Vector2f(5.0f, World.Y * brickSize + 3)
            };
        }

        public void SetPoints(int pts)
        {
            textPts.DisplayedString = "POINTS: " + pts;
        }
    }
}
