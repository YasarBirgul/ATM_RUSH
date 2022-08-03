using Controllers;
using DG.Tweening;
using Signals;
using UnityEngine;

namespace Managers
{
    public class AtmManager : MonoBehaviour
    {
        #region Self Variables
    
        #region Public Variables
        
        public AtmScoreController AtmScoreController;
        
        #endregion
    
        #region Serialized Variables

        #endregion
    
        #region Private Variables
        
        private FinalAtmAnimationCommand _finalAtmAnimationCommand;
        
        private int _instanceid;
        
        #endregion
    
        #endregion
        
        private void Awake()
        {
            _instanceid = GetComponent<AtmManager>().GetInstanceID();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }
    
        private void SubscribeEvents()
        {
            CollectableSignals.Instance.onDeposit += OnDeposit;
            CollectableSignals.Instance.onPlayerAtmCollision += OnCollision;

        }
        private void UnsubscribeEvents()
        {
            CollectableSignals.Instance.onDeposit -= OnDeposit;
            CollectableSignals.Instance.onPlayerAtmCollision -= OnCollision;
    
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        } 
        private void OnDeposit(GameObject gameObject,int id)
        {
            AtmScoreController.OnDeposit(gameObject);
            _finalAtmAnimationCommand = new FinalAtmAnimationCommand();
            
            if (gameObject.CompareTag("Collected"))
            {
                _finalAtmAnimationCommand.ShakeAtm(transform);   
            }
        }
        private void OnCollision(GameObject gameObject,int id)
        {
            if (id == _instanceid)
            {
                if (gameObject.CompareTag("Player"))
                {
                    transform.DOMoveY(-5f, 0.3f).SetEase(Ease.OutBounce);
                }
            }
        }
    }
}

