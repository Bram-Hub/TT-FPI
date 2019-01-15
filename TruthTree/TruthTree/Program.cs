using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using TruthTree.UI;

namespace TruthTree
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Force the loading of the QUT dll now so it doesn't need to do it later
            Logic.Sentence.parseFromString("A");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
