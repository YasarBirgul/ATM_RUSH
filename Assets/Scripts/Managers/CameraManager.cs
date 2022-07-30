using System;
using Cinemachine;
using Controllers;
using Managers;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        public CinemachineVirtualCamera virtualCamera;
        public CinemachineVirtualCamera MiniGameCamera;
        
        private  Animator _animator;
       
        #endregion

        #region Private Variables

        [ShowInInspector] private Vector3 _initialPosition;
        
       
        
        private CinemachineTransposer _miniGameTransposer;

        private CameraStatesType cameraStatesType = CameraStatesType.InitCam;

        #endregion

        #endregion

        #region Event Subscriptions

        private void Awake()
        {
            
             _animator = GetComponent<Animator>();
             _miniGameTransposer = MiniGameCamera.GetCinemachineComponent<CinemachineTransposer>();
             GetInitialPosition();
            
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += SetCameraTarget;
            CoreGameSignals.Instance.onSetCameraState += OnCameraChange;
            CoreGameSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
            CoreGameSignals.Instance.onReset += OnReset;
            //CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            
            
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= SetCameraTarget;
            CoreGameSignals.Instance.onSetCameraState -= OnCameraChange;
            CoreGameSignals.Instance.onSetCameraTarget -= OnSetCameraTarget;
            CoreGameSignals.Instance.onReset -= OnReset;
            //CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion


        private void GetInitialPosition()
        {
            _initialPosition = transform.localPosition;
        }

        private void OnMoveToInitialPosition()
        {
            transform.localPosition = _initialPosition;
        }

        private void SetCameraTarget()
        {
            CoreGameSignals.Instance.onSetCameraTarget?.Invoke();
            OnCameraChange(CameraStatesType.InitCam);
            
        }

        private void OnSetCameraTarget()
        {
            var playerManager = FindObjectOfType<PlayerManager>().transform;
            virtualCamera.Follow = playerManager;
        }

        private void OnReset()
        {
            virtualCamera.Follow = null;
            virtualCamera.LookAt = null;
            OnMoveToInitialPosition();
        }

        // private void OnNextLevel()
        // {
        //     OnCameraChange(CameraStatesType.InitCam);
        // }
        
        public void OnCameraChange(CameraStatesType cameraState)
        {
            if (cameraState == CameraStatesType.InitCam)
            {
                cameraStatesType = CameraStatesType.DefaultCam;
                //cameraStatesType = CameraStatesType.DefaultCam;
                _animator.Play("CameraManager"); //CM vcam1
            }
           
            else if (cameraState == CameraStatesType.DefaultCam) // currentstate = cameraStatesType
            {
                cameraStatesType = CameraStatesType.FinalCam;
                var _fakePlayer = GameObject.FindGameObjectsWithTag("MiniGamePlayer");
                //MiniGameCamera.m_Follow = _fakePlayer.tra
               
                _animator.Play("FinalCamera"); //CameraManager
            }
            // else if (cameraStatesType == CameraStatesType.FinalCam)
            // {
            //     cameraStatesType = CameraStatesType.FinalCam;
            //     var miniGamePlayer = GameObject.FindGameObjectsWithTag("MiniGamePlayer");
            //     _animator.Play("FinalCamera");
            // }
        }
        
    }
}