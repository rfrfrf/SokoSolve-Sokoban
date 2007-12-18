using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Common.FactoryPattern
{
    public interface IFactory<Provider, ProviderContext>
    {
        Provider GetInstance(ProviderContext creationContext);
    }
}
