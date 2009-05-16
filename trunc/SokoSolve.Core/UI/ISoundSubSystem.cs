using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core.UI
{
    /// <summary>
    /// A very simple handle interface to capture and pass around sound/music resources
    /// </summary>
    public interface ISoundHandle
    {
        
    }

    /// <summary>
    /// A minimal interface to allow different sound systems to be used
    /// </summary>
    public interface ISoundSubSystem
    {
        /// <summary>
        /// Resource load function
        /// </summary>
        /// <param name="name">fileName</param>
        /// <returns>music/sound handle</returns>
        ISoundHandle GetHandle(string name);

        /// <summary>
        /// Play a sound resource
        /// </summary>
        /// <param name="sound">handle</param>
        void PlaySound(ISoundHandle sound);

        /// <summary>
        /// Play a music resource
        /// </summary>
        /// <param name="music">handle</param>
        void PlayMusic(ISoundHandle music);

        /// <summary>
        /// Stop all music and sound
        /// </summary>
        void Stop();

        /// <summary>
        /// Volume level for sound resources
        /// </summary>
        int VolumeSound { get; set; }

        /// <summary>
        /// Volume level for music resources
        /// </summary>
        int VolumeMusic { get; set; }
    }

    public class MockSoundSystem : ISoundSubSystem
    {
        public ISoundHandle GetHandle(string name)
        {
            return null;
        }

        public void PlaySound(ISoundHandle sound)
        {
            
        }

        public void PlayMusic(ISoundHandle music)
        {
            
        }

        public void Stop()
        {
           
        }

        public int VolumeSound
        {
            get { return 0; }
            set { }
        }

        public int VolumeMusic
        {
            get { return 0; }
            set {  }
        }
    }
}
