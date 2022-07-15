using System;
using UnityEngine;
using Signals;
using TMPro;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Self Variables
        #region Public Variables
        private int score=0;

        #endregion
        #region Serialized Variables

        [SerializeField] private TextMeshPro scoreText;
        #endregion
    
        #endregion
        private void OnEnable()
        {
            SubscribeEvents();
        }
    
        private void SubscribeEvents()
        {
            CollectableSignals.Instance.onMoneyCollection += OnScoreUp;
        }
    
        private void UnsubscribeEvents()
        {
            CollectableSignals.Instance.onMoneyCollection -= OnScoreUp;
        }
    
        private void OnDisable()
        {
            UnsubscribeEvents(); 
        }
        private void OnScoreUp()
        {
                scoreText.text = score.ToString();
                score = score + 1;
        }
    }
}

