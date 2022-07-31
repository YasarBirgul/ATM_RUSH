using Controllers;
using DG.Tweening;
using Signals;
using UnityEngine;

public class FinalAtmManager : MonoBehaviour
{
    #region Self Variables
    
    #region Public Variables

    #endregion
    
    #region Serialized Variables
    
    [SerializeField] private FinalAtmAnimationCommand finalAtmAnimationCommand;
    
    #endregion
    
    #region Private Variables

    #endregion
    
    #endregion
    
    
    private void OnEnable()
    {
        SubscribeEvents();
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }
    private void SubscribeEvents()
    {
        CollectableSignals.Instance.onFinalAtmCollision += OnUpdateAtmScore;
    }

    private void UnSubscribeEvents()
    {
        CollectableSignals.Instance.onFinalAtmCollision -= OnUpdateAtmScore;
    }
    void OnUpdateAtmScore(GameObject CollidedObject)
    { 
        if (CollidedObject.CompareTag("Collected"))
        {
            FinalAtmShake();
            PullMoneyAndDestroy(CollidedObject);
        }
    }

    private void FinalAtmShake()
    {
        finalAtmAnimationCommand = new FinalAtmAnimationCommand();
        finalAtmAnimationCommand.ShakeAtm(transform);
    }
    private void PullMoneyAndDestroy(GameObject CollidedObject)
    {
        CollidedObject.transform.DOMoveX(-6, 1).OnComplete(() =>
        {
            Destroy(CollidedObject);
        });
    }
}
