using System;
using Cinemachine;
using Data.ValueObject;
using Enums;
using UnityEngine;
using Signals;
using TMPro;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Self Variables
        #region Public Variables

        public CollectableType _CollectableType;
        
        #endregion
        #region Serialized Variables
        public TextMeshPro scoreText;
        #endregion
        #region Private Variables

        private int minesScore;
        
        
        public float _score = 0;
        #endregion
        #endregion


       

        private void OnEnable()
        {
            SubscribeEvents();
        } 
        private void SubscribeEvents()
        {
            CollectableSignals.Instance.onMoneyCollection += OnMoneyCollection;
            ScoreSignals.Instance.onScoreDown += OnScoreDown;
        }  
        private void UnsubscribeEvents()
        {
            CollectableSignals.Instance.onMoneyCollection -= OnMoneyCollection;
            ScoreSignals.Instance.onScoreDown -= OnScoreDown;
        } 
        private void OnDisable()
        {
            UnsubscribeEvents(); 
        }
        
        private void OnMoneyCollection(GameObject self)
        { 
            ScoreUp(self);
        }
        
        private void ScoreUp(GameObject self)
        {
            if (self.CompareTag("Collectable"))
            { 
                _score += (int)self.GetComponent<CollectableManager>().StateData;
                scoreText.text = _score.ToString();
            }
        }
        private void OnScoreDown(int Value)
        {
            _score -= Value;
                    
            if (_score <= 0)
            {
                _score = 0;
            } 
            scoreText.text = _score.ToString();  
        }
    }
}

