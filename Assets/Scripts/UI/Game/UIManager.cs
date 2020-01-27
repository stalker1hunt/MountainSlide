using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

namespace MountainSlide.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] 
        private GameObject panelStart;
        [SerializeField]
        private GameObject panelFinish;
        [SerializeField]
        private Image progressBar;
        [SerializeField]
        private BoostUi boostUiSpeed;
        [SerializeField]
        private GameObject panelPause;
        private GameManager.GameManager gm => GameManager.GameManager.Instance;

        private void Start()
        {
            Application.targetFrameRate = 60;

            Time.timeScale = 0;

            panelStart.SetActive(true);
        }

        public void Pause(bool init)
        {
            Time.timeScale = init == true ? 0 : 1;
            panelPause.SetActive(init);
        }

        public void LoadMain()
        {
            SceneManager.LoadSceneAsync("Main");
        }

        public void StartGame()
        {
            Time.timeScale = 1;

            Debug.Log("StartGame");
            gm.InitGame();
            panelStart.SetActive(false);
        }

        public void FinishGame()
        {
            panelFinish.SetActive(true);
        }

        public void Restart()
        {
            gm.Respawn();
        }

        public void StartCheckDistance() 
        {
            StartCoroutine(CheckDistance());
        }

        float tSec;

        //Разделить логику бустов и УИ на отдельные модули не зависиющих один от одного
        public void StartBoost(float sec, TypeBoost typeBoost)
        {
            boostUiSpeed.gameObject.SetActive(true);
            boostUiSpeed.SetupBoostUi(typeBoost);

           StartCoroutine(StartBoostEffect(sec,()=> {
                boostUiSpeed.gameObject.SetActive(false);
            }));
        }

        #region Coruntine

        IEnumerator StartBoostEffect(float sec, Action onDone = null)
        {
            tSec += sec;
            do
            {
                boostUiSpeed.BoostTime = tSec;
                yield return new WaitForSeconds(1);
                tSec--;

            } while (tSec > 0);

            onDone.Invoke();
        }

        IEnumerator CheckDistance()
        {
            do
            {
                progressBar.fillAmount = 1 - gm.GetCurentDistance() * 0.01f;
                yield return new WaitForSeconds(0.1f);
            } while (!GameManager.GameManager.IsEndLevel);
        }

        #endregion
    }
}

