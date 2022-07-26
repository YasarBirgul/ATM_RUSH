using UnityEngine;

namespace Managers
{
    public class MiniGameManager : MonoBehaviour
    {
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
        }
        private void UnsubscribeEvents()
        {
            
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
    }
}