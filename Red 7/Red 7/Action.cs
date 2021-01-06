using System;
using System.Collections.Generic;
using System.Text;
/* 
 * This class details any simple action that can be taken
 * a move by a player may be split into several actions - especially when action rule is in play
 * 
 * type is the type of action taken, e.g. play to palette, draw card, discard card
 * end is whether this is the start of a player move or more actions were taken in that players' move before this
 * start is an int array in the form: [hand/palette, target, postition] to detail which player and where in their hand/palette the card refernces is (hand is 0, palette is 1)
 * end is an int array in the form: [hand/palette, target, postition]
 *   -1 in the target field is the canvas, -2 is the deck, end always referres to a palette
 */

namespace Red_7._0
{
    public class Action
    {
        private string type;
        private bool end;
        private Card card;
        private int[] start;
        private int[] endpoint;
        public Action(string type, Card card)
        {
            this.card = card;
            switch (type)
            {
                case "playToPalette":
                    end = true; //playing to palette can only happen at the start of a player's move
                    break;
                case "drawCard":
                    end = false; //a player move can never start by drawing a card
                    break;
                case "discardCard":
                    break;
                default:
                    throw new Exception("Invalid action type in object creation");
            }
        }
        public bool End { get { return end; } set { end = value; } }
        public int[] Start { get { return start; } set { start = value; } }
        public int[] Endpoint { get { return endpoint; } set { endpoint = value; } }
    }
}