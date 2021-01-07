using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red_7_GUI
{
    public class Action
    {
        private string type;
        private bool end;
        private int[] startPos;
        private int[] endPos;
        private Card card;
        public Action(string type, Card card)
        {
            this.card = card;
            switch (type)
            {
                case "playToPalette":
                    end = true;
                    break;
                case "discardCard":
                    break;
                case "drawCard":
                    end = false;
                    break;
                default:
                    throw new Exception("Invalid action type");
            }
        }
        public bool End { get { return end; } set { end = value; } }
        public int[] StartPos { get { return startPos; } set { startPos = value; } }
        public int[] EndPos { get { return EndPos; } set { EndPos = value; } }
        public string Type { get { return type; } }
        public Card Card { get { return card; } }
    }
}
