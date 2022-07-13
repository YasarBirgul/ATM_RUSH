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
                other.transform.parent=stackManager.transform;
                other.gameObject.AddComponent<Rigidbody>().isKinematic = true;
                other.gameObject.GetComponent<Collider>().isTrigger = true;
                other.tag = "Collected";
                stackManager.Colleted.Add(other.gameObject);
            }
        }
    }
}
