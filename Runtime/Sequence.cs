namespace Oak
{
    public class Sequence : Composite
    {
        private int _currentChildIndex;

        public override Node Compose(Status lastTaskStatus)
        {
            if (lastTaskStatus == Node.Status.Failure)
            {
                return null;
            }

            return children[_currentChildIndex++];
        }

        public override void Reset()
        {
            base.Reset();

            _currentChildIndex = 0;
        }
    }
}
