using System.Collections;
using System.Collections.Generic;
using Managers;
using Enums;
using Signals;
using DG.Tweening;
using UnityEngine;

public class MiniGameStartCommand : MonoBehaviour
{
   public void StartMiniGame(GameObject miniGamePlayer)
   {
      miniGamePlayer.gameObject.SetActive(true);
      CoreGameSignals.Instance.onSetCameraState?.Invoke(CameraStatesType.DefaultCam);
      
   }
}
