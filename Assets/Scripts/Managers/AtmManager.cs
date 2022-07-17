using Controllers;
using DG.Tweening;
using Signals;
using UnityEngine;

namespace Managers
{
    public class AtmManager : MonoBehaviour
    {
        public AtmScoreController atmScoreController;
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
        void OnDeposit(GameObject gameObject)
        {
            Debug.Log("Yes");
            atmScoreController.OnDeposit(gameObject);
            if (gameObject.CompareTag("Player"))
            {
                transform.DOMoveY(transform.position.z+10f, 0.3f);
            }
        }
    }
}

