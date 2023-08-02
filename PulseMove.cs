using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace VRAVE
{
    public class PulseMove : MonoBehaviour
    {
        //How far the pulses can go in front of the victim car before they get deleted
        private float DeadZone;


        private CarController controls;
        //private GameObject pulseSpawner;
        //private PulseSpawner script;

        private float moveSpeed;
        private GameObject car;
        public bool isUser;
        private PulseSpawner pulseSpawnerObject;
        private PulseSpawner script;
        private Renderer rend;
        // Start is called before the first frame update



        void Awake()
        {
            //pulseSpawnerObject = GetComponent<PulseSpawner>();

            //Debug.Log("pulseSpawnerObject:" + pulseSpawnerObject);
            //script = pulseSpawnerObject.GetComponent<PulseSpawner>();
            //isUser = script.isUser;
            //Debug.Log("isUser:" + isUser);
            rend = GetComponent<Renderer>();
            DeadZone = 4f;
            if (isUser)
            {
                car = GameObject.Find("Player");
                controls = car.GetComponent<CarController>();
            }
            else
            {
                rend.material.color = Color.grey;
                car = GameObject.Find("UnsuspectingAIVehicle");
                controls = car.GetComponent<CarController>();
            }
        }


        // Update is called once per frame
        void Update()
        {
            moveSpeed = controls.CurrentSpeed;
            if (isUser)
            {
                //transform.position = transform.position + (new Vector3(0f, 0f, -0.1f) * moveSpeed) * Time.deltaTime;
                transform.position += (new Vector3(0f, 0f, 0.3f) * moveSpeed) * Time.deltaTime;
                if (transform.position.z + DeadZone < car.transform.position.z)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                transform.position += (new Vector3(0f, 0f, 0.6f) * moveSpeed) * Time.deltaTime;
                //transform.position = transform.position + (new Vector3(0,0,1) * moveSpeed) *Time.deltaTime;
                //Debug.Log("transform.position"+ transform.position);
                if (transform.position.z > car.transform.position.z + DeadZone)
                {
                    Destroy(gameObject);
                }
            }

        }
    }
}

