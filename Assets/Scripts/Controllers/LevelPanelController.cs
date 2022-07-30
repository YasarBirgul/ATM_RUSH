using System.Collections.Generic;
using DG.Tweening;
using Signals;
using StylizedWater2;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class LevelPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private List<Image> stageImages;

        #endregion

        #endregion
        
        public void SetLevelText(int value)
        {
            levelText.text = value.ToString();
        }

        public void UpdateStageData(int value)
        {
            stageImages[value].DOColor(Color.green, .5f).SetEase(Ease.Linear);
        }
    }
}