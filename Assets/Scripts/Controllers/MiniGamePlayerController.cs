using DG.Tweening;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{ 
    public class MiniGamePlayerController:MonoBehaviour
    {
        [SerializeField] private ScoreManager _scoreManager; 
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
            var MiniGameScoreHeight = _scoreManager._score;
            transform.DOMoveY(MiniGameScoreHeight / 2, MiniGameScoreHeight/5).SetEase(Ease.Linear).OnComplete(() =>
            {
                CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
            });
        }
    }
}