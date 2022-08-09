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
            
            
            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.white, 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) }
            );
            
            transform.Rotate(0, 0, 0);
            gameObject.GetComponent<ParticleSystemRenderer>().material = particleMaterial;
            
            var mainModule = ParticleSystem.main;
            var textureModule = ParticleSystem.textureSheetAnimation;
            var emissionModule = ParticleSystem.emission;
            var colorOLTModule = ParticleSystem.colorOverLifetime;
            
            ParticleSystem.Stop();
            mainModule.duration = 0.4f;
            mainModule.startLifetime = 1f;
            mainModule.gravityModifier = 0.7f;
            mainModule.loop = false;
            mainModule.playOnAwake = false;
            mainModule.startSize = 2f;
            mainModule.maxParticles = 10;
            
            textureModule.enabled = true;
            textureModule.mode = ParticleSystemAnimationMode.Sprites;
           
            emissionModule.rateOverTime = 0;
            emissionModule.SetBurst(1,new ParticleSystem.Burst(0,10,15,1,0.01f));

            colorOLTModule.enabled = true;
            colorOLTModule.color = new ParticleSystem.MinMaxGradient(gradient);
            
        }

        public CollectableData GetParticleData() => Resources.Load<CD_Collectable>("Data/CD_Collectable").CollectableData;
        
    
         private void OnObstacleCollision(GameObject CollidedActiveObject,int stackedCollectablesIndex)
        {
             
            Vector3 position = CollidedActiveObject.GetComponent<Collider>().transform.position;
            var ColObjStateData = CollidedActiveObject.GetComponent<CollectableManager>().StateData;
            int ParticleOrder = (int)ColObjStateData;
            var particleSprite = Data.CollectableParticleSpriteList[ParticleOrder].CollectanbleParticals;

              if (ColObjStateData == CollectableType.Money)
              {
                  ParticleSystem.textureSheetAnimation.SetSprite(0,particleSprite);
                  
                  DoEmit(particleSprite,position);
              }
              if (ColObjStateData == CollectableType.Gold)
              {
                  ParticleSystem.textureSheetAnimation.SetSprite(0,particleSprite);
                  
                 DoEmit(particleSprite,position);
                  
              }
              if (ColObjStateData == CollectableType.Diamond)
              {
                  ParticleSystem.textureSheetAnimation.SetSprite(0,particleSprite);
                  
                  DoEmit(particleSprite,position);
              }
        }
         private void OnDeposit(GameObject CollidedActiveObject,int ID)
         {
             var particleSprite = Data.CollectableParticleSpriteList[0].CollectanbleParticals;
             
             if (CollidedActiveObject.CompareTag("Collected"))
             {
                 ParticleSystem.textureSheetAnimation.SetSprite(0,particleSprite);
                 
               Vector3 newPosAtm = CollidedActiveObject.GetComponent<Collider>().transform.position;
               Vector3 newPos = newPosAtm + new Vector3(0, 0, -4);
               DoEmit(particleSprite , newPos);
             }
         }
         void DoEmit(Sprite particleSprite , Vector3 newPos)
         {
             var TextureParams =  ParticleSystem.textureSheetAnimation;
             var emitParams = new ParticleSystem.EmitParams();
             TextureParams.SetSprite(0,particleSprite);
             emitParams.position = newPos + Vector3.up*2;
             emitParams.applyShapeToPosition = true;
             ParticleSystem.Emit(emitParams, 1);
             ParticleSystem.Play();
         }
    }
}

