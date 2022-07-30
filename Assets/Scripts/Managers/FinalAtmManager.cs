using System;
using Controllers;
using Signals;
using UnityEngine;

public class FinalAtmManager : MonoBehaviour
{
    [SerializeField] private FinalAtmAnimationCommand finalAtmAnimationCommand;
    
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

    private void Awake()
    {
        finalAtmAnimationCommand = new FinalAtmAnimationCommand();
    }

    void OnUpdateAtmScore(GameObject CollidedObject)
    {
        finalAtmAnimationCommand.ShakeAtm(transform);
    }

}
