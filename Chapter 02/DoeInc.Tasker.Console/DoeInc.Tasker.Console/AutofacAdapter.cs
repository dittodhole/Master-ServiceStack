
using Autofac;
using ServiceStack.Configuration;

namespace DoeInc.Tasker.Console
{
    internal class AutofacAdapter : ServiceStack.Configuration.IContainerAdapter, IRelease
    {
        private readonly Autofac.IContainer _container;

        public AutofacAdapter(Autofac.IContainer container)
        {
            this._container = container;
        }

        public T Resolve<T>()
        {
            return this._container.Resolve<T>();
        }

        public T TryResolve<T>()
        {
            T result;
            if (this._container.TryResolve<T>(out result))
            {
                return result;
            }
            return default(T);
        }

        public void Release(object instance)
        {
            throw new System.NotImplementedException();
        }
    }
}
