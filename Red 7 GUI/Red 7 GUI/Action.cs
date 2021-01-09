using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* This class can detail any action taken. One player move may be split into multiple actions
 * 
 * End defines if an action is the start of a move (if more actions must be undone to undo a move)
 * startPos is an array of the form [hand/palette, target, position] (0 for hand, 1 for palette) (target: -1 for canvas, -2 for deck others are players)
 * endPos is the same as startPos
 * card is the card being moved
 * type is the type of action
 */
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
            this.type = type;
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
        public int[] EndPos { get { return endPos; } set { endPos = value; } }
        public string Type { get { return type; } }
        public Card Card { get { return card; } }
    }
}
