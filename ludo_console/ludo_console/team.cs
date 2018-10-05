using System;
using System.Collections.Generic;
using System.Text;

namespace ludo_console
{
    class team
    {
        public virtual int Id { get; }
        public int Points { get; set; }
        public bool Human { get; }
        public string Color { get; }
        public piece[] Pieces = new piece[4];



        public team(string color, bool human, int id)
        {
            Id = id;
            Color = color;
            Human = human;
            Points = 0;
            for(int i = 0; i < 4; i++)
            {
                Pieces[i] = new piece(13 * id + 2, i, Color);
            }
        }


        public bool AllHome()
        {
            foreach (piece a in Pieces)
            {
                if(a.Home == false)
                {
                    return false;
                }
            }
            return true;
        }

        public void Spawn()
        {
            foreach(piece a in Pieces)
            {
                if (a.Home)
                {
                    a.Home = false;
                    a.Index = 13 * Id + 2%52;
                }
            }
        }


    }
}
