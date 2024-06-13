using System.Collections.Generic;

namespace Oak
{
    public abstract class Composite : Node
    {
        protected List<Node> children;

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
            foreach (var chid in children)
            {
                chid.Reset();
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

                context.SetCurrentTask(task);
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

        public abstract Node Compose(Node.Status lastTaskStatus);
    }
}
