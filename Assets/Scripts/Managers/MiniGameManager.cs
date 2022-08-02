using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class MiniGameManager : MonoBehaviour
    {
        
        #region Self Variables

        #region Public Variables
        
        public GameObject MiniGameBlocks;
        
        public GameObject MiniGamePLayer;

        public GameObject Player;
        
        #endregion

        #region Serialized Variables

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
               Invoke("PlayMiniGame",1.5f);
            }
        }

        private void PlayMiniGame()
        {
            Player.SetActive(false);
            MiniGameBlocks.SetActive(true);
            MiniGamePLayer.SetActive(true);
            CoreGameSignals.Instance.onSetCameraState?.Invoke(CameraStatesType.DefaultCam);
            CoreGameSignals.Instance.OnMiniGame?.Invoke(0);
        }
    }
}