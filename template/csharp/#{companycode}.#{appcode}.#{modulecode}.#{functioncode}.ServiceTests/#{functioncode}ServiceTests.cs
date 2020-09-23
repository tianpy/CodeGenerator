using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace #{companycode}.#{appcode}.#{modulecode}.#{functioncode}.ServiceTests
{
    using #{companycode}.#{appcode}.#{modulecode}.#{functioncode}.SPI;
    using #{companycode}.#{appcode}.#{modulecode}.Common;

    [TestClass()]
    public class #{functioncode}ServiceTests:BaseTests<I#{functioncode}Service>
    {
        [TestMethod()]
        public void Get#{domainname}ModelByIdTest()
        {
            var id = "";
            var model = Service.Get#{domainname}ModelById(id);

            DebugOutput(id, model);

            Assert.IsNotNull(model);        
        }

        [TestMethod()]
        public void GetPaged#{domainname}ModelListTest()
        {
            var filter = new #{domainname}Filter()
            {
                Code = "",
                Keywords = "",
                TimeType = 1,
                StartTime = new DateTime(2019, 1, 1, 0, 0, 0),
                EndTime = DateTime.Now,
                Page = 1,
                Rows = 10
            };

            var list = Service.GetPaged#{domainname}ModelList(filter);

            DebugOutput(filter, list);
            Assert.IsTrue(list.Total > 0);
        }

        [TestMethod()]
        public void Add#{domainname}Test()
        {
            var entity = new #{domainname}Entity()
            {
                DelFlag = "0",
            };

            var result = Service.Add#{domainname}(entity);

            DebugOutput(entity, result);     
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void Update#{domainname}Test()
        {
            var entity = new #{domainname}Entity()
            {
                ID="",
            };

            Service.Update#{domainname}(entity);

            DebugOutput(entity, null);
        }

        [TestMethod()]
        public void Delete#{domainname}Test()
        {
            var id = "";

            Service.Delete#{domainname}(id);

            DebugOutput(id, null);
        }
    }
}