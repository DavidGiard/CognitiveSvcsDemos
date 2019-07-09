using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecognizeTextWin
{
    static class Utilities
    {
        public static string GetKey()
        {
            string computerVisionKey = ConfigurationManager.AppSettings["ComputerVisionKey"];
            return computerVisionKey;
        }
    }
}
