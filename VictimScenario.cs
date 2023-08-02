using UnityEngine;
using System;
using System.Collections;
using MonsterLove.StateMachine;
using UnityEngine.SceneManagement;
using VRStandardAssets.Utils;
//Need to use this to modify RawImage because it is a material that renders a graphic 
using UnityEngine.UI;
//First scenario where the player is the victim of a jamming attack
namespace VRAVE
{

	public class VictimScenario : MonoBehaviour
	{

		[SerializeField] private GameObject UserCar;
		[SerializeField] private GameObject UnsuspectingAI;



		private HUDController hudController;
		private HUDAudioController audioController;
		private HUDAsyncController hudAsyncController;
		private AudioSource ambientAudioSource;
		private CarAIControl carAI;
		private CarAIControl unsuspectingCarAI;
		private CarController carController;
		private GameObject hackerScenarioFrequency;



		void Update()
		{
            //If car is user and player presses "A"
            if (UserCar.GetComponent<CarAIControl>().isUser() && OVRInput.Get(OVRInput.Button.One))
            {
				
				//Name of audio file: changing-to-manual-mode
				//Files being played can be found in ReactionTimeAudioModel.cs
				audioController.playAudio(1);
				//Calls CarUserControl
				UserCar.GetComponent<CarUserControl>().m_Driving = true;
				UserCar.GetComponent<CarUserControl>().enabled = true;
				




			}
			if (UserCar.GetComponent<AICarCollisionChecker>().isCollision == true || UserCar.GetComponent<CarUserControl>().enabled == true && UserCar.GetComponent<CarUserControl>().m_Driving == false)
            {			//Calls method changeScene() after waiting four seconds
				Invoke("changeScene", 4);
			}

			
		}
  		//Change scene to the one that has instructions for the frequency scenario
		private void changeScene()
        {
			SceneManager.LoadScene("InstructionsFrequency");
		}
		//Changes texture of RawImage from camera view to none when ultrasonic sensors become jammed
		private void changeRawImage()
		{
			GameObject canvas;
			//Finds Canvas which is a child of Player object
			canvas = UserCar.transform.Find("Canvas").gameObject;
			GameObject screen;
			screen = canvas.transform.Find("Screen").gameObject;
			RawImage image = screen.GetComponent<RawImage>();
			image.texture = null;
		}

		void Awake()
		{
			carController = UserCar.GetComponent<CarController>();
			carAI = UserCar.GetComponent<CarAIControl>();


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
			//Jams the player by changing their screen's texture to null
			Invoke("changeRawImage", 2);
			resetIntersectionScenario();


		}
  		//Used to put cars in correct position as well as determine which cars appear in the scenario 
		private void resetIntersectionScenario()
		{
			UserCar.SetActive(true);
			carAI.enabled = true;

			UserCar.GetComponent<CarUserControl>().enabled = false;
			UnsuspectingAI.SetActive(true);

			UserCar.transform.position = new Vector3(26f, 0.01f, -18.3f);
			UserCar.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

			carController.ResetSpeed();
			//position

			UnsuspectingAI.transform.position = new Vector3(26f, 0.01f, -13f);
			UnsuspectingAI.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

			hudController.Clear();

		}


    }
}
