using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace #{companycode}.#{appcode}.#{modulecode}.#{functioncode}.DAC
{
    using #{companycode}.#{appcode}.#{modulecode}.#{functioncode}.SPI;

    /// <summary>
    /// 数据访问接口
    /// </summary>
    public interface I#{domainname}Dao
    {
        /// <summary>
        /// 通过主键查询模型数据
        /// </summary>
        #{domainname}Model Get#{domainname}ModelById(string id);

        /// <summary>
        /// 分页查询模型数据
        /// </summary>
        IList<#{domainname}Model> GetPaged#{domainname}Model(IDictionary<string,object> filter);

        /// <summary>
        /// 查询模型数据总条数
        /// </summary>
        int GetAll#{domainname}Count(IDictionary<string,object> filter);

        /// <summary>
        /// 插入CASP_PlatformMerchant表数据
        /// </summary>
        string Insert#{domainname}Entity(#{domainname}Entity entity);

        /// <summary>
        /// 更新CASP_PlatformMerchant表数据
        /// </summary>
        int Update#{domainname}Entity(#{domainname}Entity entity);
    }
}