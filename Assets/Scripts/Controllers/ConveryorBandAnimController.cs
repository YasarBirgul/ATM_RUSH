using UnityEngine;

namespace Controllers
{ 
    public class ConveryorBandAnimController : MonoBehaviour
    {   
        #region SelfVariables

        #region Serialized Variables
        [SerializeField] float scrollSpeed = 2f;
        #endregion
        #region Private Variables
        private Renderer renderer;
        #endregion

        #endregion


        private void Awake()
        {
            renderer = GetComponent<Renderer>();
        }

        private void Update()
        {
            float offset = Time.time * scrollSpeed;
            renderer.material.SetTextureOffset("_BaseMap",new Vector2(0,-offset));
        }
    }
}
