using UnityEngine;

namespace Controllers
{ 
    public class ConveryorBandAnimController : MonoBehaviour
    {   
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables
       
        [SerializeField] float scrollSpeed = 2f;
        
        #endregion
        
        #region Private Variables
        
        private Renderer _renderer;
        
        #endregion

        #endregion
        
        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }
        private void Update()
        {
            float offset = Time.time * scrollSpeed;
            _renderer.material.SetTextureOffset("_BaseMap",new Vector2(0,-offset));
        }
    }
}
