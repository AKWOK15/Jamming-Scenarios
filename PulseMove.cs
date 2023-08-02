using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Moves the pulses used in FrequencyScenario.cs
namespace VRAVE
{
    public class PulseMove : MonoBehaviour
    {
        //How far the pulses can go in front of the victim car before they get deleted
        private float DeadZone;
        private CarController controls
        private float moveSpeed;
        private GameObject car;
        public bool isUser;
        private PulseSpawner pulseSpawnerObject;
        private PulseSpawner script;
        private Renderer rend;
        // Start is called before the first frame update



        void Awake()
        {
            rend = GetComponent<Renderer>();
            DeadZone = 4f;
            //Pulses vary in speed for user and victim since user's pulses need to move slower to appear as if they are going backwards while victim's pulses need to move forward
            if (isUser)
            {
                car = GameObject.Find("Player");
                controls = car.GetComponent<CarController>();
            }
            else
            {
                //changes color of pulses to grey
                rend.material.color = Color.grey;
                car = GameObject.Find("UnsuspectingAIVehicle");
                controls = car.GetComponent<CarController>();
            }
        }


        // Update is called once per frame and is responsible for moving the pulses
        void Update()
        {
            moveSpeed = controls.CurrentSpeed;
            if (isUser)
            {
                transform.position += (new Vector3(0f, 0f, 0.3f) * moveSpeed) * Time.deltaTime;
                if (transform.position.z + DeadZone < car.transform.position.z)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                transform.position += (new Vector3(0f, 0f, 0.6f) * moveSpeed) * Time.deltaTime;
                if (transform.position.z > car.transform.position.z + DeadZone)
                {
                    Destroy(gameObject);
                }
            }

        }
    }
}

