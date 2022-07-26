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

    public ObstacleAnimationsController obstacleAnimationsController;
    
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

    #endregion
    private void OnPlay()
    {
        ObstacleAnimationsOnPlay();
    }
    private void ObstacleAnimationsOnPlay()
    {
        if (ObstacleType == ObstacleType.Guillotine)
        {
            obstacleAnimationsController.GuillotineMover();
        }
        if (ObstacleType == ObstacleType.Card)
        {
            obstacleAnimationsController.CardMover();
        }
        if (ObstacleType == ObstacleType.Hand)
        {
            obstacleAnimationsController.HandMover();
        }
        if (ObstacleType == ObstacleType.Wall)
        {
            obstacleAnimationsController.WallMover();
        }
    }
}
