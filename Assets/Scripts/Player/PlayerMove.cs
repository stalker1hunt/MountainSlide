using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MountainSlide.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMove : MonoBehaviour
    {
        private Rigidbody playerRigidbody;
        [HideInInspector]
        public DynamicJoystick Joystick;
        public bool InitPlayer;

        [SerializeField]
        [Range(-100, 0)]
        private float maxSpeed;
        public float MaxSpeed { get { return maxSpeed; } }

        private float curentSpeed;
        public float CurentSpeed { get { return curentSpeed; } }

        bool boostActive;

        private void Awake()
        {
            playerRigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (!InitPlayer) return;

            if (playerRigidbody != null)
            {
                ApplyInput();
                curentSpeed = playerRigidbody.velocity.y;

                if (MaxSpeed != default && CurentSpeed < MaxSpeed)
                    curentSpeed = MaxSpeed;
            }
            else
            {
                Debug.LogWarning("Rigid body not attached to player " + gameObject.name);
            }
        }

        private void ApplyInput()
        {
            if (Joystick.Horizontal >= 0.2f)
                playerRigidbody.AddForce(new Vector3(1, 0, 0), ForceMode.Impulse);
            else if (Joystick.Horizontal <= -0.2f)
                playerRigidbody.AddForce(new Vector3(-1, 0, 0), ForceMode.Impulse);
        }

        public void ApplyBoost(TypeBoost typeBoost)
        {
            switch (typeBoost)
            {
                case TypeBoost.Default:
                    break;
                case TypeBoost.SpeedUp:
                    StartCoroutine(StartBoost());
                    break;
            }
        }

        IEnumerator StartBoost()
        {
            playerRigidbody.AddForce(new Vector3(0, -10, 0), ForceMode.Impulse);
            maxSpeed = -30;
            boostActive = true;
            yield return new WaitForSeconds(3);
            boostActive = false;
            maxSpeed = -20;
        }
    }
}
