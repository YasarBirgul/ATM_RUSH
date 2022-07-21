using Managers;
using UnityEngine;

namespace Controllers
{
   public class PlayerAnimationController : MonoBehaviour
   {
      #region Self Variables

      #region Serialized Variables

      [SerializeField] private PlayerManager playerManager;
      [SerializeField] private Animator animator;

      #endregion

      #endregion
      
      public void RunPlayerMovementAnimation()
      {
         Debug.Log("yes");
         animator.SetBool("Run",true);
      }
   
      public void IdlePlayerMovementAnimation()
      {
         animator.SetBool("Run",false);
      }
   }
}
