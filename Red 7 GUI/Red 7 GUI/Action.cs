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
 * 
 */
namespace Red_7_GUI
{
    public class Action
    {
        private bool end;
        private string type;
        private int prevGameState;
        private int[] startPos;
        private int[] endPos;
        public Action(string type, int gameState)
        {
            end = true;
            this.type = type;
            prevGameState = gameState;
        }
        public bool End { get { return end; } set { end = value; } }
        public int[] StartPos { get { return startPos; } set { startPos = value; } }
        public int[] EndPos { get { return endPos; } set { endPos = value; } }
        public int PrevGameState { get { return prevGameState; } }
        public string Type { get { return type; } }
    }
}
