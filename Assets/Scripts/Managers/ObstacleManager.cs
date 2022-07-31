using Enums;
using Signals;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    [Header("Data")] public ObstacleType ObstacleType;
    
    public ObstacleAnimationsController ObstacleAnimationsController;

    #endregion

    #region Serialized Variables
    
    #endregion
    
    #region Private Variables

    #endregion
    
    #region Event Subscription
    private void OnEnable()
    {
        SubscribeEvents();
    }
    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onPlay += OnPlay;
    }
    private void UnsubscribeEvents()
    {
        CoreGameSignals.Instance.onPlay -= OnPlay;
    }
    private void OnDisable()
    {
        UnsubscribeEvents();
    }
    #endregion

    #endregion

    
    private void OnPlay()
    {
        ObstacleAnimationsOnPlay();
    }
    private void ObstacleAnimationsOnPlay()
    {
        if (ObstacleType == ObstacleType.Guillotine)
        {
            ObstacleAnimationsController.GuillotineMover();
        }
        if (ObstacleType == ObstacleType.Card)
        {
            ObstacleAnimationsController.CardMover();
        }
        if (ObstacleType == ObstacleType.Hand)
        {
            ObstacleAnimationsController.HandMover();
        }
        if (ObstacleType == ObstacleType.Wall)
        {
            ObstacleAnimationsController.WallMover();
        }
    }
}
