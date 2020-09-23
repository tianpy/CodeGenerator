using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace #{companycode}.#{appcode}.#{modulecode}.#{functioncode}.Service
{
    using #{companycode}.#{appcode}.#{modulecode}.#{functioncode}.SPI;
    using #{companycode}.#{appcode}.#{modulecode}.#{functioncode}.DAC;
    using #{companycode}.#{appcode}.#{modulecode}.Common;

    using Teld.Core.Kernel.Service;
    using Teld.Core.Database.Service;
    using Teld.Core.Error;
    using Teld.BIZ.Common.Kernel;

    /// <summary>
    /// #{functionchinesename}服务实现
    /// </summary>
    [SGService]
    public class #{functioncode}Service : BaseService, I#{functioncode}Service
    {
        protected override string ConnName => "sqlmap_#{functioncode}_wr";

        /// <summary>
        /// 获取#{domainchinesename}
        /// </summary>
        /// <param name="id">#{domainchinesename}内码</param>
        /// <returns></returns>
        [SGService(ServiceID = "")]
        public #{domainname}Model Get#{domainname}ModelById(string id)
        {
            var dao = DaoService.GetInstance(ConnName).GetDao<I#{domainname}Dao>();
            var result = dao.Get#{domainname}ModelById(id);

            return result;
        }

        /// <summary>
        /// 获取#{domainchinesename}列表
        /// </summary>
        /// <param name="filter">#{domainchinesename}查询条件</param>
        /// <returns></returns>
        public PagedListModel<IList<#{domainname}Model>> GetPaged#{domainname}ModelList(#{domainname}Filter filter)
        {
            var dao = DaoService.GetInstance(ConnName).GetDao<I#{domainname}Dao>();
            var dicFilter = filter.ToDictionary();
            var count = dao.GetAll#{domainname}Count(dicFilter);
            if (count > 0)
            {
                var result = dao.GetPaged#{domainname}Model(dicFilter);

                return new PagedListModel<IList<#{domainname}Model>>() { Rows = result, Total = count };
            }
            else
            {
                return new PagedListModel<IList<#{domainname}Model>>() { Rows = new List<#{domainname}Model>(), Total = count };
            }
        }
   
        /// <summary>
        /// 新增#{domainchinesename}
        /// </summary>
        /// <param name="entity">#{domainchinesename}实体</param>
        /// <returns></returns>
        public string Add#{domainname}(#{domainname}Entity entity)
        {
            try
            {
                ValidateModel<#{domainname}Entity, InsertValidateAttribute>.Validate(entity);
            }
            catch (TeldException ex)
            {
                throw new #{modulecode}Exception(ExceptionCode.InvalidParam, ex.Message, ex);
            }

            var dao = DaoService.GetInstance(ConnName).GetDao<I#{domainname}Dao>();

            entity.CreateTime = entity.LastModifyTime = DateTime.Now;
            entity.Creator = entity.LastModifier = LoginUser.UserName;
            entity.DelFlag = "0";

            string id = dao.Insert#{domainname}Entity(entity);
            return id;
        }

        /// <summary>
        /// 修改#{domainchinesename}
        /// </summary>
        /// <param name="entity">#{domainchinesename}实体</param>
        /// <returns></returns>
        public void Update#{domainname}(#{domainname}Entity entity)
        {
            try
            {
                ValidateModel<#{domainname}Entity, UpdateValidateAttribute>.Validate(entity);
            }
            catch (TeldException ex)
            {
                throw new #{modulecode}Exception(ExceptionCode.InvalidParam, ex.Message, ex);
            }

            var dao = DaoService.GetInstance(ConnName).GetDao<I#{domainname}Dao>();

            entity.LastModifyTime = DateTime.Now;
            entity.LastModifier = LoginUser.UserName;

            dao.Update#{domainname}Entity(entity);
        }

        /// <summary>
        /// 删除#{domainchinesename}
        /// </summary>
        /// <param name="id">#{domainchinesename}内码</param>
        public void Delete#{domainname}(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new #{modulecode}Exception(ExceptionCode.InvalidParam, "参数不能为空");
            }

            var dao = DaoService.GetInstance(ConnName).GetDao<I#{domainname}Dao>();

            var entity = new #{domainname}Entity()
            {
                ID = id,
                LastModifyTime = DateTime.Now,
                LastModifier = LoginUser.UserName,
                DelFlag = "1"
            };

            dao.Update#{domainname}Entity(entity);
        }
    }
}
