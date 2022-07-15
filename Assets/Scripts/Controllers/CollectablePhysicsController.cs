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
        
        #endregion
        #region Serialized Variables
        [SerializeField] private CollectableManager collectableManager;
        [SerializeField] private StackManager stackManager;
        
        #endregion
        #region Private Variables
        private float waitTime = 0.3f;
        #endregion
        #endregion
        
        
        private void OnTriggerEnter(Collider other)
        { 
            if(other.CompareTag("Collectable"))
            { 
                CollectableSignals.Instance.onMoneyCollection?.Invoke();
                //other.transform.parent=stackManager.transform;
                other.gameObject.AddComponent<Rigidbody>().isKinematic = true;
                other.gameObject.GetComponent<Collider>().isTrigger = true;
                other.tag ="Collected";
                stackManager.Collected.Add(other.gameObject); 
                SetMoney();
            }        
            if (other.CompareTag("Obstacle"))
            {
                CollectableSignals.Instance.onObstacleCollision?.Invoke();
                Destroy(gameObject);
            }
        }
        
        public void SetMoney()
        {
            for (int i = stackManager.Collected.Count - 1; i >= 0; i--)
            { 
                int index = i;
                Vector3 scale = Vector3.one * 2;
                stackManager.Collected[index].transform.DOScale(scale, 0.2f).OnComplete(() => { stackManager.Collected[index].transform.DOScale(Vector3.one, 0.2f); });
                return;
            }
        }
       
    }
}