using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace #{companycode}.#{appcode}.#{modulecode}.Common
{
    using Teld.BIZ.Common.Kernel;
    using Newtonsoft.Json;

    public abstract class BaseTests<T> where T : class
    {
        protected T Service
        {
            get
            {
                return #{modulecode}ServiceFactory<T>.GetService();
            }
        }

        public BaseTests()
        {
            Teld.Core.Kernel.Service.ContextHelper.Set("Teld-ServicePath", AppDomain.CurrentDomain.BaseDirectory + ".\\");

            SessionProvider.SetCurrentUser(new CurrentUser()
            {
                UserName = "张三",
                Account = "zhangsan",
                UserId = "d578d4e7-3861-4f1a-abc2-0b92207eae7b",
                Mobile = "15834341231"
            });
        }

        protected void DebugOutput(object param, object result)
        {
            Debug.WriteLine(string.Format("参数：{0}\r\n结果：{1}", JsonConvert.SerializeObject(param), JsonConvert.SerializeObject(result)));
        }
    }
}
