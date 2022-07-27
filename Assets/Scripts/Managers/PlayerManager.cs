using System;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Keys;
using Signals;
using UnityEngine;
using UnityEngine.Jobs;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public PlayerData Data;

        #endregion

        #region Serialized Variables

        [Space][SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerAnimationController playerAnimationController;
        [SerializeField] private ScoreManager _scoreManager;
        

        #endregion

        
        #endregion


        private void Awake()
        {
            Data = GetPlayerData();
            SendPlayerDataToControllers();
        }



        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").Data;

        private void SendPlayerDataToControllers()
        {
            movementController.SetMovementData(Data.MovementData);
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputTaken += OnActivateMovement;
            InputSignals.Instance.onInputReleased += OnDeactiveMovement;
            InputSignals.Instance.onInputDragged += OnGetInputValues;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
            CollectableSignals.Instance.onObstacleCollision += OnObstacleCollision;
            CollectableSignals.Instance.onFinalAtmCollision += OnFinalAtmCollision;
            CoreGameSignals.Instance.OnMiniGame += OnMiniGame;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= OnActivateMovement;
            InputSignals.Instance.onInputReleased -= OnDeactiveMovement;
            InputSignals.Instance.onInputDragged -= OnGetInputValues;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
            CollectableSignals.Instance.onObstacleCollision -= OnObstacleCollision;
            CollectableSignals.Instance.onFinalAtmCollision -= OnFinalAtmCollision;
            CoreGameSignals.Instance.OnMiniGame -= OnMiniGame;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        #region Movement Controller


        private void OnFinalAtmCollision(GameObject Player)
        {
            if (Player.CompareTag("Player"))
            {
                movementController.IsReadyToPlay(false);
                playerAnimationController.IdlePlayerMovementAnimation();
                transform.DOMoveZ(transform.position.z + 10f, 0.2f).SetEase(Ease.InOutExpo);
                
            }
        }

        private void OnActivateMovement()
        {
            movementController.EnableMovement();
        }

        private void OnDeactiveMovement()
        {
            movementController.DeactiveMovement();
        }

        private void OnGetInputValues(HorizontalInputParams inputParams)
        {
            movementController.UpdateInputValue(inputParams);
        }

        #endregion

        private void OnObstacleCollision(GameObject self,int index)
        {
            movementController.PlayerPushBack(self);
        }

        private void OnPlay()
        {
            movementController.IsReadyToPlay(true);
            playerAnimationController.RunPlayerMovementAnimation();
            
        }

        private void OnLevelSuccessful()
        {
            movementController.IsReadyToPlay(false);
        }

        private void OnLevelFailed()
        {
            movementController.IsReadyToPlay(false);
        }
        
        private void OnReset()
        {
            movementController.OnReset();
        }
        private void OnMiniGame(int id)
        {
            var MiniGameHight = _scoreManager._score;
            transform.DOMoveY(MiniGameHight/10, MiniGameHight/100);
        }
    }
}