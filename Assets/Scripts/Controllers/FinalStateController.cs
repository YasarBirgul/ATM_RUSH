using System;
using UnityEngine;
using DG.Tweening;

namespace Controllers
{
    public class FinalStateController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("RankCube"))
            {
                other.transform.DOMoveZ(other.transform.position.z - 5, 0.5f);
            }
        }
    }
}