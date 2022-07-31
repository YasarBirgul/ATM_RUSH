using UnityEngine;
using Signals;
using TMPro;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Self Variables
        
        #region Public Variables
        
        public TextMeshPro ScoreText;
        public float Score = 0;
        
        #endregion
        
        #region Serialized Variables
       
        #endregion
        
        #region Private Variables

        private int _minesScore;
        
        #endregion
        
        #endregion


        #region EventSubscription
        
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
        #endregion
        
        private void OnMoneyCollection(GameObject self)
        { 
            ScoreUp(self);
        }
        
        private void ScoreUp(GameObject self)
        {
            if (self.CompareTag("Collectable"))
            { 
                Score += (int)self.GetComponent<CollectableManager>().StateData;
                ScoreText.text = Score.ToString();
            }
        }
        public void OnScoreDown(int DecreaseScoreValue)
        {
            Score -=DecreaseScoreValue;
                    
            if (Score <= 0)
            {
                Score = 0;
            } 
            ScoreText.text = Score.ToString();  
        }
    }
}

