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

        void OnGUI()
        {
            GUI.Label(new Rect(10, 50, 150, 100), "speed " + Mathf.Abs(CurentSpeed));
        }

        private void FixedUpdate()
        {
            if (!InitPlayer) return;

            if (playerRigidbody != null)
            {
                ApplyInput();

                curentSpeed = playerRigidbody.velocity.y;

                if (MaxSpeed != 0 && CurentSpeed < MaxSpeed)
                {
                    curentSpeed = MaxSpeed;
                    playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, MaxSpeed, playerRigidbody.velocity.z);
                }
            }
            else
            {
                Debug.LogWarning("Rigid body not attached to player " + gameObject.name);
            }
        }

        private void ApplyInput()
        {
            float deltaRotation = Joystick.Horizontal * Time.deltaTime * 4;

            float rotationX = transform.eulerAngles.x;
            float rotationZ = transform.eulerAngles.z * deltaRotation;
            float rotationY = transform.eulerAngles.y * deltaRotation;

            rotationY = Mathf.Clamp(rotationY, -45, 45);
            rotationZ = Mathf.Clamp(rotationZ, -45, 45);

            var newRotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
            transform.rotation = newRotation;

            if (Joystick.Horizontal >= 0.2f || (Joystick.Horizontal <= -0.2f))
            {
                playerRigidbody.AddForce(new Vector3(-Joystick.Horizontal, 0, 0), ForceMode.VelocityChange);
            }
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
            //playerRigidbody.AddForce(new Vector3(0, -10, -10), ForceMode.VelocityChange);
            maxSpeed = -30;
            boostActive = true;
            yield return new WaitForSeconds(3);
            boostActive = false;
            maxSpeed = -20;
        }
    }
}
