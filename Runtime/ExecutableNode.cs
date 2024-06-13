using System;
using System.Collections.Generic;

namespace Oak
{
    public abstract class ExecutableNode : Node
    {
        public event EventHandler<Status> NodeFinished;

        protected List<AuxiliaryNode> auxiliaryNodes = new();

        protected void Finish(Status status)
        {
            if (InvertResult)
            {
                status = status == Status.Success ? Status.Failure : Status.Success;
            }

            NodeFinished?.Invoke(this, status);
        }

        protected override IEnumerable<Node> Enumerate()
        {
            foreach (var auxNode in auxiliaryNodes)
            {
                yield return auxNode;
            }

            yield return this;
        }
    }
}
