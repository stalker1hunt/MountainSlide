using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MountainSlide.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMove : MonoBehaviour
    {
        private Rigidbody playerRigidbody;
        public DynamicJoystick Joystick;
        public bool InitPlayer;

        [SerializeField]
        private GameObject meshObject;

        [SerializeField]
        [Range(-100, 0)]
        private float maxSpeed;
        public float MaxSpeed { get { return maxSpeed; } }

        private float curentSpeed;
        public float CurentSpeed { get { return curentSpeed; } }

        [SerializeField]
        private float buffSpeed;

        bool boostActive;

        private void Awake()
        {
            playerRigidbody = GetComponent<Rigidbody>();
        }

        void OnGUI()
        {
            GUI.Label(new Rect(10, 100, 150, 100), "speed " + Mathf.Abs(CurentSpeed));
        }

        float smooth = 5.0f;
        float tiltAngle = 60.0f;

        private void FixedUpdate()
        {
            if (!InitPlayer) return;

            if (playerRigidbody != null)
            {
                curentSpeed = playerRigidbody.velocity.y;

                if (Joystick.Horizontal >= 0.2f || (Joystick.Horizontal <= -0.2f))
                {
     
                }

                float tiltAroundZ = Joystick.Horizontal * tiltAngle;
                Quaternion target = Quaternion.Euler(0, 0, tiltAroundZ);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
            }
            else
            {
                Debug.LogWarning("Rigid body not attached to player " + gameObject.name);
            }
        }
        [SerializeField]
        private float myDrag;
        Vector3 gForceVector = new Vector3(0, 9.81f, 0);

        //Кастомная гравитация
        private void Mass()
        {
            Vector3 newVelocity = playerRigidbody.velocity + gForceVector * playerRigidbody.mass * Time.deltaTime;
            newVelocity = newVelocity * Mathf.Clamp01(1f - myDrag * Time.deltaTime);
            playerRigidbody.velocity = -newVelocity;
        }

        private void RotationSubMesh()
        {
            float deltaRotation = curentSpeed * Time.deltaTime * 4;
            float rotationX = transform.localEulerAngles.x * deltaRotation;
            //var newRotation = Quaternion.Euler(rotationX, transform.localEulerAngles.y, transform.localEulerAngles.z);
            //Debug.Log(newRotation.eulerAngles.x);
            //meshObject.transform.localRotation = newRotation;
            var newRotation = new Vector3(rotationX, 0, 0);
            meshObject.transform.Rotate(newRotation);
        }

        private void DownSlide()
        {
            float x = -1f + 0.2f * Time.fixedDeltaTime;
            float z = -1f * buffSpeed + 0.2f * Time.fixedDeltaTime;
            playerRigidbody.AddForce(new Vector3(0, x, z), ForceMode.VelocityChange);
        }

        private void Interion()
        {
            Vector3 velocity = playerRigidbody.velocity;
            float drag = 0.9f;
            velocity.x *= drag;
            velocity.z *= drag;
            playerRigidbody.velocity = velocity;
        }

        private void ApplyInput()
        {
            float deltaRotation = Joystick.Horizontal * Time.deltaTime;

            //float rotationZ = transform.eulerAngles.z * deltaRotation;
            float rotationY = transform.eulerAngles.y * deltaRotation;

            rotationY = Mathf.Clamp(rotationY, -45, 45);
            //rotationZ = Mathf.Clamp(rotationZ, -45, 45);

            var newRotation =  Quaternion.Euler(transform.eulerAngles.x, rotationY, transform.eulerAngles.z);
            transform.rotation = newRotation;
        }

        public int xMax = 75;
        public int xMin = -75;
        public int zMax = 75;
        public int zMin = -75;

        float horizontal;
        float vertical;

        public float speed = 10;

        private void TestAngles()
        {
            horizontal = -Joystick.Horizontal * speed;
            vertical = Joystick.Horizontal * speed;

            Mathf.Clamp(horizontal, xMin, xMax);
            Mathf.Clamp(vertical, zMin, zMax);

            transform.localEulerAngles = new Vector3(vertical, 0, horizontal) * Time.deltaTime;

            print("Horizontal: " + horizontal);
            print("Vertical: " + vertical);

            print("rotation X: " + GetComponent<Rigidbody>().transform.localEulerAngles.x);
            print("rotation Z: " + GetComponent<Rigidbody>().transform.localEulerAngles.z);
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
            buffSpeed = 1.2f;
            maxSpeed = -30;
            boostActive = true;
            yield return new WaitForSeconds(3);
            boostActive = false;
            buffSpeed = 1f;
            maxSpeed = -20;
        }
    }
}
