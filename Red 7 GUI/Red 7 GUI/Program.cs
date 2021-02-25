using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Red_7_GUI
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        
        static private MenuScreen menu;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            menu = new MenuScreen();
            Application.Run(menu);//starts the aplication
        }

        //static functions that allow information/instructions to be passed up the object chain
        public static void Update(int set)//updates a hand/palette - 1 for hand, other nums for palettes
        {
            menu.Update(set);
        }
        public static void RemovePlayer(int player, bool left)//removes a player - left indicates whether they lost in a game or left the lobby
        {
            menu.RemovePlayer(player, left);
        }
        public static void LeaveGame()//tells the lobby to exit when a player quits in the game
        {
            menu.LeaveGame();
        }
        public static void Display(string msg)//displays a message (for debug)
        {
            menu.Display(msg);
        }
        public static void Left()//re-opens the menu after the player leaves a lobby
        {
            menu.Show();
        }
        public static void EndGame(int winner)//re-opens the lobby after the game ends and displays the win message
        {
            menu.EndGame(winner);
        }
    }
}