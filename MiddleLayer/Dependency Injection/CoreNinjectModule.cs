using Ninject.Modules;
using SQLConnectionLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiddleLayer.Dependency_Injection
{
    public class CoreNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISQLConnection>().To<SQLConnection>();
        }
    }
}
