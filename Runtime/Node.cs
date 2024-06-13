using System;

namespace Oak
{
    public abstract class Node
    {
        public event Action<Status> NodeFinished;

        protected OakContext context;

        public void Init(OakContext context)
        {
            this.context = context;

            Init();
        }

        protected void Finish(Status status)
        {
            NodeFinished?.Invoke(status);
        }

        public abstract void Reset();

        public abstract void Init();
        
        public enum Status
        {
            Running,
            Success,
            Failure,
        }
    }
}
