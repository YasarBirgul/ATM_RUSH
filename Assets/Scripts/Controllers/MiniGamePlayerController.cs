using DG.Tweening;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{ 
    public class MiniGamePlayerController:MonoBehaviour
    {
        #region Self Variables
    
        #region Public Variables

        #endregion
    
        #region Serialized Variables
        
        [SerializeField] private ScoreManager scoreManager;
        
        #endregion
        
        #region Private Variables

        #endregion
    
        #endregion
         
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnMiniGame += OnMiniGame;
        }
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.OnMiniGame -= OnMiniGame;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        void OnMiniGame(int integer)
        {
            var MiniGameScoreHeight = scoreManager.Score;
            transform.DOMoveY(MiniGameScoreHeight / 2, MiniGameScoreHeight/10).SetDelay(1.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
            });
        }
    }
}