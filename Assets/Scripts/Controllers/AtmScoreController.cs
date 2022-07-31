using Managers;
using TMPro;
using UnityEngine;

namespace Controllers
{ 
    public class AtmScoreController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables
        
        [SerializeField] private TextMeshPro scoreText;
        
        #endregion
        
        #region Private Variables
        
        private float _score;
        
        #endregion

        #endregion
        public void OnDeposit(GameObject self)
        {
            if (self.CompareTag("Collected"))
            {
                int state = (int) self.GetComponent<CollectableManager>().StateData; 
                _score += state;
                scoreText.text = _score.ToString();
            }
        }
    }
}