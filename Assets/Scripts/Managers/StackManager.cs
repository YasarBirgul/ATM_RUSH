using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Extentions;
using Signals;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{ 
    public class StackManager : MonoSingleton<StackManager>
    {
        public List<GameObject> Collected = new List<GameObject>();
        private bool isCollected;
        
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
            isCollected = true;
        }
        
        private void OnObstacleCollision()
        {
            // for loop remove index tut
            
        }
        
        private void Update()
        { 
            StackMove();
        }
        private void StackMove()
        {
            if(!isCollected) return;
            else
            {
                 if (Collected.Count > 1)
                 {
                     for (int i = 1; i < Collected.Count; i++)
                     {
                         var FirstBall = Collected.ElementAt(i - 1);
                         var SectBall = Collected.ElementAt(i);
                     
                         SectBall.transform.position = new Vector3(Mathf.Lerp(SectBall.transform.position.x,FirstBall.transform.position.x,15 * Time.deltaTime)
                             ,SectBall.transform.position.y,Mathf.Lerp(SectBall.transform.position.z,FirstBall.transform.position.z  +1.5f,15 * Time.deltaTime));
                     }
                 }
            }
        }
    }
}