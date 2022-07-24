using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Extentions;
using Signals;
using UnityEngine;

namespace Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Variables
        #region Public Variables
        public List<GameObject> Collected = new List<GameObject>();
        public GameObject TempHolder;
        private TweenCallback tweenCallback;
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
            CollectableSignals.Instance.onFinalAtmCollision += OnFinalAtmCollision;
        }

        private void UnsubscribeEvents()
        {
            CollectableSignals.Instance.onMoneyCollection -= OnMoneyCollection;
            CollectableSignals.Instance.onObstacleCollision -= OnObstacleCollision;
            CollectableSignals.Instance.onDeposit -= OnDeposit;
            CollectableSignals.Instance.onFinalAtmCollision -= OnFinalAtmCollision;
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

        private void OnObstacleCollision(GameObject CollidedActiveObject,int stackedCollectablesIndex)
        {
            RemoveFromStack(CollidedActiveObject,stackedCollectablesIndex);
     
        }
        
        private void OnDeposit(GameObject CollidedActiveObject,int stackedCollectablesIndex)
        {
            RemoveFromStack(CollidedActiveObject,stackedCollectablesIndex);
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

        void OnFinalAtmCollision(GameObject Collectable)
        { 
            Collected.Remove(Collectable);
            Collectable.transform.DOMoveX(Collectable.transform.position.x - 10, 1);
            Collectable.transform.DOMoveZ(Collectable.transform.position.z , 1);
        }
        
        
        // private void CollectableScaleUp(GameObject other)
        // {
        //      for (int i = Collected.Count - 1; i >= 1; i-- )
        //     {
        //         // for (int i = Collected.Count - 1; i >= 1; i-- )
        //         // for (int i = other.transform.GetSiblingIndex()-1; i >= 0; i--)
        //         int index = i;
        //         Vector3 scale = Vector3.one * 2;
        //         Collected[index-1].transform.DOScale(scale, 0.5f).OnComplete(() => {Collected[index-1].transform.DOScale(Vector3.one, 0.5f);}).SetEase(Ease.OutSine);
        //         return;
        //     }
        // }
        public IEnumerator CollectableScaleUp()
        {
            for (int i = Collected.Count -1; i >= 1; i--)
            {
                int index = i;
                Vector3 scale = Vector3.one * 1.5f;
                Collected[index-1].transform.DOScale(scale, 0.1f).OnComplete(() => {Collected[index-1].transform.DOScale(Vector3.one, 0.1f);}).SetEase(Ease.OutSine);
                yield return new WaitForSeconds(0.03f);
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
            StartCoroutine(CollectableScaleUp());
        }
                private void RemoveFromStack(GameObject CollidedActiveObject,int stackedCollectablesIndex) 
                {
                    Debug.Log(CollidedActiveObject);
                    
                    if (CollidedActiveObject.CompareTag("Player"))
                    {
                        for (int i = Collected.Count-1; i >= 0; i--)
                        {
                            if (i < 0 )
                            {
                                return;
                            }
                            Collected[i].transform
                                .DOJump(Collected[i].transform.position + new Vector3(Random.Range(-3, 3), 0, (Random.Range(9, 15))), 4.0f, 2, 1f);
                            Collected[i].transform.tag = "Collectable";
                            Collected[i].transform.SetParent(TempHolder.transform);
                            Collected.Remove(Collected[i]);
                        }
                        Collected.TrimExcess();
                    }
                    if (CollidedActiveObject.CompareTag("Collected"))
                    {
                        var ChildCheck = Collected.Count-1;
                        
                        if (stackedCollectablesIndex == ChildCheck)
                        {
                             Collected.Remove(CollidedActiveObject);
                             Destroy(CollidedActiveObject); 
                             Collected.TrimExcess();
                        }
                        else
                        {
                           for (int i = ChildCheck; i > stackedCollectablesIndex; i--)
                           {
                               if (i > ChildCheck)
                               {
                                   return;
                               }
                               Collected[i].transform.SetParent(TempHolder.transform);
                               Collected[i].transform.DOJump(Collected[i].transform.position + new Vector3(Random.Range(-3, 3), 0, (Random.Range(9, 15))), 4.0f, 2, 1f);
                               Collected[i].transform.tag = "Collectable";
                               Collected.Remove(Collected[i]);
                           }
                           Collected.TrimExcess();
                           if (ChildCheck == 0)
                           {
                               return;
                           }
                        }
                        Collected.TrimExcess();
                    }
                } 
                #endregion
        
    }
}