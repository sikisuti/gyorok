using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ninject;
using System.Configuration;

namespace Common.Dependency_Injection
{
    public sealed class DIContainer
    {
        private static readonly object SyncRoot = new object();

        private StandardKernel _kernel = new StandardKernel();

        private static DIContainer instance;

        private DIContainer()
        {
            this._kernel = new StandardKernel();
            this._kernel.Load(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static DIContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new DIContainer();
                        }
                    }
                }
                return instance;
            }
        }

        public T Get<T>()
        {
            return this._kernel.Get<T>();
        }
    }
}
