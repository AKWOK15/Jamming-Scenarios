using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
//Only gets used in victim scenario if player presses "A" on ocolus controller to swtich from AI (autonomous) to manual 
namespace VRAVE
{
    //Necessary requirement?
    //[RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
		[SerializeField] private bool m_allowReverse = true;

        private CarController m_Car; // the car controller we want to use
        private VisualSteeringWheelController m_SteeringWheel; //SteeringWheelController
        //Plays audio for tires
        private HUDAudioController audioController;

        private float handbrake = 0f; 
		public float Handbrake { get { return handbrake; } set { handbrake = value; } }
        //Is the user car driving 
        public bool m_Driving;


        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController> ();
            //Debug.Log("awake: " + m_Car);

            //get the steering wheel controller
            m_SteeringWheel = GetComponentInChildren<VisualSteeringWheelController> ();
            audioController = GetComponentInChildren<HUDAudioController>();
        }
        public void StopCar()
        {
            audioController.playAudio(6);
            m_Car.MaxSpeed = 0f;
            m_Driving = false;



        }

        public void StartCar()
        {
            Handbrake = 0f;
            m_Car.ReverseTorque = 0f;


        }
        private void onEnable()
        {
            //When switched to UserControl mode, expand steeringAngle
            //m_Car.MaxSteeringAngle = 60f;
        }
        private void changeScene()
        {
            SceneManager.LoadScene("InstructionsFrequency");
        }

        private void FixedUpdate()
        {
            GetComponent<CarAIControl>().enabled = false;
            // If user presses B button on right control
            if (OVRInput.Get(OVRInput.Button.Two))
            {
                StopCar ();
                Invoke("changeScene", 4);


            }
            //Debug.Log("Fixed update: CarUserControl");
            // pass the input to the car!
            //double h = gain * CrossPlatformInputManager.GetAxis("Horizontal");
            double hh = Input.GetAxis("Horizontal");
            //double v = gain * CrossPlatformInputManager.GetAxis("Vertical");
            double vv = Input.GetAxis("Vertical");

            //double h_raw = Input.GetAxisRaw("Horizontal");
            //double v_raw = Input.GetAxisRaw("Vertical");

            //Debug.Log("Horizontal: " + hh.ToString());
            //Debug.Log("Vertical: " + vv.ToString());


            /*
            #if !MOBILE_INPUT
                        float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            */
            //m_Car.Move(h, v, v, handbrake);

            /*#else*/

			if (!m_allowReverse) {
				if (vv < 0 && Math.Abs (m_Car.CurrentSpeed) < 1f) {
                    StopCar ();
				} else if (Handbrake > 0f) {
                    StartCar ();
				}
			}
            //Accelerates if trigger is held, if trigger is not pushed, trigger has default value of 0.01
            if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) >= 0.01f)
            {
                Debug.Log("hi");
                m_Car.Move((float)hh, 1.0f, (float)vv, Handbrake);
            }
            //Car drifts at current speed, no acceleration
            else
            {
                m_Car.Move((float)hh, (float)vv, (float)vv, Handbrake);
            }
            //m_Car.Move((float)hh, 1, (float)vv, Handbrake);
            m_SteeringWheel.turnSteeringWheel((float)hh, m_Car.CurrentSteerAngle);
/*#endif*/
        }
    }
}
