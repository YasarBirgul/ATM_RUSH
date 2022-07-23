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

        [HideInInspector]
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
          // CollectableSignals.Instance.onDeposit -= OnDeposit;

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
           ScoreDown(self);
        }
        private void ScoreUp(GameObject self)
        {
            
            if (self.CompareTag("Collected"))
            { 
                _score += 1;
                scoreText.text = _score.ToString();
            }

           
        }
        private void ScoreDown(GameObject self)
        {
            if (self.CompareTag("Collected"))
            {
                _score -= 1;
                    
                if (_score <= 0)
                {
                    _score = 0;
                } 
                scoreText.text = _score.ToString();  
            }
        }
    }
}

