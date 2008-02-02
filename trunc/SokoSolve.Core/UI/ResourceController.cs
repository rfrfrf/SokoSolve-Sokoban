using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.FactoryPattern;

namespace SokoSolve.Core.UI
{
    public sealed class ResourceController : IFactory<ResourceFactory, string>
    {
        private static ResourceController singleton = new ResourceController();
        private ResourceFactory defaultFactory;
         private ISoundSubSystem sound;
      

        public void Init(ISoundSubSystem soundSys)
        {
            sound = soundSys;
            defaultFactory = new ResourceFactory(FileManager.getContent("$DefaultResources.xml"), sound);
        }

       
        static public ResourceController Singleton
        {
            get
            {
                return singleton;
            }
        }

        public ResourceFactory GetInstance(string creationContext)
        {
            
            // "Default"
            return defaultFactory;
        }
    }
}
