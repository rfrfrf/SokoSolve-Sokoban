using System;
using System.Collections.Generic;
using System.Text;
using SokoSolve.Common.FactoryPattern;

namespace SokoSolve.Core.UI
{
    public sealed class ResourceFactory : IFactory<ResourceManager, string>
    {
        private static ResourceFactory singleton = new ResourceFactory();
        private ResourceManager defaultManager;

        private ResourceManager gameTiles;

        private ResourceFactory()
        {
            defaultManager = new ResourceManager();
            defaultManager.Load(FileManager.getContent(@"$Graphics\Tiles\Static\StaticSmall-16x16.xml"));

            gameTiles = new ResourceManager();
            gameTiles.Load(FileManager.getContent(@"$Graphics\Tiles\Clean\Clean.xml"));
           
        }


        static public ResourceFactory Singleton
        {
            get
            {
                return singleton;
            }
        }

        public ResourceManager GetInstance(string creationContext)
        {
            if (creationContext == "Default.Tiles")
            {
                return defaultManager;
            }

            if (creationContext == "Default.GameTiles")
            {
                return gameTiles;
            }

            // "Default"
            return defaultManager;
        }
    }
}
