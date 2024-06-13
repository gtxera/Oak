using System;

namespace Oak
{
    public abstract class Node
    {
        public event Action<Status> NodeFinished;

        protected OakContext context;

        public virtual void Init(OakContext context)
        {
            this.context = context;
        }

        protected void Finish(Status status)
        {
            NodeFinished?.Invoke(status);
        }

        public abstract void Reset();

        public enum Status
        {
            Running,
            Success,
            Failure,
        }
    }
}
