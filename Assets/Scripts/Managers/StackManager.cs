using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
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
            StartCoroutine(CollectableScaleUp());
        }

        private void OnObstacleCollision(GameObject CollidedActiveObject,GameObject Collided,int stackedCollectablesIndex)
        {
            RemoveFromStack(CollidedActiveObject,Collided,stackedCollectablesIndex);
     
        }
        
        private void OnDeposit(GameObject CollidedActiveObject,GameObject Collided,int stackedCollectablesIndex)
        {
            RemoveFromStack(CollidedActiveObject,Collided,stackedCollectablesIndex);
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
                            SectBall.transform.DOMoveZ(FirstBall.transform.position.z + 1.5f, 1*Time.deltaTime);
                        }
                    }
                }
        void OnFinalAtmCollision(GameObject Collected)
        { 
            if (Collected.CompareTag("Collected"))
            { 
                this.Collected.Remove(Collected);
                Collected.transform.DOMoveX(Collected.transform.position.x - 10, 1);
                Collected.transform.DOMoveZ(Collected.transform.position.z , 1);   
            }
        } 
        public IEnumerator CollectableScaleUp()
        {
            for (int i = Collected.Count -1; i >= 0; i--)
            {
                int index = i;
                Vector3 scale = Vector3.one * 1.5f;
                Collected[index].transform.DOScale(scale, 0.2f).SetEase(Ease.Flash);
                Collected[index].transform.DOScale(Vector3.one, 0.2f).SetDelay(0.2f).SetEase(Ease.Flash);
                yield return new WaitForSeconds(0.05f);
            }
            Collected.TrimExcess();
        }
        #endregion
        #region Stack Adding and Removing
        private void AddOnStack(GameObject other)
        { 
            other.transform.parent = transform;
            Collected.Add(other.gameObject);
        }
        private void RemoveFromStack(GameObject CollidedActiveObject,GameObject Collided,int stackedCollectablesIndex) 
        {
            if (CollidedActiveObject.CompareTag("Player"))
            {
                for (int i = Collected.Count-1; i >= 0; i--)
                { 
                    if (i < 0 )
                    {
                      return; 
                    }
                    int DecreaseScoreValue = (int)Collected[i].GetComponent<CollectableManager>().StateData;
                    if (Collided.CompareTag("Obstacle"))
                    {
                        ScoreSignals.Instance.onScoreDown(DecreaseScoreValue);
                    }
                    Collected[i].transform.DOJump(Collected[i].transform.position + new Vector3(Random.Range(-3, 3), 0, (Random.Range(9, 15))), 4.0f, 2, 1f);
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
                    int DecreaseScoreValue = (int)Collected[ChildCheck].GetComponent<CollectableManager>().StateData;
                    
                    if (Collided.CompareTag("Obstacle"))
                    {
                        ScoreSignals.Instance.onScoreDown(DecreaseScoreValue);
                    }
                    Collected.Remove(CollidedActiveObject);
                    Destroy(CollidedActiveObject); 
                    Collected.TrimExcess();
                }
                else
                { 
                    for (int i = ChildCheck; i > stackedCollectablesIndex; i--)
                    {
                        int DecreaseScoreValue = (int)Collected[i].GetComponent<CollectableManager>().StateData;
                        if (Collided.CompareTag("Obstacle"))
                        {
                            ScoreSignals.Instance.onScoreDown(DecreaseScoreValue);
                        }
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