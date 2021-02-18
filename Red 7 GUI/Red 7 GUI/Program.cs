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
            Application.Run(menu);
        }
        public static void Update(int set)//updates a hand/palette - 1 for hand, other nums for palettes
        {
            menu.Update(set);
        }
        public static void RemovePlayer(int player)
        {
            menu.RemovePlayer(player);
        }
    }
}