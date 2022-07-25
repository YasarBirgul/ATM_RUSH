using System;
using UnityEngine;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {
        private Animator _animator;
        private bool _mainCamera = true;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                CameraChange();
            }
            
        }

        public void CameraChange()
        {
            if (_mainCamera)
            {
                _animator.Play("CM vcam1");
            }
            else
            {
                _animator.Play("CameraManager");
            }

            _mainCamera = !_mainCamera;
        }
    }
}
