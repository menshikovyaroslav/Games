using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mines.Classes
{
    public static class RegistryMethods
    {
        public static int Width
        {
            get
            {
                var result = 10;
                try
                {
                    var hkcu = Registry.CurrentUser;
                    RegistryKey settingsBranch = hkcu.OpenSubKey("Software\\YarcheMines", true);
                    if (settingsBranch == null)
                    {
                        hkcu.CreateSubKey("Software\\YarcheMines");
                        settingsBranch = hkcu.OpenSubKey("Software\\YarcheMines", true);
                    }

                    var width = settingsBranch.GetValue("Width", "10").ToString();
                    Int32.TryParse(width, out result);

                    settingsBranch?.Close();
                    hkcu?.Close();
                }
                catch (Exception ex)
                {
                }
                return result;
            }
            set
            {
                try
                {
                    var hkcu = Registry.CurrentUser;
                    RegistryKey settingsBranch = hkcu.OpenSubKey("Software\\YarcheMines", true);
                    settingsBranch.SetValue("Width", value, RegistryValueKind.String);
                    settingsBranch?.Close();
                    hkcu?.Close();
                }
                catch (Exception ex)
                {
                }
            }
        }

        public static int Heigth
        {
            get
            {
                var result = 10;
                try
                {
                    var hkcu = Registry.CurrentUser;
                    RegistryKey settingsBranch = hkcu.OpenSubKey("Software\\YarcheMines", true);
                    if (settingsBranch == null)
                    {
                        hkcu.CreateSubKey("Software\\YarcheMines");
                        settingsBranch = hkcu.OpenSubKey("Software\\YarcheMines", true);
                    }

                    var width = settingsBranch.GetValue("Heigth", "10").ToString();
                    Int32.TryParse(width, out result);

                    settingsBranch?.Close();
                    hkcu?.Close();
                }
                catch (Exception ex)
                {
                }
                return result;
            }
            set
            {
                try
                {
                    var hkcu = Registry.CurrentUser;
                    RegistryKey settingsBranch = hkcu.OpenSubKey("Software\\YarcheMines", true);
                    settingsBranch.SetValue("Heigth", value, RegistryValueKind.String);
                    settingsBranch?.Close();
                    hkcu?.Close();
                }
                catch (Exception ex)
                {
                }
            }
        }

        public static int Bombs
        {
            get
            {
                var result = 10;
                try
                {
                    var hkcu = Registry.CurrentUser;
                    RegistryKey settingsBranch = hkcu.OpenSubKey("Software\\YarcheMines", true);
                    if (settingsBranch == null)
                    {
                        hkcu.CreateSubKey("Software\\YarcheMines");
                        settingsBranch = hkcu.OpenSubKey("Software\\YarcheMines", true);
                    }

                    var width = settingsBranch.GetValue("Bombs", "20").ToString();
                    Int32.TryParse(width, out result);

                    settingsBranch?.Close();
                    hkcu?.Close();
                }
                catch (Exception ex)
                {
                }
                return result;
            }
            set
            {
                try
                {
                    var hkcu = Registry.CurrentUser;
                    RegistryKey settingsBranch = hkcu.OpenSubKey("Software\\YarcheMines", true);
                    settingsBranch.SetValue("Bombs", value, RegistryValueKind.String);
                    settingsBranch?.Close();
                    hkcu?.Close();
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
