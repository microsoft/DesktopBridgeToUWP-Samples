using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStoreDemo
{
    public class StartUp
    {
        [STAThread]
        public static void Main(string[] args)
        {
            SingleInstanceManager wrapper = new SingleInstanceManager();
            wrapper.Run(args);
        }
    }
}
