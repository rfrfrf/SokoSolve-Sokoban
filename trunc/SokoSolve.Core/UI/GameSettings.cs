using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core.UI
{
    /// <summary>
    /// DTO-style class to capture game settings
    /// </summary>
    public class GameSettings
    {
        public bool StartFullScreen
        {
            get { return startFullScreen; }
            set { startFullScreen = value; }
        }

        public int VolumeMusic
        {
            get { return volumeMusic; }
            set { volumeMusic = value; }
        }

        public int VolumeSound
        {
            get { return volumeSound; }
            set { volumeSound = value; }
        }

        public string MusicDirectory
        {
            get { return musicDirectory; }
            set { musicDirectory = value; }
        }

        private string musicDirectory;

        private bool startFullScreen;
        private int volumeMusic;
        private int volumeSound;
    }
}
