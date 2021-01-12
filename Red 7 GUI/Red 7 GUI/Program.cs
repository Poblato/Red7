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
        
        static private GameScreen gameScreen;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Random rnd = new Random();
            gameScreen = new GameScreen(4, 0, true, true, rnd.Next(0, 2147483647));
            Application.Run(gameScreen);
        }
        public static void Update(int set)//updates a hand/palette - 1 for hand, other nums for palettes
        {
            if (set < 0)
            {
                gameScreen.RedrawHand();
            }
            else
            {
                gameScreen.RedrawPalette(set);
            }
        }
        public static void RemovePlayer(int player)
        {
            gameScreen.RemovePlayer(player);
        }
    }
}