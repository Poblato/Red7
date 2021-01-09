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
            gameScreen = new GameScreen(4, true, true);
            Application.Run(gameScreen);
        }
        public static void Update(int set)//updates a hand/palette - 1 for hand, other nums for palettes
        {
            if (set == -1)
            {
                gameScreen.RedrawHand();
            }
            else
            {
                gameScreen.RedrawPalette(set);
            }
        }
    }
}
