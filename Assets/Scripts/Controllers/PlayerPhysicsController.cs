using System;
using System.Collections;
using System.Collections.Generic;
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

        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private StackManager stackManager;
        private new Collider Collider;

        #endregion

        #endregion


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                CollectableSignals.Instance.onMoneyCollection?.Invoke();
                other.transform.parent = stackManager.transform;
                stackManager.Colleted.Add(other.gameObject);
                other.tag = "Collected";
            }
        }
    }
}
