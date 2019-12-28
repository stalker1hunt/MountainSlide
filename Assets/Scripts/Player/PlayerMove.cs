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
                playerRigidbody.AddForce(new Vector3(10, 0, 0), ForceMode.Acceleration);
            else if (Joystick.Horizontal <= -0.2f)
                playerRigidbody.AddForce(new Vector3(-10, 0, 0), ForceMode.Acceleration);
        }

        public void ApplyBoost()
        {

        }
    }
}
