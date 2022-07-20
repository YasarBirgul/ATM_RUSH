using System;
using System.Net.NetworkInformation;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;
using DG.Tweening;

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

        public GameObject go;


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
                    CollectableSignals.Instance.onObstacleCollision += OnObstacleCollision;

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

        #endregion


        private void Awake()
        {
            StateData = GetCollectableStateData();
        }

        private CollectableType GetCollectableStateData() =>
            Resources.Load<CD_Collectable>("Data/CD_Collectable").CollectableData.CollectableType;
        

        private void OnMoneyCollection(GameObject self)
        {
           // İndex ataması
        }
        private void OnObstacleCollision(GameObject self)
        {
            // Fizik controlden Para yok olacak 
        }
        public void OnUpgradeMoney()
        {
            OnChangeCollectableState(StateData);
        }

        public void OnMoveMoney()
        {
            OnMoveMoneyFinalState();
        }
        public void OnChangeCollectableState(CollectableType _collectableTypes)
        {
            if (_collectableTypes == CollectableType.Money)
            {
                StateData = CollectableType.Gold;
                money.SetActive(false);
                gold.SetActive(true);
            }
        
            else if(_collectableTypes == CollectableType.Gold)
            {
                StateData = CollectableType.Diamond;
                gold.SetActive(false);
                diamond.SetActive(true);
            }
        }

        public void OnMoveMoneyFinalState()
        {
            StackManager.Instance.Collected.Remove(gameObject);
            transform.DOMoveX(transform.position.x - 10, 1);
            transform.DOMoveZ(transform.position.z , 1);
        }
        
    }
}