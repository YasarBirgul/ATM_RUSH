using System;
using Cinemachine;
using UnityEngine;
using Signals;
using TMPro;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Self Variables
        #region Public Variables
        #endregion
        #region Serialized Variables
        [SerializeField] private TextMeshPro scoreText;
        #endregion
        #region Private Variables
        private float _score = 0;
        #endregion
        #endregion



        private void OnEnable()
        {
            SubscribeEvents();
        } 
        private void SubscribeEvents()
        {
            CollectableSignals.Instance.onMoneyCollection += OnScoreUp;                  
            CollectableSignals.Instance.onObstacleCollision += OnScoreDown;
                
        }  
        private void UnsubscribeEvents()
        {
            CollectableSignals.Instance.onMoneyCollection -= OnScoreUp;
            CollectableSignals.Instance.onObstacleCollision -= OnScoreDown;

        } 
        private void OnDisable()
        {
            UnsubscribeEvents(); 
        }

        private void Update()
        { 
            scoreText.text = _score.ToString();
        }

        private void OnScoreUp(GameObject self)
        {
            if (self.CompareTag("Collected"))
            {
                 _score += 1;
            }
           
        }
        private void OnScoreDown(GameObject self,int index)
        {
            if (self.CompareTag("Collected"))
            {
                _score -= 1;
                
                if (_score <= 0)
                {
                    _score = 0;
                }
            }
        }
    }
}

