using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace #{companycode}.#{appcode}.#{modulecode}.Common
{
    /// <summary>
    /// 应用程序常量
    /// </summary>
    [Serializable]
    public class Application
    {
        public static readonly string AppCode = "#{appcode}";
        public static readonly string AppVersion = "1.0";
        public static readonly string ModuleCode = "#{modulecode}";
    }
}
