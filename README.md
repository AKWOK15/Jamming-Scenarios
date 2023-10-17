# Jamming-Attack-Simulation
## Background
For the summer of 2023, Dr. Sandip Ray accepted me into his Secure, Sustainable, and Accessible Transportation REU (Research for Experience Undergraduate) program, funded by the National Science Foundation. For my project, I researched cybersecurity challenges for autonomous vehicles. Specifically, what a jamming attack on vehicle ultrasonic sensors would look like in the eyes of both the victim and hacker. 

To best do this, Richard Owoputi, my Ph.D. mentor, decided to put the player through a VR scenario built by Unity. He laid down the basics, such as the cars, the overall map, and the car movement. I then built on this to create a victim and hacker scenario to help the user learn about jamming attacks.

## Built With
![Unity][Unity.com] \
![VisualStudio][VisualStudio.com] \
![C#][C#.com]

[Unity.com]:https://img.shields.io/badge/Unity-100000?style=for-the-badge&logo=unity&logoColor=white
[VisualStudio.com]:https://img.shields.io/badge/Visual_Studio_Code-0078D4?style=for-the-badge&logo=visual%20studio%20code&logoColor=white
[C#.com]:https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white

## Rationale Behind the Simulation
During a jamming attack, the victim’s car fails to detect the attack and therefore cannot alert the victim. This occurs because the ultrasonic sensors think that no objects exist around the car. To mimic this in my simulation, the victim’s car screen turns white when their car’s sensors become jammed. When not jammed, the screen depicts a view from the front of the car.

For the jamming attack to be successful, the jammer must be within 10 meters of the ultrasonic sensors, be aimed at the ultrasonic sensors, have a higher voltage power than the ultrasonic sensor, and be on the same frequency as the ultrasonic sensor, Since, I was not able to conduct real life jamming tests, I primarily depended on this paper to figure out the jammer requirements: https://ieeexplore.ieee.org/stamp/stamp.jsp?arnumber=8451864&tag=1. 

In this paper, the authors successfully attacked a Tesla from up to 10 meters away. The range depends on the voltage of the jammer, and in this experiment, they used 20 volts. Voltage is one of the independent variables for a successful attack because the voltage is proportional to the amplitude of the waves, amplitude determines the strength of the wave, and the jammer waves must overpower the ultrasonic waves. The jammer pulses decrease in strength (amplitude) as they move farther and farther out. As a result, if the jammer pulses reach the ultrasonic sensor, but do not have high enough amplitude to overpower the sensor’s own pulses, the attack will not work. To maintain this 10-meter distance, the hacker and victim car travel along a city street. 

The jammer must be aimed at the sensor to ensure that the jammer’s pulses can reach the sensor. This is why, in the hacker scenario, the jammer is placed directly in line with the ultrasonic sensor. 

Finally, the jammer must operate on the same frequency as the sensor to ensure that the sensor recognizes the jammer’s waves as its own. An ultrasonic sensor operates between 40-50 kHZ which is the range that the victim's car gets successfully hacked in the hacker scenario.

If the hacker succeeds, when the two cars approach the stop sign, the victim's car will crash into the hacker because the victim’s car fails to detect the hacker’s car. 

For future work, one could have the user control the voltage, aim the jammer, and even have to steer the car. To have a successful attack, a person would have to do all of this at once which shows that a jamming attack is possible but quite difficult.

## Demo
https://github.com/AKWOK15/Jamming-Attack-Simulation/assets/121518425/a1d6b3d2-a9bc-4854-a6c6-f2104c9193ef




