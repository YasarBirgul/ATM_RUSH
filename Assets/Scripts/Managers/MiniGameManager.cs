using Signals;
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
            CoreGameSignals.Instance.onLevelInitialize += OnLevelInitialize;
        }
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= OnLevelInitialize;
            
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        void OnLevelInitialize()
        {
            
        }
    }
}