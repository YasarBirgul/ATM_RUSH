using Controllers;
using Data.UnityObject;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class CollectableManager : MonoBehaviour
    {
        #region Self Variables
        
        #region Public Variables
        
        public CollectableType StateData;
        
        public GameObject Money;
        
        public GameObject Gold;
        
        public GameObject Diamond;
        
        public ScoreManager ScoreManager;
        
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
            CollectableSignals.Instance.onMoneyCollection += OnMoneyCollection;

        }
        private void UnsubscribeEvents()
        { 
            CollectableSignals.Instance.onMoneyCollection -= OnMoneyCollection;
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        } 

        #endregion


        private void Awake()
        {
            StateData = GetCollectableStateData();
        }

        private CollectableType GetCollectableStateData() =>
            Resources.Load<CD_Collectable>("Data/CD_Collectable").CollectableData.CollectableType;
        

        private void OnMoneyCollection(GameObject other)
        {
            AddOnStack(other);
        } 
        private static void AddOnStack(GameObject other)
        {
            if (other.CompareTag("Collectable"))
            {
                other.tag = "Collected";
                other.transform.localPosition = new Vector3(0, 0, 3f);
            }
        }
        public void OnUpgradeMoney()
        {
            OnChangeCollectableState(StateData);
        }
        public void OnChangeCollectableState(CollectableType _collectableTypes)
        {
            if (_collectableTypes == CollectableType.Money)
            {
                StateData = CollectableType.Gold;
                Money.SetActive(false);
                Gold.SetActive(true);
                ScoreManager.Score += 1;
                ScoreManager.ScoreText.text = ScoreManager.Score.ToString();
            }
        
            else if(_collectableTypes == CollectableType.Gold)
            {
                StateData = CollectableType.Diamond;
                Gold.SetActive(false);
                Diamond.SetActive(true);
                ScoreManager.Score += 1;
                ScoreManager.ScoreText.text = ScoreManager.Score.ToString();
            }
        }
    }
}