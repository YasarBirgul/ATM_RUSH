using UnityEngine;
using DG.Tweening;

namespace Controllers
{
   public class FinalAtmAnimationCommand
   {
      #region Self Variables

      #region Public Variables

      #endregion

      #region Serialized Variables

      #endregion

      #region Private Variables
      
      private Tween _tween;
      
      #endregion

      #endregion
      public void ShakeAtm(Transform Atm)
      {
         if (_tween != null)
            _tween.Kill(true);
         _tween = Atm.DOShakeScale(0.1f, 0.1f, 1);
      }
   }
}

