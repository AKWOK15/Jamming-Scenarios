using UnityEngine;
using System;
using System.Collections;
using MonsterLove.StateMachine;
using UnityEngine.SceneManagement;
using VRStandardAssets.Utils;
//Need to use this to modify RawImage because it is a material that renders a graphic 
using UnityEngine.UI;
using UnityStandardAssets.Utility;


//Scenario where player is the hacker and adjusts the frequency to try and jam the car behind it
namespace VRAVE
{

	public class FrequencyScenario : MonoBehaviour
	{
		[SerializeField] private GameObject UserCar;
		[SerializeField] private GameObject UnsuspectingAI;
		//Screen that represents what victim's ultrasonic sensors see and will appear as the right screen in the user's car
  		[SerializeField] private GameObject victimScreen;
    		//Camera that is used to capture what victim's ultrasonic sensors would see
		[SerializeField] private GameObject victimScreenCamera;
  		//Texture that victimSreenCamera produces and is then displayed on the victimScreen
		[SerializeField] private Texture victimView;
		// This texture is used to change the left screen of the user's car to the side view of the pulses
		[SerializeField] private Texture sideView;
		//Need this because this scenario has a default of no collision, if victim gets jammed, need to change the second waypoint of the victim's
  		//car to have a collision 
		[SerializeField] private WaypointCircuit circuit;
		//Slider object found in the canvas of the player
		[SerializeField] private Slider slider;
	



		private HUDController hudController;
		private HUDAudioController audioController;
		private HUDAsyncController hudAsyncController;
		private AudioSource ambientAudioSource;
		private CarAIControl carAI;
		private CarAIControl unsuspectingCarAI;
		//Waypoint that gets changed if user jams AI vehicle
		private GameObject waypoint;
		private Vector3 pos;
		private CarController carController;
		private CarController carAIController;






        void Update()
		{
			//Sets pulse spawnRate of user car equal to the value of the slider, "this" refers to object that script is attached to
			this.transform.GetChild(0).gameObject.GetComponent<PulseSpawner>().spawnRate = slider.value;
			// If user successfuly jams, AI car has its waypoint 001's (second waypoint) z coordinate changed so that cars get into a crash
			if (slider.value >= 40 && slider.value <= 50)
            {
				changeRawImage("VictimScreen", null);
                pos.z = 60;
                waypoint.transform.position = pos;
            }
	    		//Victim screen remains unaffected and still shows objects since jamming attack is unsuccessful 
			else
            {
				changeRawImage("VictimScreen", victimView);
				pos.z = 40;
				waypoint.transform.position = pos;
			}

            // If user gets in a collision or AI Car stops driving
            if (UserCar.GetComponent<AICarCollisionChecker>().isCollision == true || unsuspectingCarAI.m_Driving == false)
            {
                // Disables PulseSpawnerPlayer
                this.transform.GetChild(0).gameObject.SetActive(false);
                // Disables PulseSpawnerAI
                this.transform.GetChild(1).gameObject.SetActive(false);
            }


        }

        //Changes texture of RawImage of different screens (gives the screen a different camera view)
        private void changeRawImage(string screenName, Texture texture)
		{
			GameObject canvas;
			//Finds Canvas which is a child of Player
			canvas = UserCar.transform.Find("Canvas").gameObject;
			GameObject screen;
			screen = canvas.transform.Find(screenName).gameObject;
			RawImage image = screen.GetComponent<RawImage>();
			image.texture = texture;



		}

		void Awake()
		{
			carAI = UserCar.GetComponent<CarAIControl>();
			//Or else user still keeps getting called and will car will coast 
			UserCar.GetComponent<CarUserControl>().enabled = false;
			carController = UserCar.GetComponent<CarController>();


			hudController = UserCar.GetComponentInChildren<HUDController>();
			audioController = UserCar.GetComponentInChildren<HUDAudioController>();
			ambientAudioSource = GameObject.FindWithTag(VRAVEStrings.Ambient_Audio).GetComponent<AudioSource>();
			ambientAudioSource.mute = true;
			hudAsyncController = UserCar.GetComponentInChildren<HUDAsyncController>();

			audioController.audioModel = GameObject.FindObjectOfType<ReactionTimeAudioModel>();

			// configure HUD models
			hudController.models = new HUDModel[2];
			hudController.durations = new float[2];
			hudController.models[0] = new HUDVRAVE_Default();
			hudController.model = hudController.models[0];

			// configure ASYNC controller
			hudAsyncController.Configure(audioController, hudController);

         
            		unsuspectingCarAI = UnsuspectingAI.GetComponent<CarAIControl>();
			carAIController = UnsuspectingAI.GetComponent<CarController>();

			//Changes user tv screen to show side profile of cars
			changeRawImage("Screen", sideView);
			
			//Defines waypoint and pos variable so we can later use them to adjust car paths if does or does not jam
			waypoint = circuit.transform.GetChild(1).gameObject;
			pos = waypoint.transform.position;
			resetIntersectionScenario();


		}
		//Used to put cars in correct position as well as determine which cars appear in the scenario  
		private void resetIntersectionScenario()
		{
			UserCar.SetActive(true);
			carAI.enabled = true;
			

			UserCar.transform.position = new Vector3(26f, 0.01f, -11f);
			UserCar.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
			//carController.ResetSpeed();
			//carAIController.ResetSpeed();

			UnsuspectingAI.SetActive(true);
			UnsuspectingAI.transform.position = new Vector3(26f, 0.01f, -19);
			UnsuspectingAI.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
			hudController.Clear();
	
		}


	}
}
