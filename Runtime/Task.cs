
namespace Oak
{
    public abstract class Task : ExecutableNode
    {
        public void Update()
        {
            var status = DoTask();

            if (status != Node.Status.Running)
            {
                Finish(status);
                Reset();
            }
        }

        protected abstract Node.Status DoTask();
    }
}
