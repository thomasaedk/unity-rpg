using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        private IAction currentAction;
        
        public void StartAction(IAction action)
        {
            if (currentAction == action) return;
            if (currentAction != null)
            {
                // Cancel the previous action:
                currentAction.Cancel();
            }
            // Perform current action:
            currentAction = action;
        }
    }
}