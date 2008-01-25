using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace SokoSolve.Core.Analysis.Solver
{
    /// <summary>
    /// Measures time intervals for perfmon
    /// </summary>
    public class CodeTimer : IDisposable
    {
        [DllImport("KERNEL32")]
        private static extern bool QueryPerformanceCounter(
          out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);

        private long start = 0;
        private long stop = 0;
        private long frequency;

        string name;



        /// <summary>
        /// Default constructor passing a global variable to hold a value that will be used to calculate a duration in nanoseconds. 
        /// </summary>
        public CodeTimer(string timerName)
        {
            this.name = timerName;
            if (QueryPerformanceFrequency(out frequency) == false)
            {
                // Frequency not supported
                throw new Win32Exception();
            }

            Debug.WriteLine(string.Format("Starting {0}", timerName));
            Start();
        }

        /// <summary>
        /// Start method that gets the current value from QueryPerformanceCounter. Uses a global variable to store the retrieved value. 
        /// </summary>
        public void Start()
        {
            start = 0;
            stop = 0;
            QueryPerformanceCounter(out start);
        }

        /// <summary>
        /// Stop method that gets the current value from QueryPerformanceCounter. Uses a global variable to store the retrieved value. 
        /// </summary>
        public void Stop()
        {
            QueryPerformanceCounter(out stop);
        }

        /// <summary>
        /// TicksDuration method that returns the ticks for all operations
        /// </summary>
        /// <returns></returns>
        public long TicksOperationDuration()
        {
            if (stop == 0) Stop();
            return stop - start;
        }

        /// <summary>
        /// Duration method that accepts the number of iterations as an argument and returns a duration per operation value. 
        /// </summary>
        /// <param name="iterations"></param>
        /// <returns></returns>
        public double Duration(int iterations)
        {
            if (stop == 0) Stop();
            return ((((double)(TicksOperationDuration()) / (double)frequency) / (double)iterations));
        }

        /// <summary>
        /// Number of iterations per second achieved.
        /// </summary>
        /// <param name="iterations"></param>
        /// <returns></returns>
        public double IterationsPerSecond(int iterations)
        {
            if (stop == 0) Stop();
            return (double)iterations / (((double)(TicksOperationDuration()) / (double)frequency));
        }

        #region IDisposable Members

        ///<summary>
        ///Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ///</summary>
        ///<filterpriority>2</filterpriority>
        public virtual void Dispose()
        {
            if (stop == 0) Stop();
            Debug.WriteLine(string.Format("Complete {0} in {1:0.000000}", name, this.Duration(1)));
        }

        #endregion
    }
}
