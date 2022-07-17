using TMPro;
using UnityEngine;

namespace Controllers
{
    public class AtmScoreController : MonoBehaviour
    {
        [SerializeField] private TextMeshPro scoreText;
        private float score;

        
        private void Update()
        { 
            scoreText.text = score.ToString();
        }
        
        public void OnDeposit(GameObject self)
        {
            if (self.CompareTag("Collected"))
            {
                score += 1;
            }
        }
    }
}