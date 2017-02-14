using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStoreDemo
{
    public class SingleInstanceManager : WindowsFormsApplicationBase
    {
        App app;
        public SingleInstanceManager()
        {
            base.IsSingleInstance = true;
        }

        protected override bool OnStartup(StartupEventArgs e)
        {
           
            app = new App();
            
            app.Run();
            
            return false;
        }

        protected override void OnStartupNextInstance(StartupNextInstanceEventArgs e)
        {
           
            if (e.CommandLine.Count>0)
            {
                app.ProcessCommandLine(e.CommandLine.ToArray());
            }

            app.Activate();
        }


       
    }
}
