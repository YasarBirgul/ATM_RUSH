using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Collectable"))
            {
                CollectableSignals.Instance.onMoneyCollection?.Invoke(other.gameObject);
            }        
            if (other.CompareTag("Obstacle"))
            {             
                CollectableSignals.Instance.onPlayerObstacleCollision?.Invoke(gameObject);
            }
            if (other.CompareTag("Atm"))
            {
                CollectableSignals.Instance.onPlayerAtmCollision?.Invoke(gameObject,other.GetComponent<AtmManager>().GetInstanceID());
            } 
            if (other.CompareTag("Conveyor"))
            {
                CollectableSignals.Instance.onFinalAtmCollision?.Invoke(gameObject);
            }
        }
    }
}
