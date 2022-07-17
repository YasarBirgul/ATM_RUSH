﻿using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Extentions;
using Signals;
using UnityEngine;

namespace Managers
{
    public class StackManager : MonoSingleton<StackManager>
    {
        #region Self Variables
        #region Public Variables
        public List<GameObject> Collected = new List<GameObject>();
        #endregion
        #region Serialized Variables
        #endregion
        #endregion
        #region Event Subscription 
        
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CollectableSignals.Instance.onMoneyCollection += OnMoneyCollection;
            CollectableSignals.Instance.onObstacleCollision += OnObstacleCollision;
            CollectableSignals.Instance.onDeposit += OnDeposit;
        }

        private void UnsubscribeEvents()
        {
            CollectableSignals.Instance.onMoneyCollection -= OnMoneyCollection;
            CollectableSignals.Instance.onObstacleCollision -= OnObstacleCollision;
            CollectableSignals.Instance.onDeposit -= OnDeposit;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private void Update() 
        {
            StackLerpMove();
        }
        private void OnMoneyCollection(GameObject other)
        {
            AddOnStack(other);
        }

        private void OnObstacleCollision(GameObject self)
        {
            RemoveFromStack(self);
        }

        private void OnDeposit(GameObject gameObject)
        {
            RemoveFromStack(gameObject);
        }

        
        #region LerpMove
        private void StackLerpMove()
                {
                    if (Collected.Count > 1)
                    {
                        for (int i = 1; i < Collected.Count; i++)
                        {
                            var FirstBall = Collected.ElementAt(i - 1);
                            var SectBall = Collected.ElementAt(i);
        
                            SectBall.transform.DOMoveX(FirstBall.transform.position.x, 20 * Time.deltaTime);
                            SectBall.transform.DOMoveZ(FirstBall.transform.position.z + 1.5f, 15 * Time.deltaTime);
                        }
                    }
                }
        private void CollectableScaleUp(GameObject other)
        {
            for (int i = other.transform.GetSiblingIndex()-1; i >= 0; i--)
            {
                int index = i;
                Vector3 scale = Vector3.one * 2;
                Collected[index].transform.DOScale(scale, 0.2f).OnComplete(() => {Collected[index].transform.DOScale(Vector3.one, 0.2f);});
                return;
            }
        }
        
        #endregion
        #region Stack Adding and Removing
        private void AddOnStack(GameObject other)
                {
                    other.tag = "Collected"; 
                    other.transform.parent = transform;
                    other.transform.localPosition = new Vector3(0, 0, 5f);
                    Collected.Add(other.gameObject);
                    StackLerpMove();
                    CollectableScaleUp(other);
                }
                private void RemoveFromStack(GameObject self) 
                {
                    if (self.CompareTag("Collected"))
                    {
                        var ChildCheck = Collected.Count; 
                        
                        if ( transform.childCount == ChildCheck)
                        {
                            Collected.Remove(self);
                            Destroy(self);
                       }
        
                       else
                       {
                           
                           int crashedObject = self.transform.GetSiblingIndex();
                           int lastIndex = self.transform.childCount - 1;
        
                           for (int i = crashedObject; i <= lastIndex; i++)
                           {
                               self.tag = "Collectable";
                               Collected[i].SetActive(false);
                           }
                       }
                    }
                } 
                #endregion
    }
}