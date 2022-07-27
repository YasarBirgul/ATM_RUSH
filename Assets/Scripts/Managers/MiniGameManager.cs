using Controllers;
using DG.Tweening;
using Signals;
using UnityEngine;

namespace Managers
{
    public class MiniGameManager : MonoBehaviour
    {
        private int id;

        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += OnLevelInitialize;
            CoreGameSignals.Instance.OnMiniGame += OnMiniGame;
        }
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= OnLevelInitialize;
            CoreGameSignals.Instance.OnMiniGame -= OnMiniGame;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        void OnLevelInitialize()
        {
            
        } void OnMiniGame(int index)
        {
            MiniGameBlockMoves(index);
        }

        private void MiniGameBlockMoves(int index)
        {
            id = GetComponent<MiniGameManager>().GetInstanceID();
            Debug.Log(id);
            if (id == index)
            {
                transform.DOMoveZ(transform.position.z - 5, 0.5f);
            }
        }
    }
}