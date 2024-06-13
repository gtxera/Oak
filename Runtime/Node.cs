using System;

namespace Oak
{
    public abstract class Node
    {
        public event Action<Status> NodeFinished;

        public abstract void Reset();

        public enum Status
        {
            Running,
            Success,
            Failure,
        }
    }
}
