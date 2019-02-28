using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;


namespace snake_sfml_cs
{
    class Snake : Drawable
    {
        int dir;

        List<Vector2i> s = new List<Vector2i>();

        Sprite head = new Sprite();
        Sprite segm = new Sprite();
        Sound  sound;

        void Drawable.Draw(RenderTarget target, RenderStates states)
        {
            head.Position = new Vector2f(s[0].X * World.brickSize, s[0].Y * World.brickSize);
            target.Draw(head, states);

            for (int i = 0; i < s.Count; i++)
            {
                segm.Position = new Vector2f(s[i].X * World.brickSize, s[i].Y * World.brickSize);
                target.Draw(segm, states);
            }
        }

        public Snake()
        {
            dir = 0;

            for (int i = 0; i < 3; i++)
                s.Add(new Vector2i(5, 5-i));

            head  = new Sprite(new Texture("res\\head.png"));
            segm  = new Sprite(new Texture("res\\segm.png"));
            sound = new Sound(new SoundBuffer("res\\bang.wav"));
        }

        public int GetLength() { return s.Count; }
        public int GetDir() { return dir; }
        public Vector2i GetHead() { return s[0]; }
        public void SetDir(int _d) { dir = _d; }
        public void Grow() { s.Add(GetHead()); } // +1

        public void Move()
        {
            for (int i = s.Count-1; i > 0;  i--)
            {
                s.Insert(i, s[i-1]);
                s.RemoveAt(i+1);
            }

            Vector2i tmp = new Vector2i(s[0].X,s[0].Y);
            if (dir == 0) tmp.Y += 1;
            if (dir == 1) tmp.X -= 1;
            if (dir == 2) tmp.X += 1;
            if (dir == 3) tmp.Y -= 1;
            s.Insert(0, tmp);
            s.RemoveAt(1);

            tmp = new Vector2i(s[0].X, s[0].Y);
            if (tmp.X > World.X - 1) tmp.X = 0;
            if (tmp.X < 0) tmp.X = Convert.ToInt32(World.X) - 1;
            if (tmp.Y > World.Y - 1) tmp.Y = 0;
            if (tmp.Y < 0) tmp.Y = Convert.ToInt32(World.Y) - 1;
            s.Insert(0, tmp);
            s.RemoveAt(1);

            for (int i = 1; i < s.Count; i++)
            {
                if (s[0] == s[i])
                {
                    s.RemoveRange(i, s.Count - i);
                    sound.Play();
                }
            }
        }
    }
}
