using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using WMPLib;

namespace SokoSolve.UI.Controls.Primary
{
        /// <summary>
        /// This class wraps the Windows Media Player (WMP) COM Libraries and exposes a 
        /// simple API for playing music. 
        /// <list type="">
        ///    <item>Open an MP3 file and play</item>
        ///    <item>With completed play detection</item>
        /// </list>
        /// </summary>
        /// <remarks>
        /// Required References:
        /// WMPLib (COM References) 
        /// C:\WINDOWS\SYSTEM32\WMPLib.DLL
        /// </remarks>
        public class WindowsMediaPlayerWrapper : IDisposable
        {
            /// <summary>
            /// Default Constructor
            /// </summary>
            public WindowsMediaPlayerWrapper()
            {
                player = new WindowsMediaPlayerClass();
                player.PlayStateChange += new _WMPOCXEvents_PlayStateChangeEventHandler(player_PlayStateChange);
            }

            /// <summary>
            /// Open a WMP Resource
            /// </summary>
            /// <param name="filename"></param>
            /// <returns></returns>
            public WindowsMediaPlayerResource Open(string filename)
            {
                WindowsMediaPlayerResource res = new WindowsMediaPlayerResource();
                res.fileName = filename;
                res.media = player.newMedia(res.fileName);
                return res;
            }

           
            /// <summary>
            /// Play a media file
            /// </summary>
            /// <param name="res">WMP resource</param>
            public void Play(WindowsMediaPlayerResource res)
            {
                player.currentMedia = res.media;
                player.controls.play();

                current = res;

                return;
            }
            
            /// <summary>
            /// Overloaded. Play a media file
            /// </summary>
            /// <param name="filename"></param>
            public void Play(string filename)
            {
                WindowsMediaPlayerResource res = Open(filename);
                if (res != null)
                {
                    Play(res);
                }
            }

            /// <summary>
            /// Stop Playing
            /// </summary>
            public void Stop()
            {
                player.controls.stop();
            }

            /// <summary>
            /// Volume level
            /// </summary>
            public int Volume
            {
                get { return player.settings.volume; }
                set { player.settings.volume = value; }
            }

            /// <summary>
            /// Get the actual UI control, allows the player to be imbedded if nessesary
            /// </summary>
            /// <returns></returns>
            public WindowsMediaPlayer GetControl()
            {
                return player;   
            }

            /// <summary>
            /// Fired when the current music is finished
            /// </summary>
            public event EventHandler OnComplete;

            /// <summary>
            /// Capture player state changes
            /// </summary>
            void player_PlayStateChange(int newState)
            {
                if (prevState == 8 && newState == 9 && current != null)
                {
                    // Tell the resource
                    current.FireComplete();

                    // Tell the player wrapper
                    if (OnComplete != null)
                    {
                        OnComplete(this, new EventArgs());
                    }
                    current = null;
                }

                prevState = newState;
            }


            #region IDisposable Members

            ///<summary>
            ///Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            ///</summary>
            ///<filterpriority>2</filterpriority>
            public void Dispose()
            {
                Stop();

            }

            #endregion

            private WindowsMediaPlayerClass player;
            private WindowsMediaPlayerResource current;
            private int prevState;
            
            

        }
    }
