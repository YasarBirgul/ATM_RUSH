using Controllers;
using DG.Tweening;
using Signals;
using UnityEngine;

namespace Managers
{
    public class MiniGameManager : MonoBehaviour
    {
        
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables
        
        public GameObject MiniGamePlayer;
        
        #endregion

        #region Private Variables
        
        #endregion

        #endregion
        
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            CollectableSignals.Instance.onFinalAtmCollision += OnFinalAtmCollision;
        }
        
        private void UnsubscribeEvents()
        { 
            CollectableSignals.Instance.onFinalAtmCollision -= OnFinalAtmCollision;
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        } 

        #endregion
        void OnFinalAtmCollision(GameObject CollidedActiveObject)
        {
            if (CollidedActiveObject.CompareTag("Player"))
            {
                MiniGamePlayer.SetActive(true);
            }
        }
    }
}