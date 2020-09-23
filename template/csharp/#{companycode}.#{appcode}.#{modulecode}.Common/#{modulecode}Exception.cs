using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace #{companycode}.#{appcode}.#{modulecode}.Common
{
    using Teld.Core.Error;

    /// <summary>
    /// 异常基类
    /// </summary>
    [Serializable]
    public class #{modulecode}Exception : TeldException
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public #{modulecode}Exception()
            : base()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="excptionCode"></param>
        /// <param name="level"></param>
        public #{modulecode}Exception(string excptionCode, TeldExceptionLevel level = TeldExceptionLevel.Error)
            : base(excptionCode, level)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="exceptionCode"></param>
        /// <param name="innerException"></param>
        /// <param name="level"></param>
        public #{modulecode}Exception(string exceptionCode, Exception innerException, TeldExceptionLevel level = TeldExceptionLevel.Error)
            : base(exceptionCode, innerException, level)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="exceptionCode"></param>
        /// <param name="message"></param>
        /// <param name="level"></param>
        public #{modulecode}Exception(string exceptionCode, string message, TeldExceptionLevel level = TeldExceptionLevel.Error)
            : base(exceptionCode, message, level)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="exceptionCode"></param>
        /// <param name="innerException"></param>
        /// <param name="context"></param>
        /// <param name="level"></param>
        public #{modulecode}Exception(string exceptionCode, Exception innerException, IDictionary<string, string> context, TeldExceptionLevel level = TeldExceptionLevel.Error)
            : base(exceptionCode, innerException, context, level)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="exceptionCode"></param>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        /// <param name="level"></param>
        public #{modulecode}Exception(string exceptionCode, string message, Exception innerException, TeldExceptionLevel level = TeldExceptionLevel.Error)
            : base(exceptionCode, message, innerException, level)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="exceptionCode"></param>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <param name="level"></param>
        public #{modulecode}Exception(string exceptionCode, string message, IDictionary<string, string> context, TeldExceptionLevel level = TeldExceptionLevel.Error)
            : base(exceptionCode, message, context, level)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="exceptionCode"></param>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        /// <param name="context"></param>
        /// <param name="level"></param>
        public #{modulecode}Exception(string exceptionCode, string message, Exception innerException, IDictionary<string, string> context, TeldExceptionLevel level = TeldExceptionLevel.Error)
            : base(exceptionCode, message, innerException, context, level)
        {
        }

        /// <summary>
        /// 获取对象数据
        /// </summary>
        /// <param name="info">序列化信息</param>
        /// <param name="context">上下文</param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="info">序列化信息</param>
        /// <param name="context">上下文</param>
        protected #{modulecode}Exception(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.Code = info.GetString("ExceptionCode");
            this.Level = (TeldExceptionLevel)info.GetSingle("Level");
        }
    }
}
