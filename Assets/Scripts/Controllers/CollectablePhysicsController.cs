using System;
using Managers;
using Signals;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;

namespace Controllers
{
    public class CollectablePhysicsController : MonoBehaviour
    {
        #region Self Variables
        #region Public Variables
        public int index;
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
            if (other.CompareTag("Obstacle"))
            {             
                CollectableSignals.Instance.onObstacleCollision?.Invoke(gameObject);
            }
        }
    }
}