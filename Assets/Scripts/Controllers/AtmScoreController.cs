using Managers;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class AtmScoreController : MonoBehaviour
    {
        [SerializeField] private TextMeshPro scoreText;
        private float score;
        #region SelfVariables
        #endregion
        public void OnDeposit(GameObject self)
        {
            if (self.CompareTag("Collected"))
            {
                int state = (int) self.GetComponent<CollectableManager>().StateData; 
                score += state;
                scoreText.text = score.ToString();
            }
        }
    }
}