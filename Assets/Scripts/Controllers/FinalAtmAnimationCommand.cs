using UnityEngine;
using DG.Tweening;

namespace Controllers
{
   public class FinalAtmAnimationCommand
   {
      private Tween _tween;
      
      public void ShakeAtm(Transform Atm)
      {
         if (_tween != null)
            _tween.Kill(true);
         _tween = Atm.DOShakeScale(0.1f, 0.1f, 1);
      }
   }
}

