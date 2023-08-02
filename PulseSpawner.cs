using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VRAVE
{
    //This script spawns pulses dictated by the spawnRate
    public class PulseSpawner : MonoBehaviour
    {

        [SerializeField] private GameObject pulse;
        //How often pulses spawn
        [SerializeField] public float spawnRate;
        [SerializeField] private GameObject UnsuspectingAI;
        [SerializeField] private GameObject Player;
        [SerializeField] public bool isUser;


        private float timer = 0f;
        private float totalTime = 0f;


        // Update is called once per frame
        void Update()
        {
            totalTime += Time.deltaTime;
            // Spawns after certain amount of time to avoid constantly spawning
            // If spawnrate is 50, pulse will spawn about every 0.12 seconds
            if (timer < 10/spawnRate)
            {
                timer += Time.deltaTime;
            }
            else if (spawnRate != 0f)
            {
                spawnPulse();
                timer = 0f;
            }
            
        }
        void spawnPulse()
        //Uses position off user and victim car to determine where to spawn pulse
        //Add or subtract a little bit to have it appear as if the pulses spawn from back of user's car (where the jammer is) or spawn from front of victim's car 
        //(where ultrasonic sensor is)
        {   if (isUser)
            {
                transform.position = Player.transform.position - new Vector3(0f, 0f, 2.5f);
            }
            else
            {
                transform.position = UnsuspectingAI.transform.position + new Vector3(0f, 0f, 2.5f);
            }
            // Tells PulseMove.cs if it needs to run as the user (hacker) or the UnsuspectingAI (victim)
            pulse.GetComponent<PulseMove>().isUser = isUser;
            // Actually spawns the pulse
            Instantiate(pulse, transform.position, transform.rotation);
    
        }
    }
}

