using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace KTXToolkit
{
    class Program
    {
        [ImportMany(typeof(IPlugin))]
        private IPlugin[] plugins = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            new Program().Run();
        }

        private void Run()
        {
            load_plugins();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(plugins));
        }

        private void load_plugins()
        {
            var catalog = new DirectoryCatalog(".");
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }
    }
}
