using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class CollectableManager : MonoBehaviour
    {

        public CollectableData CollectableData;
      //  public CollectablePhysicsController CollectablePhysicsController;

        
        #region Event Subscription

                private void OnEnable()
                {
                    SubscribeEvents();
                }
                private void SubscribeEvents()
                {
                    CollectableSignals.Instance.onMoneyCollection += OnMoneyCollection;
                    CollectableSignals.Instance.onObstacleCollision += OnObstacleCollision;
                    CollectableSignals.Instance.onUpgradeMOney += OnUpgradeMoney;
                    CollectableSignals.Instance.onChangeState += OnChangeState;
                    CollectableSignals.Instance.onDeposit += OnDeposit;
                }
        
                private void UnsubscribeEvents()
                { 
                    CollectableSignals.Instance.onMoneyCollection -= OnMoneyCollection;
                    CollectableSignals.Instance.onObstacleCollision -= OnObstacleCollision;
                    CollectableSignals.Instance.onUpgradeMOney -= OnUpgradeMoney;
                    CollectableSignals.Instance.onChangeState -= OnChangeState;
                    CollectableSignals.Instance.onDeposit -= OnDeposit;
                }
        
                private void OnDisable()
                {
                    UnsubscribeEvents();
                } 

        #endregion
       
        private void OnMoneyCollection()
        {
           // CollectablePhysicsController.
        }
        private void OnObstacleCollision()
        {
            // Fizik controlden Para yok olacak 
        }
        private void OnUpgradeMoney()
        {
            // Fizk controlden Paralar dönüşecek
        }

        private void OnChangeState()
        {
            // Listeden dağıtılacak kısım.
        }
        private void OnDeposit()
        {
            // Para yok olacak, atmye yatacak
        }
    }
}