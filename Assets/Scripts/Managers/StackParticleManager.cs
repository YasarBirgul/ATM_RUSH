using System;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{ 
    public class StackParticleManager : MonoBehaviour
    {

        public CollectableData Data;
        public ParticleSystem Particle;
        
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

        private void Awake()
        {
            Particle = GetComponent<ParticleSystem>();
            Data = GetParticleData();
        }

        private CollectableData GetParticleData() =>
            Resources.Load<CD_Collectable>("Data/CD_Collectable").CollectableData;

         private void OnObstacleCollision(GameObject CollidedActiveObject,GameObject Collided,int stackedCollectablesIndex)
        {
            if (CollidedActiveObject.CompareTag("Collected")) 
            {
              transform.position = Collided.transform.position;
              
              var ColObjStateData = CollidedActiveObject.GetComponent<CollectableManager>().StateData;
              int ParticleOrder = (int)ColObjStateData-1;
              var particleSprite = Data.CollectableParticleSpriteList[ParticleOrder].CollectanbleParticals;
              
              if (ColObjStateData == CollectableType.Money)
              {
                   Particle.textureSheetAnimation.SetSprite(0, particleSprite);
                   PlayTheParticle(Particle);
              }
              if (ColObjStateData == CollectableType.Gold)
              {
                   Particle.textureSheetAnimation.SetSprite(0, particleSprite);
                   PlayTheParticle(Particle);
              }
              if (ColObjStateData == CollectableType.Diamond)
              {
                   Particle.textureSheetAnimation.SetSprite(0, particleSprite);
                   PlayTheParticle(Particle);
              }  
            } 
            void PlayTheParticle(ParticleSystem particleSystem)
          {
              particleSystem.Play();
          }
        }

    }
}

