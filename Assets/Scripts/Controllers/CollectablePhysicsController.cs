using Managers;
using Signals;
using UnityEngine;


namespace Controllers
{
    public class CollectablePhysicsController : MonoBehaviour
    {
        #region Self Variables
       
        #region Public Variables

        public CollectableManager CollectableManager;
        
        #endregion
        
        #region Serialized Variables
        
        #endregion
        
        #region Private Variables
        
        #endregion
        
        #endregion
        private void OnTriggerEnter(Collider other)
        { 
            if(other.CompareTag("Collectable"))
            { 
                CollectableSignals.Instance.onMoneyCollection?.Invoke(other.gameObject);
            }        
            else if (other.CompareTag("Obstacle"))
            {
                CollectableSignals.Instance.onObstacleCollision?.Invoke(gameObject,transform.GetSiblingIndex());
            }
            else if (other.CompareTag("Atm"))
            {
                CollectableSignals.Instance.onDeposit?.Invoke(gameObject,transform.GetSiblingIndex());
            }
            else if (other.CompareTag("UpgradeGate"))
            {        
               CollectableManager.OnUpgradeMoney();
            }
            else if (other.CompareTag("Conveyor"))
            {
                CollectableSignals.Instance.onFinalAtmCollision?.Invoke(gameObject);
            }
        }
    }
}