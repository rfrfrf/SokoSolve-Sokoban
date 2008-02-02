using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MediaPlayer;
using SokoSolve.Core;
using SokoSolve.Core.UI;

namespace SokoSolve.UI.Controls.Primary
{
    class WindowsSoundSubSystem  : ISoundSubSystem
    {

        public WindowsSoundSubSystem()
        {
            player = new MediaPlayerClass();
           
        }

        #region ISoundSubSystem Members

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
                string file = FileManager.getContent("$music", name);
                if (!File.Exists(file)) return null;

                SoundMusic msc = new SoundMusic(file);
                return msc;
            }

            return null;
        }

        public void PlaySound(ISoundHandle sound)
        {
            if (sound == null) return;

            SoundFX sfx = sound as SoundFX;
            if (sfx != null)
            {
                sfx.Play();
            }
        }

        public void PlayMusic(ISoundHandle music)
        {
            if (music == null) return;

            try
            {
                // Add a delegate for the PlayStateChange event.

               
               
                player.Open(music.ToString());
                player.Play();
                
                currentMusic = music;
                
              
                string states = player.PlayState.ToString();
            }
            catch(Exception ex)
            {
                throw new Exception("Cannot play music file: " + music.ToString(), ex);
            }
            
        }

      

        void player_EndOfStream(int Result)
        {
            SoundMusic msc = currentMusic as SoundMusic;
            if (msc != null)
            {
                if (msc.OnMusicComplete != null) msc.OnMusicComplete(this, new EventArgs());
            }
        }

        #endregion

        private MediaPlayer.MediaPlayerClass player;
        private ISoundHandle currentMusic;
        
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
        public SoundMusic(string fileName)
        {
            this.fileName = fileName;
        }

        public override string ToString()
        {
            return fileName;
        }

        public EventHandler OnMusicComplete;

        private string fileName;
    }
}
