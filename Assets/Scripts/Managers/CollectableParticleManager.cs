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

        public ParticleSystem system;

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

            Data = GetParticleData();
            
           Material particleMaterial = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
            var go = gameObject;
            go.transform.Rotate(-90, 45, 0); // Rotate so the system emits upwards.
            system = go.GetComponent<ParticleSystem>();
            // go.GetComponent<ParticleSystemRenderer>().material = particleMaterial;
            var mainModule = system.main;
            mainModule.gravityModifier = 0;
            mainModule.loop = false;
            mainModule.startSize = 0.5f;
            mainModule.maxParticles = 10;

            // Every 2 secs we will emit.
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
                  system.textureSheetAnimation.SetSprite(0,particleSprite);
                  
                  DoEmit();
              }
              if (ColObjStateData == CollectableType.Gold)
              {
                  system.textureSheetAnimation.SetSprite(0,particleSprite);
                  
                 DoEmit();
                  
              }
              if (ColObjStateData == CollectableType.Diamond)
              {
                  system.textureSheetAnimation.SetSprite(0,particleSprite);
                  
                  DoEmit();
              }
        }
         private void OnDeposit(GameObject CollidedActiveObject,int ID)
         {
             var particleSprite = Data.CollectableParticleSpriteList[0].CollectanbleParticals;
             
             if (CollidedActiveObject.CompareTag("Collected"))
             {
                 system.textureSheetAnimation.SetSprite(0,particleSprite);
                 
                 transform.position = CollidedActiveObject.GetComponent<Collider>().transform.position;
               
                 DoEmit();
             }
         }

         void DoEmit()
         {
             var emitParams = new ParticleSystem.EmitParams();
             emitParams.startSize = 1f;
             system.Emit(emitParams, 10);
             system.Play();
         }
    }
}

