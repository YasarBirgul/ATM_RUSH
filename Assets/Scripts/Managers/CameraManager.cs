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
        
        #region Public Variables

        #endregion

        #region Serialized Variables

        public CinemachineVirtualCamera VirtualCamera;
        public CinemachineVirtualCamera MiniGameCamera;
        
        
       
        #endregion

        #region Private Variables

        [ShowInInspector] private Vector3 _initialPosition;
        
        private CinemachineTransposer _miniGameTransposer;
        
        private  Animator _animator;

        private CameraStatesType _cameraStatesType = CameraStatesType.InitCam;

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
            
            
            
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= SetCameraTarget;
            CoreGameSignals.Instance.onSetCameraState -= OnCameraChange;
            CoreGameSignals.Instance.onSetCameraTarget -= OnSetCameraTarget;
            CoreGameSignals.Instance.onReset -= OnReset;
            
            
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
            
            
        }

        private void OnSetCameraTarget()
        {
            var playerManager = FindObjectOfType<PlayerManager>().transform;
            VirtualCamera.Follow = playerManager;
        }

        private void OnReset()
        {
            VirtualCamera.Follow = null;
            VirtualCamera.LookAt = null;
            OnMoveToInitialPosition();
        }

        
        
        public void OnCameraChange(CameraStatesType cameraState)
        {
            if (cameraState == CameraStatesType.InitCam)
            {
                _cameraStatesType = CameraStatesType.DefaultCam;
                _animator.Play("CameraManager"); 
            }
            if (cameraState == CameraStatesType.DefaultCam) 
            {
                _cameraStatesType = CameraStatesType.FinalCam;
                var _fakePlayer = GameObject.FindGameObjectWithTag("MiniGamePlayer");
                MiniGameCamera.m_Follow = _fakePlayer.transform;
                _animator.Play("FinalCamera");
            }
            else if (cameraState == CameraStatesType.FinalCam)
            {
                 _cameraStatesType = CameraStatesType.InitCam;
                 _animator.Play("CM vcam1");
            }
        }
    }
}