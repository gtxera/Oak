using System;

namespace Oak
{
    public abstract class Node
    {
        public event EventHandler<Status> NodeFinished;

        protected OakContext context;

        public int Depth { get; private set; }
        public int Priority { get; private set; }

        private bool _invertResult;

        public virtual void Init(OakContext context)
        {
            this.context = context;
        }

        protected void Finish(Status status)
        {
            if (_invertResult)
            {
                status = status == Status.Success ? Status.Failure : Status.Success;
            }

            NodeFinished?.Invoke(this, status);
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
