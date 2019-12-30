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
        [Range(0, 100)]
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
           // float delta = 2 * Joystick.Horizontal;
           
            if (Joystick.Horizontal >= 0.2f || (Joystick.Horizontal <= -0.2f))
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y * Joystick.Horizontal, 0);
            }
            //else
            //{
            //    //if (transform.eulerAngles.y > 45 || transform.eulerAngles.y < -45)
            //    //    transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, 0);
            //}

            float rotationZ = transform.eulerAngles.z * Joystick.Horizontal * Time.deltaTime * 5;
            float rotationY = transform.eulerAngles.y * Joystick.Horizontal * Time.deltaTime * 5;

            rotationY = Mathf.Clamp(rotationY, -45, 45);
            rotationZ = Mathf.Clamp(rotationZ, -45, 45);

            var newRotation = Quaternion.Euler(transform.localEulerAngles.x, rotationY, rotationZ);
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
            playerRigidbody.AddForce(new Vector3(0, -10, 0), ForceMode.Impulse);
            maxSpeed = -30;
            boostActive = true;
            yield return new WaitForSeconds(3);
            boostActive = false;
            maxSpeed = -20;
        }
    }
}
