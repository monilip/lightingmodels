using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

// 01.05.15
namespace LightingModels
{
    static class Program
    {
        // Singleton
        static private Singleton _singleton = null;
        static public Singleton Singleton { get { return _singleton; } }
  
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // init singleton
            _singleton = new Singleton();
            Singleton.Init();

            Application.Run(new Form1());
        }
    }
}
