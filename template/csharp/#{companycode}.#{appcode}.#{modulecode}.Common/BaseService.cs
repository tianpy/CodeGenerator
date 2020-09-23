using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace #{companycode}.#{appcode}.#{modulecode}.Common
{
    using Teld.BIZ.Common.Kernel;

    /// <summary>
    /// 服务基类
    /// </summary>
    public abstract class BaseService
    {
        /// <summary>
        /// 数据连接字符串
        /// </summary>
        protected abstract string ConnName { get; }

        /// <summary>
        /// 登录用户
        /// </summary>
        protected CurrentUser LoginUser
        {
            get
            {
                return SessionProvider.GetCurrentUser();
            }
        }
    }
}
