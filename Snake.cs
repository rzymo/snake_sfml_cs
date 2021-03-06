﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;


namespace snake_sfml_cs
{
    class Snake : World, Drawable
    {
        int dir;

        List<Vector2i> s = new List<Vector2i> { };

        Sprite head;
        Sprite segm;
        Sound  sound;

        void Drawable.Draw(RenderTarget target, RenderStates states)
        {
            head.Position = new Vector2f(s[0].X * brickSize, s[0].Y * brickSize);
            target.Draw(head, states);

            for (int i = 0; i < s.Count; i++)
            {
                segm.Position = new Vector2f(s[i].X * brickSize, s[i].Y * brickSize);
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
                s[i] = s[i - 1];

            if (dir == 0) s[0] = new Vector2i(s[0].X, s[0].Y+1);
            if (dir == 1) s[0] = new Vector2i(s[0].X-1, s[0].Y);
            if (dir == 2) s[0] = new Vector2i(s[0].X+1, s[0].Y);
            if (dir == 3) s[0] = new Vector2i(s[0].X, s[0].Y-1);

            if (s[0].X > X - 1) s[0] = new Vector2i(0, s[0].Y);
            if (s[0].X < 0) s[0] = new Vector2i(Convert.ToInt32(World.X) - 1, s[0].Y);
            if (s[0].Y > Y - 1) s[0] = new Vector2i(s[0].X, 0);
            if (s[0].Y < 0) s[0] = new Vector2i(s[0].X, Convert.ToInt32(World.Y) - 1);

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
