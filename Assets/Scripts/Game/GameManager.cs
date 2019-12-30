using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using MountainSlide.Player;
using MountainSlide.Camera;
using MountainSlide.Level.Boost;

namespace MountainSlide.GameManager
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance { get { return instance ? instance : instance = FindObjectOfType<GameManager>(); } }

        [SerializeField]
        private UI.UIManager uIManager;

        [Header("Player")]
        [SerializeField]
        private PlayerMove playerPrefab;
        [SerializeField]
        private Transform spawnHolderPlayer;
        [SerializeField]
        private CameraFollow cameraFollow;
        [SerializeField]
        private DynamicJoystick joystick;
        private PlayerMove cachePlayer;

        [Header("Level")]
        [SerializeField]
        private Transform finish;
        public static bool IsEndLevel;

        private void Awake()
        {
            instance = this;
        }

        public void InitGame()
        {
            InitPlayer(() => {
                uIManager.StartCheckDistance();
            });
        }

        private void InitPlayer(Action onSucsess = null, Action onFailed = null)
        {
            try
            {
                var pl = Instantiate(playerPrefab, spawnHolderPlayer, true);
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

        public float GetCurentDistance()
        {
            return Vector3.Distance(cachePlayer.gameObject.transform.position, finish.position);
        }

        public void Respawn()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void BoostTake(TypeBoost typeBoost)
        {
            cachePlayer.ApplyBoost(typeBoost);
            uIManager.StartBoost(3, typeBoost);
        }

        public void FinishLevel()
        {
            IsEndLevel = true;
            uIManager.FinishGame();
        }
    }
}