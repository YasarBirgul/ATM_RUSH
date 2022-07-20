using System;
using Controllers;
using DG.Tweening;
using Signals;
using UnityEngine;

namespace Managers
{
    public class AtmManager : MonoBehaviour
    {
        public AtmScoreController atmScoreController;

        private int instanceid;
        
        private void Awake()
        {
            instanceid = gameObject.GetComponent<AtmManager>().GetInstanceID();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }
    
        private void SubscribeEvents()
        {
            CollectableSignals.Instance.onDeposit += OnDeposit;

        }
        private void UnsubscribeEvents()
        {
            CollectableSignals.Instance.onDeposit -= OnDeposit;
    
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        } 
        void OnDeposit(GameObject gameObject,int id)
        {
            atmScoreController.OnDeposit(gameObject);
            if (id == instanceid)
            {
                if (gameObject.CompareTag("Player"))
                {
                    transform.DOMoveY(-5f, 0.3f).SetEase(Ease.OutBounce);
                }
            }
        }
    }
}

