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
            CollectableSignals.Instance.onObstacleCollision += OnObstacleCollision; 
            // CollectableSignals.Instance.onDeposit -= OnDeposit;
        }  
        private void UnsubscribeEvents()
        {
            CollectableSignals.Instance.onMoneyCollection -= OnMoneyCollection;
            CollectableSignals.Instance.onObstacleCollision -= OnObstacleCollision;

        } 
        private void OnDisable()
        {
            UnsubscribeEvents(); 
        }
        
        private void OnMoneyCollection(GameObject self)
        { 
            ScoreUp(self);
        }
        
        private void OnObstacleCollision(GameObject self,int index)
        {
           // ScoreDown(self,index);
        }
        private void ScoreUp(GameObject self)
        {
            
            if (self.CompareTag("Collectable"))
            { 
                _score += 1;
                scoreText.text = _score.ToString();
            }

           
        }
        public void ScoreDownUpdate(int value, GameObject tp)
        {
            minesScore = value;
            ScoreDown(tp,value);
        }
        private void ScoreDown(GameObject self, int minusScore)
        {
            if (self.CompareTag("Collected"))
            {
                _score -= minesScore;
                    
                if (_score <= 0)
                {
                    _score = 0;
                } 
                scoreText.text = _score.ToString();  
            }
        }

        
    }
}

