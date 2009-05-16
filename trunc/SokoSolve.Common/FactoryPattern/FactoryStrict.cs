using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Common.FactoryPattern
{
    /// <summary>
    /// Implement a strict factory.
    /// Allow sealed creation (a marker afterwhich no calls to CreateInstance are allowed)
    /// GetInstance() will throw an exception is not found (vs creations or returning null)
    /// </summary>
    /// <typeparam name="Provider"></typeparam>
    /// <typeparam name="ProviderContext"></typeparam>
    public abstract class FactoryStrict<Provider, ProviderContext> : Factory<Provider, ProviderContext>
    {

        bool creationSealed = false;

        /// <summary>
        /// When set to true, this will stop the creation of new instances (via CreateInstance)
        /// </summary>
        protected bool CreationSealed
        {
            get { return creationSealed; }
            set { creationSealed = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Never null</returns>
        protected override Provider CreateInstance(ProviderContext creationContext)
        {
            if (creationSealed)
            {
                throw new Exception("This factory is sealed for new instances. See the property this.CreationSealed");
            }

            Provider provider = base.CreateInstance(creationContext);
            if (provider == null) throw new ArgumentNullException("Cannot create instance for creationContext");
            return provider;
        }

        /// <summary>
        /// Can this factory create an instance for the following context? 
        /// Note: This method will lazy-load to check if this instance exists, it does not just check the cache.
        /// </summary>
        /// <param name="providerContext"></param>
        /// <returns></returns>
        public virtual new bool Contains(ProviderContext providerContext)
        {
            return base.CacheContains(providerContext);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Never null</returns>
        public override Provider GetInstance(ProviderContext creationContext)
        {
            Provider provider = base.GetInstance(creationContext);
            if (provider == null) throw new ArgumentNullException("Could not get Provider based on creationContext");
            return provider;
        }


    }
}

