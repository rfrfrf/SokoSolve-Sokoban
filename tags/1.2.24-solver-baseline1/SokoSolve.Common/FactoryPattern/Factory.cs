using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Permissions;
using System.Text;

namespace SokoSolve.Common.FactoryPattern
{
    /// <summary>
    /// In implementation of the factory class design pattern
    /// </summary>
    /// <typeparam name="Provider">Provider which who instances are factory made by this class</typeparam>
    /// <typeparam name="ProviderContext">The key or context used to build a Provider</typeparam>
    public abstract class Factory<Provider, ProviderContext> : IFactory<Provider, ProviderContext>
    {
        private Dictionary<ProviderContext, Provider> cache;
        private object cacheLock = new object();
        public Factory()
        {
            cache = new Dictionary<ProviderContext, Provider>();
        }

        /// <summary>
        /// Get the number of instances in the factory (note this is lazy loading)
        /// </summary>
        public int InstanceCount
        {
            get
            {
                return cache.Count;
            }
        }

        /// <summary>
        /// All instances currently in the cache
        /// </summary>
        public virtual List<Provider> Instances
        {
            get
            {
                List<Provider> allInstances = new List<Provider>(cache.Values);
                return allInstances;
            }
        }

        /// <summary>
        /// Can this factory create an instance for the following context? 
        /// Note: This method will lazy-load to check if this instance exists, it does not just check the cache.
        /// </summary>
        /// <param name="providerContext"></param>
        /// <returns></returns>
        public virtual bool Contains(ProviderContext providerContext)
        {
            return GetInstance(providerContext) != null;
        }

        /// <summary>
        /// Init the cache with pre-created instances
        /// </summary>    
        protected void Init(Provider newProvider, ProviderContext creationContext)
        {
            cache.Add(creationContext, newProvider);
        }

        /// <summary>
        /// Key method. This method is responsible for returning or loading and returning a valid instance of Provider based on ProviderContext
        /// </summary>
        /// <param name="creationContext"></param>
        /// <returns></returns>
        public virtual Provider GetInstance(ProviderContext creationContext)
        {
            lock (cacheLock)
            {
                if (creationContext == null) throw new ArgumentNullException("providerContext");

                // Reflection vodoo and validation
                if (cache.ContainsKey(creationContext))
                {
                    return cache[creationContext];
                }
                else
                {
                    // Create
                    Provider newInstance = CreateInstance(creationContext);
                    if (newInstance == null)
                    {
                        throw new ArgumentNullException("newInstance");
                    }

                    cache.Add(creationContext, newInstance);

                    return newInstance;
                }
            }
        }

        /// <summary>
        /// Default implementation of creation which used reflection to create an instance of Provider
        /// </summary>
        /// <param name="creationContext"></param>
        /// <returns></returns>
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        protected virtual Provider CreateInstance(ProviderContext creationContext)
        {
            try
            {
                Type providerType = GetProviderType(creationContext);
                if (providerType == null) throw new ArgumentNullException("Cannot find type for creationContext");
                Provider newInstance = (Provider)Activator.CreateInstance(providerType);
                if (newInstance == null)
                {
                    throw new ArgumentNullException(string.Format("Cannot create a new instance of {0} of type {1}", creationContext, creationContext.GetType()));
                }
                Type typeProvider = typeof (Provider);
                List<Type> typesCreated = new List<Type>();
                typesCreated.Add(newInstance.GetType());
                typesCreated.AddRange(newInstance.GetType().GetNestedTypes());
                typesCreated.AddRange(newInstance.GetType().GetInterfaces());
                if (!typesCreated.Contains(typeProvider))
                {
                    throw new ArgumentNullException(string.Format("Create new instance, however it is of the wrong type. Expected {0}, but got {1}", typeof(Provider), newInstance.GetType()));
                }
                return newInstance;
            }
            catch (TargetInvocationException tiEx)
            {
                throw new ArgumentException(string.Format("Cannot create a new instance of {0} of type {1}", creationContext, creationContext.GetType())+tiEx.Message, tiEx);
            }
         }

        /// <summary>
        /// Based on the ProviderContext return the Type to be created
        /// </summary>
        /// <param name="providerContext"></param>
        /// <returns></returns>
        protected virtual Type GetProviderType(ProviderContext providerContext)
        {
           string providerNamespace = GetProviderClassNamespace(providerContext);
            if (providerNamespace == null)
            {
                throw new ArgumentNullException("providerNamespace");
            }

            // Try the current assembly
            Type typeResult = Type.GetType(providerNamespace, false, true);
            if (typeResult != null) return typeResult;

            // Try all loaded assemblies
            string searchlist = "";
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                searchlist += asm.FullName + " ";
                typeResult = asm.GetType(providerNamespace, false, true);
                if (typeResult != null) return typeResult;
            }

            throw new Exception("Cannot find type in any of the following assemblies: " + searchlist);
        }

        /// <summary>
        /// Helper method. Converts a ProviderContext into the type namespace for the class to be created. 
        /// This is just a short-cut to using the Type class which is strongly typed, but has more overhead/developer work in creating.
        /// </summary>
        /// <param name="providerContext"></param>
        /// <returns></returns>
        protected virtual string GetProviderClassNamespace(ProviderContext providerContext)
        {
            throw new NotSupportedException("Override GetProviderClassNamespace(...)");
        }

        /// <summary>
        /// Does the cache already contain an Provider for a ProviderContext
        /// </summary>
        /// <param name="providerContext"></param>
        /// <returns></returns>
        protected bool CacheContains(ProviderContext providerContext)
        {
            return cache.ContainsKey(providerContext);
        }
    }
}
