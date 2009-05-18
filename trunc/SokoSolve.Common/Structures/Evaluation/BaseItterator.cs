using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SokoSolve.Common.Structures.Evaluation
{
    /// <summary>
    /// Abstract class to provide a common starter implementation of <see cref="IEvaluationStrategyItterator<T>"/> 
    /// </summary>
    public abstract class BaseItterator<T> : IEvaluationStrategyItterator<T>, IDisposable
    {
      

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="getDepth"></param>
        protected  BaseItterator(GetDepthDelegate getDepth)
        {
            GetDepth = getDepth;
            exitConditions = new ItteratorExitConditions();
        }

        /// <summary>
        /// Should the timer be used (on Init) Default is OFF
        /// </summary>
        public bool TimerEnabled
        {
            get { return timerEnabled; }
            set { timerEnabled = value; }
        }

        /// <summary>
        /// Exit Status. This is the result of the last GetNext() opperation. <see cref="GetNext"/>
        /// </summary>
        public EvalStatus ExitStatus
        {
            get { return exitStatus; }
        }

        /// <summary>
        /// Exit conditions (limits to the search algorithm)
        /// </summary>
        public ItteratorExitConditions ExitConditions
        {
            get { return exitConditions; }
            set { exitConditions = value; }
        }

        /// <summary>
        /// Allow the depth of a node to be retrieved. This allows parameterisation of INode instead of ITreeNode
        /// </summary>
        /// <param name="EvalNode"></param>
        /// <returns></returns>
        public delegate int GetDepthDelegate(INode<T> EvalNode);

        /// <summary>
        /// Provide simple debug information
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} LastStatus:{1} Itt:{2} Depth:{3}", this.GetType().Name,  exitStatus, currentItteration, currentMaxDepth);
        }


        #region IEvaluationStrategyItterator<T> Members

        /// <summary>
        /// Get the next node to evaluate, based on depth-first.
        /// </summary>
        /// <returns>null means exit evaluation</returns>
        public abstract INode<T> GetNext(out EvalStatus Status);

        /// <summary>
        /// Add another node to evaluate
        /// </summary>
        /// <param name="NewEvalNode"></param>
        abstract public void Add(INode<T> NewEvalNode);


        /// <summary>
        /// Remove a node (it has been evaluated)
        /// </summary>
        /// <param name="EvalNode"></param>
        public abstract void Remove(INode<T> EvalNode);


        /// <summary>
        /// Return a copy of the evaluation list
        /// </summary>
        /// <returns>A copy of the nodes</returns>
        public abstract List<INode<T>> GetEvalList();

        /// <summary>
        /// Remove the eval list
        /// </summary>
        public abstract void Clear();
        

        /// <summary>
        /// Start the itterator (this will also start the inner clock)
        /// </summary>
        public void Init()
        {
            elapsedSeconds = 0;
            if (timerEnabled)
            {
                secondTick = new Timer(TimerSecondTick, null, (int)TimeSpan.FromSeconds(1).TotalMilliseconds, (int)TimeSpan.FromSeconds(1).TotalMilliseconds);    
            }
        }


        public void Dispose()
        {
            if (secondTick != null)
            {
                secondTick.Dispose();
                secondTick = null;
            }
        }

        /// <summary>
        /// The timer tick method
        /// </summary>
        /// <param name="notUsed"></param>
        private void TimerSecondTick(object notUsed)
        {
            elapsedSeconds++;
            System.Console.Write(".");
        }

        #endregion

        /// <summary>
        /// Helper method to provide basic (non-sucess) exit conditions. 
        /// NOTE: This will also increment the itterators counter
        /// </summary>
        /// <param name="node">Next Node for evaluation (result of GetNext)</param>
        /// <returns>true is exit condition found</returns>
        protected bool CheckExitConditions(INode<T> node)
        {
            if (node == null) return false;

            // Depth
            if (GetDepth != null)
            {
                int depth = GetDepth(node);
                if (depth > currentMaxDepth) currentMaxDepth = depth;
                if (currentMaxDepth > exitConditions.MaxDepth)
                {
                    return true;
                }
            }

            // Itterations
            currentItteration++;
            if (currentItteration > exitConditions.MaxItterations)
            {
                return true;
            }

            // Time
            if (elapsedSeconds > exitConditions.MaxTimeSecs)
            {
                
                return true;
            }

            return false;
        }

        protected GetDepthDelegate GetDepth;
        protected ItteratorExitConditions exitConditions;
        protected EvalStatus exitStatus;
        protected int currentMaxDepth;
        protected int currentItteration;
        private Timer secondTick;
        private uint elapsedSeconds;
        private bool timerEnabled;
    }
}
