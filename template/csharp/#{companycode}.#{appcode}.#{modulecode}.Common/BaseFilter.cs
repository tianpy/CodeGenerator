using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace #{companycode}.#{appcode}.#{modulecode}.Common
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 查询条件基类
    /// </summary>
    [Serializable]
    public class BaseFilter
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string Keywords { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 时间类型
        /// 1：创建时间，2：修改时间，...
        /// </summary>
        public int TimeType { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public string DelFlag
        {
            get { return "0"; }
        }

        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页行数
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// 转成字典
        /// </summary>
        public virtual Dictionary<string, object> ToDictionary()
        {
            return JsonConvert.DeserializeObject<Dictionary<string,object>>(JsonConvert.SerializeObject(this));
        }
    }
}
