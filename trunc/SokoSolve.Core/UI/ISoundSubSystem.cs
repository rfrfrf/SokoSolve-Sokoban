using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core.UI
{
    public interface ISoundHandle
    {
        
    }

    /// <summary>
    /// A minimal interface to allow different sound systems to be used
    /// </summary>
    public interface ISoundSubSystem
    {
        ISoundHandle GetHandle(string name);
        void PlaySound(ISoundHandle sound);
        void PlayMusic(ISoundHandle music);
    }
}
