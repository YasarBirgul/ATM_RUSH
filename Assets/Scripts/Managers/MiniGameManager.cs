using System;
using DG.Tweening;
using Signals;
using UnityEngine;

namespace Managers
{
    public class MiniGameManager : MonoBehaviour
    {
        private int index;
        
        #region Event Subscription
        

        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += OnLevelInitialize;
            CoreGameSignals.Instance.onMiniGame -= OnMiniGame;
        }
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= OnLevelInitialize;
            CoreGameSignals.Instance.onMiniGame -= OnMiniGame;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        void OnLevelInitialize()
        {
            
        }

        private void OnMiniGame(int id)
        {
            index = GetComponent<MiniGameManager>().GetInstanceID();
            if (id == index)
            {
                transform.DOMoveZ(-5f, 0.02f).SetEase(Ease.InBounce);
            }
        }
    }
}