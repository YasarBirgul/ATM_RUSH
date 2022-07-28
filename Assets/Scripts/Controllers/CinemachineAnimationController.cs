using UnityEngine;

namespace Controllers
{
    public class CinemachineAnimationController : MonoBehaviour
    {
        public Animator _animator;
        
        
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        // private void Update()
        // {
        //     // if (Input.GetKey(KeyCode.Space))
        //     // {
        //     //     CameraChange();
        //     // }
        //     
        // }
        
        
        // public void CameraChange()
        // {
        //     if (_animator.)
        //     {
        //         _animator.Play("FirstCamera");
        //     }
        //     else
        //     {
        //         _animator.Play("PlayerCamera");
        //     }
        //
        //     
        // }
    }
}
