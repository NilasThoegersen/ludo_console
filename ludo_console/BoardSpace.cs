using System;
using System.Collections.Generic;
using System.Text;

namespace ludo_console
{
    class BoardSpace
    {

        public int Index { get; set; }
        public piece BoardPiece { get; set; }
        public int Count { get; set; }
        public string Special { get; set; }

        public void removePiece()
        {
            if(Count > 1)
            {
                Count--;
            }
            else
            {
                Count--;
                BoardPiece = null;
            }
        }

        public void AddPiece(piece number1)
        {
            BoardPiece = number1;
            Count++;
        }

        public BoardSpace(int i, string type)
        {
            Index = i;
            Special = type;
            Count = 0;
        }

       //public BoardSpace(int i, string start)


    }
}
