using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    [Header("Data")] public ObstacleType ObstacleType;

    #endregion

    #region Serialized Variables

    [SerializeField] private List<DOTweenAnimation> DoTweenAnimations;
    #region Event Subscription 
        
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onPlay += OnPlay;
       // CollectableSignals.Instance.onDeposit += OnDeposit;
    }





    private void UnsubscribeEvents()
    {
        CoreGameSignals.Instance.onPlay -= OnPlay;
       // CollectableSignals.Instance.onDeposit -= OnDeposit;
    }
    private void OnDisable()
    {
        UnsubscribeEvents();
    }
    #endregion

    #endregion

    #endregion
    
    
    
    private void OnPlay()
    {
        throw new NotImplementedException();
    }
    
    //bu hep çalışssın.

    private void Start()
    {
       
    }
}
