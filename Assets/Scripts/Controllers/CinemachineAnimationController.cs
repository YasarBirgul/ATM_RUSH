using UnityEngine;

namespace Controllers
{
    public class CinemachineAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        public Animator Animator;
        
        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        #endregion

        #endregion
        private void Awake()
        {
            Animator = GetComponent<Animator>();
        }
    }
}
