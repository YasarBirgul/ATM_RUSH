using System;
using UnityEngine;

namespace Controllers
{
    public class CollectableMovementController: MonoBehaviour 
    {
        #region Public Variables

        public Transform ConnectedNode;

        #region Serialized Variables
        
        #endregion
        #endregion
        private void LerpMove() 
        {
            transform.position = new Vector3(
                Mathf.Lerp(transform.position.x, ConnectedNode.position.x, Time.deltaTime * 20),
                ConnectedNode.position.y,
                ConnectedNode.position.z + 1);
        }

        private void Update()
        {
            LerpMove();
        }
    }
}