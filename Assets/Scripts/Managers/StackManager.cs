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
        #region Self Variables

        #region Public Variables
        public List<GameObject> Collected = new List<GameObject>();
        #endregion

        #region Serialized Variables
        #endregion
        #endregion
        public float SCALE_MULTIPLIER = .5f;
        public float SCALE_DURATION = .5f;
        private Tween scaleTween;
        private bool isCollected;
        private bool isPickedUp;

        #region Event Subscription 
        
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
        #endregion
        
        private void OnMoneyCollection(GameObject other)
        {
            AddOnStack(other);
        }

        private void OnObstacleCollision(GameObject self)
        {
            int nextIndex = transform.GetSiblingIndex();
            int currentindex = nextIndex - 1;
            RemoveFromStack(self);
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
       // public void CollectableScaleUp()
       // {
       //     for (int i = Collected.Count - 1; i >= 0; i--)
       //     {
       //         int index = i;
       //         Vector3 scale = Vector3.one * 2;
       //         Collected[index].transform.DOScale(scale, 0.2f).OnComplete(() => {Collected[index].transform.DOScale(Vector3.one, 0.2f);});
       //         return;
       //     }
       // }
        
        private void AddOnStack(GameObject other)
        {   
            
            foreach(GameObject i in Collected)
            {
                i.transform.parent = transform;
            }
            other.tag = "Collected"; 
            other.transform.parent = transform;
           // other.transform.localPosition = Vector3.forward;
            Collected.Add(other.gameObject);
            StartCoroutine(CollectableScaleUp());

        }
        public void RemoveFromStack(GameObject self) 
        {
            if (self.CompareTag("Collected"))
            {
               if (Collected.Count-1 == Collected.IndexOf(self.gameObject))
               {
                   Collected.Remove(self);
                   Destroy(self);
               }

               else
               {
                   int crashedObject = Collected.IndexOf(self);
                   int lastIndex = Collected.Count - 1;

                   for (int i = crashedObject; i <= lastIndex; i++)
                   {
                       Collected.RemoveAt(i);
                       Destroy(self);
                   }
               }
            }
        }
        
        IEnumerator CollectableScaleUp()
        {
            for (int i = 0; i < Collected.Count; i++)
            { 
                ShakeScaleOfStack(Collected[i].transform);
                yield return new WaitForSeconds(0.5f);
            }
     
        }
        private void ShakeScaleOfStack(Transform transform)
        {
            if (scaleTween != null)
                scaleTween.Kill(true);
             
            scaleTween = transform.DOPunchScale(Vector3.one * SCALE_MULTIPLIER, SCALE_DURATION, 2);
        }
        private void throwOthers(GameObject trow)
        {
            trow.transform.Translate(0,10,0);
        }
    }
}