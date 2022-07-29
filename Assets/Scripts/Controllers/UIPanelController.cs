using System.Collections.Generic;
using Enums;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class UIPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<GameObject> panels;
        [SerializeField] private GameObject noAdsButton;
        [SerializeField] private GameObject shopButton;
        [SerializeField] private GameObject settingsButton;
        [SerializeField] private GameObject layoutGroup;
        #endregion
        #region Private Variables

        #endregion
        #endregion
       

        public void OpenPanel(UIPanels panelParam)
        {
            panels[(int) panelParam].SetActive(true);
            noAdsButton.SetActive(true);
            shopButton.SetActive(true);
            settingsButton.SetActive(true);
            layoutGroup.SetActive(true);
        }

        public void ClosePanel(UIPanels panelParam)
        {
            panels[(int) panelParam].SetActive(false);
            noAdsButton.SetActive(false);
            shopButton.SetActive(false);
            settingsButton.SetActive(false);
            layoutGroup.SetActive(false);
        }
    }
}