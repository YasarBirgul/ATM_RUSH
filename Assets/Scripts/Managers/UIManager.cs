using System;
using Controllers;
using Enums;
using Signals;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private UIPanelController uiPanelController;
        [SerializeField] private LevelPanelController levelPanelController;

        #endregion
        
        public CinemachineAnimationController _cinemachineAnimationController;
        private bool _mainCamera = true;
        
        public CameraManager CameraManager;
        
        #endregion

        #region Event Subscriptions

        

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            UISignals.Instance.onUpdateStageData += OnUpdateStageData;
            UISignals.Instance.onSetLevelText += OnSetLevelText;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onUpdateStageData -= OnUpdateStageData;
            UISignals.Instance.onSetLevelText -= OnSetLevelText;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
       
        

        private void OnOpenPanel(UIPanels panelParam)
        {
            uiPanelController.OpenPanel(panelParam);
        }

        private void OnClosePanel(UIPanels panelParam)
        {
            uiPanelController.ClosePanel(panelParam);
            
            
        }

        private void OnUpdateStageData(int value)
        {
            levelPanelController.UpdateStageData(value);
        }

        private void OnSetLevelText(int value)
        {
            levelPanelController.SetLevelText(value);
        }

        private void OnPlay()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
            // _cinemachineAnimationController._animator.SetBool("isDefault",true);
            
           
        }

        private void OnLevelFailed()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.FailPanel);
        }

        private void OnLevelSuccessful()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.WinPanel);
        }

        public void Play()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
            Time.timeScale = 1f;
            
        }

        public void NextLevel()
        {
            CoreGameSignals.Instance.onNextLevel?.Invoke();
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.WinPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
        }

        public void RestartLevel()
        {
            CoreGameSignals.Instance.onRestartLevel?.Invoke();
            //UISignals.Instance.onClosePanel?.Invoke(UIPanels.FailPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel); 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            

        }
        public void PauseLevel()
        {
            //UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
            CoreGameSignals.Instance.onReset?.Invoke();
            Time.timeScale = 0f;

        }
        
    }
}