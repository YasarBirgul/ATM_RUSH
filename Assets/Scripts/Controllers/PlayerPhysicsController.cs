using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

      
        #region Public Variables
        
        #endregion

        #region Serialized Variables
        
        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Collectable"))
            {
                CollectableSignals.Instance.onMoneyCollection?.Invoke(other.gameObject);
            }        
            if (other.CompareTag("Obstacle"))
            {             
                CollectableSignals.Instance.onObstacleCollision?.Invoke(gameObject,0);
            }
            if (other.CompareTag("Atm"))
            {             
                CollectableSignals.Instance.onDeposit?.Invoke(gameObject,other.GetComponent<AtmManager>().GetInstanceID());
            } 
            if (other.CompareTag("Conveyor"))
            {
                CollectableSignals.Instance.onFinalAtmCollision?.Invoke(gameObject);
            }

            if (other.CompareTag("RankCube"))
            {
                CoreGameSignals.Instance.OnMiniGame?.Invoke(other.GetComponent<MiniGameManager>().GetInstanceID());
            }
        }
    }
}
