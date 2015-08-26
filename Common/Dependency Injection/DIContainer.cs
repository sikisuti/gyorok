using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ninject;

namespace Common.Dependency_Injection
{
    public sealed class DIContainer
    {
        private StandardKernel kernel;
        private DIContainer instance = new DIContainer();

        private DIContainer()
        {
            this.kernel = new StandardKernel();
        }

        public DIContainer Instance
        {
            get
            {
                return instance;
            }
        }

        public T Get<T>()
        {
            return this.kernel.Get<T>();
        }
    }
}
