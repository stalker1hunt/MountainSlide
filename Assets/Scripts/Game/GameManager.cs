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
        public bool AiEnable;
        [SerializeField] private List<Material> skyboxs = new List<Material>();

        private int levelCompleted;
        public int LevelCompleted { get { return levelCompleted; } }

        private void Awake()
        {
            instance = this;
            RenderSettings.skybox = GetRandomSkyBox();
        }

        public void InitGame(Action onSuncsess = null)
        {
            //InitPlayer(() => {
            //});


            levelCompleted = Prefs.LevelCompleted;

            if (AiEnable)
                spawner.FindWaysForBots();

            uIManager.StartCheckDistance();

            cacheEulerAnglesY = cachePlayer.transform.localRotation.eulerAngles.y;

            onSuncsess?.Invoke();
        }

        float cacheEulerAnglesY;
        private void FixedUpdate()
        {
         
        }

        private Material GetRandomSkyBox()
        {
            return skyboxs[(UnityEngine.Random.Range(0, skyboxs.Count - 1))];
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

        private bool boostActive;
        private int secondActive;
        public void BoostTake(TypeBoost typeBoost)
        {
            if (!boostActive)
            {
                secondActive += 3;
                boostActive = true;
                StartCoroutine(StartBoost());
            }
            else
            {
                secondActive += 3;
            }

            uIManager.StartBoost(3, typeBoost);
        }

        IEnumerator StartBoost(Action onDone = null)
        {
            var _mobInput = cachePlayer.GetComponent<MobileInput>();
            while (secondActive > 0)
            {
                _mobInput.SetBoost(boostActive);
                yield return new WaitForSeconds(1);
                secondActive--;
            }
            boostActive = false;
            _mobInput.SetBoost(boostActive);
        }

        public void FinishLevel()
        {
            Prefs.LevelCompleted = ++levelCompleted;
            IsEndLevel = true;
            uIManager.FinishGame();
        }
    }
}