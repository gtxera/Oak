using System;

namespace Oak
{
    public abstract class Node
    {
        public event Action<Status> NodeFinished;

        public abstract void Reset();

        protected void Finish(Status status)
        {
            NodeFinished?.Invoke(status);
        }

        public enum Status
        {
            Running,
            Success,
            Failure,
        }
    }
}
