using GyorokRentService.Validations;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyorokRentService.Dependency_Injection
{
    public class CoreNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<FieldNotEmpty>().To<FieldNotEmpty>().InSingletonScope();
            Bind<FieldOnlyLong>().To<FieldOnlyLong>().InSingletonScope();
        }
    }
}
