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

        /// <summary>
        /// This is primarily for sound systems
        /// </summary>
        protected ResourceController()
        {
            defaultFactory = new ResourceFactory(FileManager.GetContent("$DefaultResources.xml"), new MockSoundSystem());
        }
      

        public void Init(ISoundSubSystem soundSys)
        {
            sound = soundSys;
            defaultFactory = new ResourceFactory(FileManager.GetContent("$DefaultResources.xml"), sound);
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
