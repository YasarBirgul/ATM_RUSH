using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{ 
    public class CollectableParticleManager : MonoBehaviour
    {
        #region Self Variables
    
        #region Public Variables
        
        public CollectableData Data;
        
        public ParticleSystem Particle;
        
        #endregion
    
        #region Serialized Variables

        #endregion
    
        #region Private Variables

        #endregion
    
        #endregion

        #region Event Subscription 
        
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CollectableSignals.Instance.onObstacleCollision += OnObstacleCollision;
            CollectableSignals.Instance.onDeposit += OnDeposit;
        }

        private void UnsubscribeEvents()
        {
            CollectableSignals.Instance.onObstacleCollision -= OnObstacleCollision;
            CollectableSignals.Instance.onDeposit += OnDeposit;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        private void Start()
        {
            Particle = GetComponent<ParticleSystem>();
            Data = GetParticleData();
        }

        private CollectableData GetParticleData() =>
            Resources.Load<CD_Collectable>("Data/CD_Collectable").CollectableData;

         private void OnObstacleCollision(GameObject CollidedActiveObject,int stackedCollectablesIndex)
        {
             
            transform.position = CollidedActiveObject.GetComponent<Collider>().transform.position;
            var ColObjStateData = CollidedActiveObject.GetComponent<CollectableManager>().StateData; 
            int ParticleOrder = (int)ColObjStateData;
              var particleSprite = Data.CollectableParticleSpriteList[ParticleOrder].CollectanbleParticals;
              
              if (ColObjStateData == CollectableType.Money)
              {
                   Particle.textureSheetAnimation.SetSprite(0, particleSprite);
                   Particle.Play();
              }
              if (ColObjStateData == CollectableType.Gold)
              {
                   Particle.textureSheetAnimation.SetSprite(0, particleSprite);
                   Particle.Play();
              }
              if (ColObjStateData == CollectableType.Diamond)
              {
                   Particle.textureSheetAnimation.SetSprite(0, particleSprite);
                   Particle.Play();
              }  
            
        }
         private void OnDeposit(GameObject CollidedActiveObject,int ID)
         {
             transform.position = CollidedActiveObject.GetComponent<Collider>().transform.position;
             var particleSprite = Data.CollectableParticleSpriteList[0].CollectanbleParticals;
             
             if (CollidedActiveObject.CompareTag("Collected"))
             {
                 Particle.textureSheetAnimation.SetSprite(0, particleSprite);
                 Particle.Play();
             }
         }
    }
}

