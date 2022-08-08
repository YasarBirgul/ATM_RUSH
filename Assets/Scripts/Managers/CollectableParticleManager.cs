using System;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class CollectableParticleManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public CollectableData Data;

        public ParticleSystem ParticleSystem;

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
            CollectableSignals.Instance.onDeposit -= OnDeposit;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion


        private void Awake()
        {
            Data = GetParticleData();
            gameObject.AddComponent<ParticleSystem>();
            ParticleSystem = GetComponent<ParticleSystem>();
        }

        private void Start()
        {
            Material particleMaterial = new Material(Shader.Find("Universal Render Pipeline/Particles/Unlit"));
            
            transform.Rotate(0, 0, 0);
            gameObject.GetComponent<ParticleSystemRenderer>().material = particleMaterial;
            
            var mainModule = ParticleSystem.main;
            var textureModule = ParticleSystem.textureSheetAnimation;
            var emissionModule = ParticleSystem.emission;
            var shapeModule = ParticleSystem.shape;
            
            ParticleSystem.Stop();
            mainModule.duration = 0.4f;
            mainModule.startLifetime = 1f;
            mainModule.gravityModifier = 0.4f;
            mainModule.loop = false;
            mainModule.playOnAwake = false;
            mainModule.startSize = 0.5f;
            mainModule.maxParticles = 10;
            
            textureModule.enabled = true;
            textureModule.mode = ParticleSystemAnimationMode.Sprites;
            
            emissionModule.rateOverTime = 0;
            emissionModule.burstCount = 10;
            
            
        }

        public CollectableData GetParticleData() => Resources.Load<CD_Collectable>("Data/CD_Collectable").CollectableData;
        
    
         private void OnObstacleCollision(GameObject CollidedActiveObject,int stackedCollectablesIndex)
        {
             
            transform.position = CollidedActiveObject.GetComponent<Collider>().transform.position;
            var ColObjStateData = CollidedActiveObject.GetComponent<CollectableManager>().StateData;
            int ParticleOrder = (int)ColObjStateData;
            var particleSprite = Data.CollectableParticleSpriteList[ParticleOrder].CollectanbleParticals;

              if (ColObjStateData == CollectableType.Money)
              {
                  ParticleSystem.textureSheetAnimation.SetSprite(0,particleSprite);
                  
                  DoEmit(particleSprite);
              }
              if (ColObjStateData == CollectableType.Gold)
              {
                  ParticleSystem.textureSheetAnimation.SetSprite(0,particleSprite);
                  
                 DoEmit(particleSprite);
                  
              }
              if (ColObjStateData == CollectableType.Diamond)
              {
                  ParticleSystem.textureSheetAnimation.SetSprite(0,particleSprite);
                  
                  DoEmit(particleSprite);
              }
        }
         private void OnDeposit(GameObject CollidedActiveObject,int ID)
         {
             var particleSprite = Data.CollectableParticleSpriteList[0].CollectanbleParticals;
             
             if (CollidedActiveObject.CompareTag("Collected"))
             {
                 ParticleSystem.textureSheetAnimation.SetSprite(0,particleSprite);
                 
                 transform.position = CollidedActiveObject.GetComponent<Collider>().transform.position;
               
                 DoEmit(particleSprite);
             }
         }

         void DoEmit(Sprite particleSprite)
         {
             var TextureParams =  ParticleSystem.textureSheetAnimation;
             var emitParams = new ParticleSystem.EmitParams();
             TextureParams.SetSprite(0,particleSprite);
             ParticleSystem.Emit(emitParams, 10);
             ParticleSystem.Play();
         }
    }
}

