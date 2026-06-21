using System;
using System.Windows.Forms;
using InventoryApp.Utils;

namespace InventoryApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // Load configuration files
            LocalizationManager.Load();
            TransliterationManager.Load();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}