using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
        [SerializeField]
        private TextMeshProUGUI countLevels;

        private GameManager.GameManager gm => GameManager.GameManager.Instance;

        private void Start()
        {
            Application.targetFrameRate = 60;

            Time.timeScale = 0;

            countLevels.gameObject.SetActive(false);
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

            countLevels.gameObject.SetActive(true);

            gm.InitGame(() =>
            {
                countLevels.text = $"{gm.LevelCompleted} count lvl completed" ;
                panelStart.SetActive(false);
            });
        }

        public void FinishGame()
        {
            panelFinish.SetActive(true);
        }

        public void Restart()
        {
            gm.Respawn();
        }

        public void NextLevel()
        {
            var _levelData = Resources.Load<LevelDataManager>("Data/LevelManager");
            var _level = _levelData.GetNextLevel(SceneManager.GetActiveScene().name);
            if (_level != null)
            {
                SceneManager.LoadSceneAsync(_level.LevelName);
            }
            else
            {
                var _levelStart = _levelData.GetLevel(1);
                if (_levelStart != null)
                    SceneManager.LoadSceneAsync(_levelStart.LevelName);
                else
                    Restart();
            }
        }

        public void StartCheckDistance()
        {
            StartCoroutine(CheckDistance());
        }

        private bool boostActive;
        private int secondActive;

        //Разделить логику бустов и УИ на отдельные модули не зависиющих один от одного
        public void StartBoost(int sec, TypeBoost typeBoost)
        {
            if (!boostActive)
            {
                boostActive = true;
                boostUiSpeed.gameObject.SetActive(true);
                boostUiSpeed.SetupBoostUi(typeBoost);
                StartCoroutine(StartBoostEffect(sec, () =>
                {
                    boostUiSpeed.gameObject.SetActive(false);
                    boostActive = false;
                }));
            }
            else
            {
                secondActive += 3;
            }
        }

        #region Coruntine
        IEnumerator StartBoostEffect(int sec, Action onDone = null)
        {
            secondActive += sec;
            do
            {
                boostUiSpeed.BoostTime = secondActive;
                yield return new WaitForSeconds(1);
                secondActive--;

            } while (secondActive > 0);

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

