using System;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class CollectablePhysicsController : MonoBehaviour
    {
        #region Self Variables
        #region Public Variables
        #endregion
        #region Serialized Variables
        [SerializeField] private CollectableManager collectableManager;
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private new Collider Collider;
        #endregion
        #endregion
        private void OnTriggerEnter(Collider other)
        { 
            if(other.gameObject.CompareTag("Collectable"))
            {
                CollectableSignals.Instance.onMoneyCollection?.Invoke();
                other.gameObject.transform.position = transform.position + Vector3.forward;
                other.gameObject.tag = "Collected";
            }
        }
    }
}