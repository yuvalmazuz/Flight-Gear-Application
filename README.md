# Explanation of the project

In this project, an application was built that will allow us to display flight data on a dedicated simulator and study them. Our users are flight researchers or pilots who want to view data, sampled at a certain rate during a flight.
The flight data includes the steering mode, speed, altitude direction, etc., and are recorded into a text file, which can be loaded in our app. The app will play the data like a movie from the beginning of the recording until the end, it will graphically display the plane in relation to the earth, the rudder position, and additional flight data in a number of different views, including a view designed to find anomalies in the data.

**This project includes the following elements:**

* Design Patters (MVVM)
* Client-Server architecture
* Usage of data structures
* Working with files
* Working with threads


**Installations are required for the project:**

* FlightGear (You can download the simulator from [here](https://www.flightgear.org/))
* WPF App (.NET Framework)
* Oxyplot


**installation instructions and initial run of the app:**

**Step one:** Download the flight simulator, after downloading the flight simulator go to *settings* and write the following lines in the lowest square:
*--generic = socket, in, 10,127.0.0.1,5400, tcp, playback_small*
*--fdm = null*
We will explain these lines: We set the simulator to open this time a TCP server on port 5400 and using a socket a client will connect to it and dictate data to it in the order defined in playback_small.xml at a rate of 10 times per second.
The definition --fdm = null tells him not to run a simulation.
**Next**, put the XML file in the *protocol* folder and then click *FLY*.
Once the simulator has finished loading, run the application (Make sure you have installed the Oxyplot), click on *get started*, then a window will open, click on *browse* and select the file of the flight data and finally click on *play*.



Documentation files are located in a folder named **UML**


Link to demo video [here](https://www.youtube.com/watch?v=2FmdOqeiMnY)

