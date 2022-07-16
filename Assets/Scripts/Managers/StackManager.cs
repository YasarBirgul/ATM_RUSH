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
             CollectableScaleUp();
        }

        private void OnObstacleCollision()
        {
           
        }

        private void Update()
        {
            StackLerpMove();
        }

        void StackLerpMove()
        {
            if (Collected.Count > 1)
            {
                for (int i = 1; i < Collected.Count; i++)
                {
                    var FirstBall = Collected.ElementAt(i - 1);
                    var SectBall = Collected.ElementAt(i);

                    SectBall.transform.DOMoveX(FirstBall.transform.position.x, 15 * Time.deltaTime);
                    SectBall.transform.DOMoveZ(FirstBall.transform.position.z + 1.5f, 15 * Time.deltaTime);
                }
            }
        }
        public void CollectableScaleUp()
        {
            for (int i = Collected.Count - 1; i >= 0; i--)
            {
                int index = i;
                Vector3 scale = Vector3.one * 2;
                Collected[index].transform.DOScale(scale, 0.2f).OnComplete(() => {Collected[index].transform.DOScale(Vector3.one, 0.2f);});
                return;
            }
        }
    }
}