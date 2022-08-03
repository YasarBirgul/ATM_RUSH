using Managers;
using UnityEngine;

namespace Controllers
{
   public class PlayerAnimationController : MonoBehaviour
   {
      #region Self Variables
    
      #region Public Variables

      #endregion
    
      #region Serialized Variables
      
      [SerializeField] private Animator animator;
      
      #endregion
    
      #region Private Variables

      #endregion
    
      #endregion
      
      public void RunPlayerMovementAnimation()
      {
         animator.SetBool("Run",true);
      }
   
      public void IdlePlayerMovementAnimation()
      {
         animator.SetBool("Run",false);
      }
      
   }
}
