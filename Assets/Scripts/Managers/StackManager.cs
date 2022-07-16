using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Extentions;
using Keys;
using Signals;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{ 
    public class StackManager : MonoSingleton<StackManager>
    {
        public List<GameObject> Collected = new List<GameObject>();
        private bool isCollected;
        private bool isPickedUp;
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CollectableSignals.Instance.onMoneyCollection += OnMoneyCollection;
            CollectableSignals.Instance.onObstacleCollision += OnObstacleCollision;
        }

        private void UnsubscribeEvents()
        {
            CollectableSignals.Instance.onMoneyCollection -= OnMoneyCollection;
            CollectableSignals.Instance.onObstacleCollision -= OnObstacleCollision;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }


        private void OnMoneyCollection()
        {
            StartCoroutine(StackScaleUp());
        }
        
        private void OnObstacleCollision()
        {
            
        }

        private void Update()
        {
            StackLerpMove();
        }

        public IEnumerator StackScaleUp()
        {
            yield return new WaitForSeconds(0.1f);
            for (int i = Collected.Count - 1; i >= 0; i--)
            {
                Collected[i].transform.DOScale(1.2f, 0.2f).SetLoops(2, LoopType.Yoyo);
                yield return new WaitForSeconds(0.2f);
            }
            isPickedUp = true;

        }
        void StackLerpMove()
        {
            if (Collected!=null)
            {
                for (int i = Collected.Count-1; i >= 1; i--)
                {
                    Collected[i].transform.DOMoveX(Collected[i - 1].transform.position.x, 0.1f);
                }   
            }
        }
    }
}