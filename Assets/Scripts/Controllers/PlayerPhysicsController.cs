using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        [SerializeField] private StackManager stackManager;
        #region Public Variables
        
        #endregion

        #region Serialized Variables
        
        [SerializeField] private PlayerManager playerManager;
        private new Collider Collider;

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
                CollectableSignals.Instance.onObstacleCollision?.Invoke(gameObject);
            }
        }
    }
}
