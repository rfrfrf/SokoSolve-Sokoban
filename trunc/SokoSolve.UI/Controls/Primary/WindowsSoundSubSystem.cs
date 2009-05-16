using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using SokoSolve.Core;
using SokoSolve.Core.UI;

namespace SokoSolve.UI.Controls.Primary
{
    /// <summary>
    /// Implemenent a VERY simple sound & music interface. THis handles pre-loaded resouces and playing.
    /// </summary>
    class WindowsSoundSubSystem  : ISoundSubSystem
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public WindowsSoundSubSystem()
        {
            player = new WindowsMediaPlayerWrapper();
           
        }

        #region ISoundSubSystem Members

        /// <summary>
        /// Load a resource by filename
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ISoundHandle GetHandle(string name)
        {
            if (name.ToLower().EndsWith("wav"))
            {
                SoundFX sfx = new SoundFX(name);
                sfx.Load();
                return sfx;
            }

            if (name.ToLower().EndsWith("mp3"))
            {
                string file = FileManager.GetContent("$music", name);
                if (!File.Exists(file)) return null;

                SoundMusic msc = new SoundMusic(player.Open(file));
                return msc;
            }

            return null;
        }

        /// <summary>
        /// Play a sound
        /// </summary>
        /// <param name="sound"></param>
        public void PlaySound(ISoundHandle sound)
        {
            if (sound == null) return;

            SoundFX sfx = sound as SoundFX;
            if (sfx != null)
            {
                sfx.Play();
            }
        }

        /// <summary>
        /// Play music
        /// </summary>
        /// <param name="music"></param>
        public void PlayMusic(ISoundHandle music)
        {
            SoundMusic msc = music as SoundMusic;
            if (msc != null)
            {
                player.Play(msc.Res);    
            }
        }

        /// <summary>
        /// Stop Music and Sound
        /// </summary>
        public void Stop()
        {
            player.Stop();
        }

        /// <summary>
        /// Set the volume level for sound.
        /// MinVolume=0, MaxVolume=100
        /// </summary>
        /// <remarks>
        /// For more information see 
        /// http://www.geekpedia.com/tutorial176_Get-and-set-the-wave-sound-volume.html
        /// </remarks>
        public int VolumeSound
        {
            get
            {
                // By the default set the volume to 0
                 uint CurrVol = 0;
                 // At this point, CurrVol gets assigned the volume
                 waveOutGetVolume(IntPtr.Zero, out CurrVol);
                 // Calculate the volume
                 ushort CalcVol = (ushort)(CurrVol & 0x0000ffff);
                 // Get the volume on a scale of 1 to 10 (to fit the trackbar)
                 return CalcVol / (ushort.MaxValue / 100);
            }
            set
            {
                // Calculate the volume that's being set
                int NewVolume = ((ushort.MaxValue / 100) * value);
                // Set the same volume for both the left and the right channels
                uint NewVolumeAllChannels = (((uint)NewVolume & 0x0000ffff) | ((uint)NewVolume << 16));
                // Set the volume
                waveOutSetVolume(IntPtr.Zero, NewVolumeAllChannels);
            }
        }

        /// <summary>
        /// Set the volume level for music
        /// </summary>
        public int VolumeMusic
        {
            get { return player.Volume; }
            set { player.Volume = value; }
        }

        #endregion

        /// <summary>
        /// C# interop to get the current application volume level
        /// </summary>
        /// <param name="hwo"></param>
        /// <param name="dwVolume"></param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        private static extern int waveOutGetVolume(IntPtr hwo, out uint dwVolume);

        /// <summary>
        /// C# interop to set the current application volume level
        /// </summary>
        /// <param name="hwo"></param>
        /// <param name="dwVolume"></param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        private static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

        private WindowsMediaPlayerWrapper player;        
    }

    class SoundFX : System.Media.SoundPlayer, ISoundHandle
    {
        public SoundFX(string soundLocation) : base(soundLocation)
        {
        }

        public SoundFX()
        {
        }
    }

    class SoundMusic: ISoundHandle
    {
        public SoundMusic(WindowsMediaPlayerResource res)
        {
            this.res = res;
        }

        public WindowsMediaPlayerResource Res
        {
            get { return res; }
            set { res = value; }
        }

        private WindowsMediaPlayerResource res;
    }
}
