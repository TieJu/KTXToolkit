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
            LoadPlugins();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.Run(new Form1(plugins));
        }

        private void LoadPlugins()
        {
            CompositionContainer container = new CompositionContainer( new DirectoryCatalog( "." ) );
            container.ComposeParts(this);
        }
    }
}
