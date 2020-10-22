using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mines.Classes
{
    public static class Options
    {
        public static int MapWidth { get { return RegistryMethods.Width; } set { RegistryMethods.Width = value; } }
        public static int MapHeight { get { return RegistryMethods.Heigth; } set { RegistryMethods.Heigth = value; } }
        public static int Bombs { get { return RegistryMethods.Bombs; } set { RegistryMethods.Bombs = value; } }
    }
}
