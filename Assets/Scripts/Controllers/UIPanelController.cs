using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Controllers
{
    public class UIPanelController : MonoBehaviour
    {
        
        #region Self Variables
        
        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private List<GameObject> panels;
        
        [SerializeField] private GameObject layoutGroup;
        
        #endregion
        
        #region Private Variables

        #endregion
        #endregion
        
       

        public void OpenPanel(UIPanels panelParam)
        {
            panels[(int) panelParam].SetActive(true);
            
        }

        public void ClosePanel(UIPanels panelParam)
        {
            panels[(int) panelParam].SetActive(false);
            
        }

        public void SettingButtonClose()
        {
            layoutGroup.SetActive(false);
        }
        public void SettingButtonOpen()
        {
            layoutGroup.SetActive(true);
        }
    }
}