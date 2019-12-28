using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using MountainSlide.Player;
using MountainSlide.Camera;

namespace MountainSlide.GameManager
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance { get { return instance ? instance : instance = FindObjectOfType<GameManager>(); } }

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

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            InitManager();
        }

        public void InitManager()
        {
            InitPlayer(() => { });
        }

        private void InitPlayer(Action onSucsess = null, Action onFailed = null)
        {
            try
            {
                var pl = Instantiate(playerPrefab, spawnHolderPlayer, true);
                pl.GetComponent<Rigidbody>().isKinematic = true;
                pl.InitPlayer = false;
                pl.Joystick = joystick;
                cameraFollow.target = pl.gameObject.transform;
                pl.GetComponent<Rigidbody>().isKinematic = false;
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

        public void Respawn()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void BoostTake(TypeBoost typeBoost)
        {
            Debug.Log(typeBoost.ToString());
            //поднять скорость, отобразить на уи
        }

        public void FinishLevel()
        {

        }
    }
}