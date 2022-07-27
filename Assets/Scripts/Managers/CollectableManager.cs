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
        public GameObject money;
        public GameObject gold;
        public GameObject diamond;
        public ScoreManager ScoreManager;
        
        #endregion
        #region Serialized Variables
        [SerializeField] private CollectablePhysicsController collectablePhysicsController;

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
                other.transform.localPosition = new Vector3(0, 0, 5f);
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
                money.SetActive(false);
                gold.SetActive(true);
                ScoreManager._score += 1;
                ScoreManager.scoreText.text = ScoreManager._score.ToString();
            }
        
            else if(_collectableTypes == CollectableType.Gold)
            {
                StateData = CollectableType.Diamond;
                gold.SetActive(false);
                diamond.SetActive(true);
                ScoreManager._score += 1;
                ScoreManager.scoreText.text = ScoreManager._score.ToString();
            }
        }
    }
}