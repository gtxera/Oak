using System;
using UnityEngine;

namespace Oak
{
    public class BehaviourTree
    {
        public Action<Node.Status> BehaviourTreeFinished;

        private OakContext _context;

        private GameObject _ownerObject;

        private Task _currentTask;
        private Composite _treeRoot;

        private bool _isPaused;

        public BehaviourTree(GameObject ownerObject, Composite treeRoot, bool startImmediate)
        {
            _ownerObject = ownerObject;

            _treeRoot = treeRoot;
            _treeRoot.NodeFinished += (_, status) => BehaviourTreeFinished?.Invoke(status);

            _isPaused = true;

            if (startImmediate)
            {
                Start();
            }
        }
        public void Execute()
        {
            if (!ShouldExecute())
            {
                return;
            }

            _currentTask.Update();
        }

        public bool OwnerObjectExists()
        {
            return _ownerObject != null;
        }

        public void Start()
        {
            _treeRoot.Init(_context);

            foreach (var node in _treeRoot)
            {
                if (node is Decorator)
                {

                }
            }

            _currentTask = _treeRoot.Start();

            _isPaused = false;
        }

        public void Stop()
        {
            _treeRoot.Reset();

            _isPaused = true;
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void Unpause()
        {
            _isPaused = false;
        }

        public void SetCurrentTask(Task task)
        {
            _currentTask = task;
        }
        private bool ShouldExecute()
        {
            return !_isPaused && _ownerObject.activeInHierarchy;
        }
    }
}
