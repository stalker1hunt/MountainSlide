using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using MountainSlide.Player;
using MountainSlide.Camera;
using MountainSlide.Level.Boost;
using MountainSlide.Save;
using RVP;

namespace MountainSlide.GameManager
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance { get { return instance ? instance : instance = FindObjectOfType<GameManager>(); } }

        [SerializeField]
        private UI.UIManager uIManager;
        [SerializeField]
        private SpawnerBots spawner;

        [Header("Player")]
        //[SerializeField]
        //private PlayerMove playerPrefab;
        [SerializeField]
        private Transform spawnHolderPlayer;
        //[SerializeField]
        //private CameraFollow cameraFollow;
        //[SerializeField]
        //private DynamicJoystick joystick;
        [SerializeField]
        private GameObject cachePlayer;

        [Header("Level")]
        [SerializeField]
        private Transform finish;
        public static bool IsEndLevel;

        private int levelCompleted;
        public int LevelCompleted { get { return levelCompleted; } }

        private void Awake()
        {
            instance = this;
        }

        public void InitGame(Action onSuncsess = null)
        {
            //InitPlayer(() => {
            //});

            levelCompleted = Prefs.LevelCompleted;
            spawner.FindWaysForBots();
            uIManager.StartCheckDistance();

            onSuncsess?.Invoke();
        }

        /*  private void InitPlayer(Action onSucsess = null, Action onFailed = null)
        {
            try
            {
                var pl = Instantiate(playerPrefab, spawnHolderPlayer.position, Quaternion.identity);
                var rb = pl.GetComponent<Rigidbody>();
                rb.Sleep();
                pl.InitPlayer = false;
                pl.Joystick = joystick;
                cameraFollow.target = pl.gameObject.transform;
                if (rb.IsSleeping())
                    rb.WakeUp();

                pl.InitPlayer = true;
                cachePlayer = pl;

                onSucsess.Invoke();
            }
            catch (Exception)
            {
                onFailed.Invoke();
                throw;
            }
        }
        */

        public float GetCurentDistance()
        {
            return Vector3.Distance(cachePlayer.transform.position, finish.position);
        }

        public void Respawn()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void BoostTake(TypeBoost typeBoost)
        {
            StartCoroutine(StartBoost());
            uIManager.StartBoost(3, typeBoost);
        }

        IEnumerator StartBoost()
        {
            cachePlayer.GetComponent<MobileInput>().SetBoost(true);
            yield return new WaitForSeconds(3);
            cachePlayer.GetComponent<MobileInput>().SetBoost(false);
        }

        public void FinishLevel()
        {
            Prefs.LevelCompleted = ++levelCompleted;
            IsEndLevel = true;
            uIManager.FinishGame();
        }
    }
}