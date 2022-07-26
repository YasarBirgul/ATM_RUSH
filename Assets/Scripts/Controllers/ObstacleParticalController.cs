using Signals;
using UnityEngine;

namespace Controllers
{
    public class ObstacleParticalController : MonoBehaviour
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