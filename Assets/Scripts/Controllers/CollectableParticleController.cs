using Signals;
using UnityEngine;

namespace Controllers
{ 
    public class CollectableParticleController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem moneypParticle;
        private int Index;

        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CollectableSignals.Instance.onObstacleCollision += OnObstacleCollision;
        }

        private void UnsubscribeEvents()
        {
            CollectableSignals.Instance.onObstacleCollision -= OnObstacleCollision;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        void OnObstacleCollision(GameObject CollectableGO,int IndexMoney)
        { 
            IndexMoney = transform.parent.GetSiblingIndex();
           ParticalPlay(CollectableGO,IndexMoney);
        }
        private void ParticalPlay(GameObject CollectableGO,int IndexMoney)
        {
            Debug.LogWarning("IndexMOney : "+IndexMoney);
            Debug.LogWarning("Index : "+Index);
            
            if (CollectableGO.CompareTag("Collected"))
            {
                if (IndexMoney == Index)
                {
                    moneypParticle.Play();
                }
            }
        }
    }
}