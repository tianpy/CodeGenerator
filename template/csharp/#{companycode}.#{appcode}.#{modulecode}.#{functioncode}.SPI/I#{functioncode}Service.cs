using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace #{companycode}.#{appcode}.#{modulecode}.#{functioncode}.SPI
{
    using Teld.BIZ.Common.Kernel;
    using Teld.Core.Kernel.Service;

    /// <summary>
    /// #{functionchinesename}服务接口
    /// </summary>
    [SGService]
    public interface I#{functioncode}Service
    {
        /// <summary>
        /// 获取#{domainchinesename}
        /// </summary>
        /// <param name="id">#{domainchinesename}内码</param>
        /// <returns></returns>
        #{domainname}Model Get#{domainname}ModelById(string id);

        /// <summary>
        /// 获取#{domainchinesename}列表
        /// </summary>
        /// <param name="filter">#{domainchinesename}查询条件</param>
        /// <returns></returns>
        PagedListModel<IList<#{domainname}Model>> GetPaged#{domainname}ModelList(#{domainname}Filter filter);
   
        /// <summary>
        /// 新增#{domainchinesename}
        /// </summary>
        /// <param name="entity">#{domainchinesename}实体</param>
        /// <returns></returns>
        string Add#{domainname}(#{domainname}Entity entity);

        /// <summary>
        /// 修改#{domainchinesename}
        /// </summary>
        /// <param name="entity">#{domainchinesename}实体</param>
        /// <returns></returns>
        void Update#{domainname}(#{domainname}Entity entity);

        /// <summary>
        /// 删除#{domainchinesename}
        /// </summary>
        /// <param name="id">#{domainchinesename}内码</param>
        void Delete#{domainname}(string id);
    }
}
