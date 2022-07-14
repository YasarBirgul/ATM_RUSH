using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Extentions;
using UnityEngine;
namespace Managers
{ 
    public class StackManager : MonoSingleton<StackManager>
    {
        public List<GameObject> Colleted = new List<GameObject>();
        private void Update()
        { 
            StackMove();
        }
        void StackMove()
        {
            if (Colleted.Count > 1)
            {
                for (int i = 1; i < Colleted.Count; i++)
                {
                    var FirstBall = Colleted.ElementAt(i - 1);
                    var SectBall = Colleted.ElementAt(i);
             
                    //  var DesireDistance = Vector3.Distance(FirstBall.position,SectBall.position );
             
                    //    if (DesireDistance <= Distance)
                    //    {
                    SectBall.transform.position = new Vector3(Mathf.Lerp(SectBall.transform.position.x,FirstBall.transform.position.x,15 * Time.deltaTime)
                        ,SectBall.transform.position.y,Mathf.Lerp(SectBall.transform.position.z,FirstBall.transform.position.z  +1.5f,15 * Time.deltaTime));
                    //}
                }
            }
        }
    }
}