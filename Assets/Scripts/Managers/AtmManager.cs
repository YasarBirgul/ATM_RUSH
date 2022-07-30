using Controllers;
using DG.Tweening;
using Signals;
using UnityEngine;

namespace Managers
{
    public class AtmManager : MonoBehaviour
    {
        public AtmScoreController atmScoreController;
        private FinalAtmAnimationCommand FinalAtmAnimationCommand;

        private int instanceid;
        
        private void Awake()
        {
            instanceid = GetComponent<AtmManager>().GetInstanceID();
            Debug.Log(instanceid);
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
        private void OnDeposit(GameObject gameObject,GameObject Collided,int id)
        {
            atmScoreController.OnDeposit(gameObject);
            FinalAtmAnimationCommand = new FinalAtmAnimationCommand();

            if (gameObject.CompareTag("Collected"))
            {
                FinalAtmAnimationCommand.ShakeAtm(transform);   
            }

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

