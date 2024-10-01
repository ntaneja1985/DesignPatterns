using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            //Example of Simple Factory
            Bind<IDal>().To<SqlDal>().Named("S");
            Bind<IDal>().To<OracleDal>().Named("O");
            Bind<MySupplier>().ToSelf();
        }
    }
}
