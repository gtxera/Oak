
namespace Oak
{
    public abstract class Task : Node
    {
        public void Update()
        {
            var status = DoTask();

            if (status != Node.Status.Running)
            {
                Finish(status);
            }
        }

        protected abstract Node.Status DoTask();
    }
}
