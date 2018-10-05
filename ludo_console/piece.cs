using System;
using System.Collections.Generic;
using System.Text;

namespace ludo_console
{
    class piece
    {
        //public string Color { get; }
        public int WinStart { get; }
        public int WinEnd { get; }


        public bool Home { get; set; }
        public int Index { get; set; }
        public bool Won { get; set; }
        public  int Id { get; }
        public string Color { get; set; }
        public bool Inner { get; set; }


        public piece(int StartIndex, int id, string color)
        {
            Id = id;
            Home = true ;
            Won = false;
            Index = StartIndex;
            WinStart = (StartIndex + 50)%52;
            WinEnd = (StartIndex + 56) % 52;
            Inner =false;
        }
    }
}
