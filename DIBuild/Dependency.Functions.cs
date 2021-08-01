using System;

namespace DIBuild
{
    public partial class Dependency
    {
        private void SetFunc(Func<object> func)
        {
            Func = func;
            IsFuncInstance = true;
        }

        public void SetFuncInstance()
        {
            if (!IsFuncInstance)
                return;

            Instance = Func.Invoke();
            HasInstance = true;
        }

        public void SetInstance(object instance)
        {
            if (ServiceLifeTime == ServiceLifeTime.Transient)
                return;

            Instance = instance;
            HasInstance = true;
        }

        #region Dispose

        private void ReleaseUnmanagedResources()
        {
            if (Instance is IDisposable disposable)
                disposable.Dispose();

            Instance = default;
            HasInstance = false;
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~Dependency()
        {
            ReleaseUnmanagedResources();
        }

        #endregion
    }
}