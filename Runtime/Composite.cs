using System;
using System.Collections.Generic;

namespace Oak
{
    public abstract class Composite : ExecutableNode
    {
        public event Action<Task> NextTaskFound;

        protected List<ExecutableNode> children;

        public override void Init(OakContext context)
        {
            base.Init(context);

            foreach (var child in children)
            {
                child.NodeFinished += OnNodeFinished;
                child.Init(context);
            }
        }

        public override void Reset()
        {
            foreach (var child in children)
            {
                child.Reset();
            }
        }

        private void OnNodeFinished(object sender, Node.Status status)
        {
            var nextNode = Compose(status);

            if (nextNode == null)
            {
                Finish(status);
            }
            else
            {
                nextNode.Reset();

                Task task = nextNode as Task;

                if (task == null)
                {
                    task = ((Composite)nextNode).Start();
                }

                NextTaskFound?.Invoke(task);
            }
        }

        public Task Start()
        {
            var nextNode = Compose(Node.Status.Running);

            Task task = nextNode as Task;

            if (task == null)
            {
                task = ((Composite)nextNode).Start();
            }

            return task;
        }

        protected override IEnumerable<Node> Enumerate()
        {
            foreach (var node in base.Enumerate())
            {
                yield return node;
            }

            foreach (var child in children)
            {
                foreach (var node in child)
                {
                    yield return node;
                }
            }
        }

        public abstract ExecutableNode Compose(Node.Status lastTaskStatus);
    }
}
