using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Common.FactoryPattern
{
    /// <summary>
    /// Define a class factory where the ProviderContext is just the fully qualified string namespace of the class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FactoryStringNamespace<T> : FactoryStrict<T, string>
    {
        /// <summary>
        /// Init the cache with pre-created instances
        /// </summary>    
        protected void Init(string classNamespace)
        {
            GetInstance(classNamespace);
        }

        protected override string GetProviderClassNamespace(string providerContext)
        {
            return providerContext;
        }
    }
}
