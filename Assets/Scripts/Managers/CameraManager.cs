using System;
using Cinemachine;
using Controllers;
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
        
         private Animator _animator;
        #endregion

        #region Private Variables

        [ShowInInspector] private Vector3 _initialPosition;

        public CameraStatesType cameraStatesType = CameraStatesType.InitCam;

        #endregion

        #endregion

        #region Event Subscriptions

        private void Awake()
        {
            
             _animator = GetComponent<Animator>();
             CameraChange(cameraStatesType);
             GetInitialPosition();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += SetCameraTarget;
            CoreGameSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
            CoreGameSignals.Instance.onReset += OnReset;
            
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= SetCameraTarget;
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
            virtualCamera.Follow = playerManager;
            


        }

        private void OnReset()
        {
            virtualCamera.Follow = null;
            virtualCamera.LookAt = null;
            OnMoveToInitialPosition();
        }
        
        public void CameraChange(CameraStatesType cameraStatesType)
        {
            if (cameraStatesType == CameraStatesType.InitCam)
            {
                _animator.Play("CameraManager");
                
                cameraStatesType = CameraStatesType.DefaultCam;
                Debug.Log(cameraStatesType);
            }
            else if (cameraStatesType == CameraStatesType.DefaultCam)
            {
                
                _animator.Play("CM vcam1");
                cameraStatesType = CameraStatesType.FinalCam;
            }

            
        }
    }
}