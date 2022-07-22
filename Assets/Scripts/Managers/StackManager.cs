using System.Collections.Generic;
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
        public GameObject TempHolder;
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

        private void OnObstacleCollision(GameObject self,int index)
        {
            RemoveFromStack(self,index);
     
        }
        
        private void OnDeposit(GameObject gameObject,int index)
        {
            RemoveFromStack(gameObject,index);
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
                private void RemoveFromStack(GameObject self,int index) 
                {
                    
                    if (self.CompareTag("Player"))
                    {
                        var ChildCheck = Collected.Count-1;
                        for (int i = 0; i <= ChildCheck; i++)
                        {
                            Collected[i].transform.DOJump(Collected[i].transform.position + new Vector3(Random.Range(-3, 3), 0, (Random.Range(14, 15))), 4.0f, 2, 1f);
                            Collected[i].transform.tag = "Collectable";
                            Collected[i].transform.SetParent(TempHolder.transform);
                            Collected.Remove(Collected[i]); 
                            Collected.TrimExcess();
                        }
                       
                    }
                    if (self.CompareTag("Collected"))
                    {
                        var ChildCheck = Collected.Count;
                        
                        if (index == ChildCheck-1)
                        {
                             Collected.Remove(self);
                             Destroy(self); 
                        }
                        else
                        {
                           for (int i = index; i < ChildCheck; i++)
                           {
                               Collected[i].transform.SetParent(TempHolder.transform);
                               Collected[i].transform.DOJump(Collected[i].transform.position + new Vector3(Random.Range(-3, 3), 0, (Random.Range(9, 15))), 4.0f, 2, 1f);
                               Collected[i].transform.tag = "Collectable";
                               Collected.Remove(Collected[i]);
                           }
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