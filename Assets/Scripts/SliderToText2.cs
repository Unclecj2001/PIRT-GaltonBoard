using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderToText2 : MonoBehaviour
{
        //Text Fields
    [SerializeField]
    private Text _BallsToSpawnText, _PegRowsToSpawnText, _RacksToSpawnText, _BallScaleText, _BallBouncinessText;

        //Slider Fields
    [SerializeField]
    private Slider _BallsToSpawnSlider, _PegRowsToSpawnSlider, _RacksToSpawnSlider, _BallScaleSlider, _BallBouncinessSlider;
    

        //Values
        [SerializeField]
    private int _BallsToSpawn, _PegRowsToSpawn, _RacksToSpawn, _BallBouncinessToChange, _Spawned;
    private float _BallScaleMultiplier;

        //Boolean Triggers
    private bool _PlaySim = false;
    private bool _PlayPressed = false;

        //Prefab Objects
    [SerializeField]
    private GameObject Ball, Peg, Rack, Detector, Text;

        //Text (forgot i had a previous text field and cant be bothered to re serialize it if i change it over)
    [SerializeField]
    private Text _ballsSpawned;

        // A scale for the ball scaling slider
    private Vector3 _Scale;


    void Start()
    {
        //This double checks to make sure the sliders are on the correct value at the start of simulation
        OnValueChange();
        //Sets the value of current balls spawned to zero
        _Spawned = 0;
    }

    private void Update()
    {
        //This detects if the play button has been pressed
        if (_PlaySim == true)
        {
            //Begin spawning balls - probably could get rid of the begin spawning method and replace this the coroutine
            BeginSpawning();
            //Prevents pressing play multiple times - prevents starting the ball spawning multiple times
            _PlaySim = false;
        }
    }

    //Updating the value of sliders upon user change
    public void OnValueChange()
    {
            //Slider value conversion to text
        _BallsToSpawnText.text      = _BallsToSpawnSlider.value.ToString();
        _PegRowsToSpawnText.text    = _PegRowsToSpawnSlider.value.ToString();
        _RacksToSpawnText.text      = _RacksToSpawnSlider.value.ToString();
        _BallScaleText.text         = _BallScaleSlider.value.ToString();
        _BallBouncinessText.text    = _BallBouncinessSlider.value.ToString();
            //Slider value conversion to variable
        _BallsToSpawn               = (int) _BallsToSpawnSlider.value;
        _PegRowsToSpawn             = (int) _PegRowsToSpawnSlider.value;
        _RacksToSpawn               = (int) _RacksToSpawnSlider.value;
        _BallScaleMultiplier        = _BallScaleSlider.value;
        _BallBouncinessToChange     = (int) _BallBouncinessSlider.value;
    }

    //Tells the play button what to do when pressed
    public void PlayButtonPressed()
    {
        //checks to see if the play button was pressed once already / if the sim is already running or not
        if (_PlayPressed == false)
        {
                //Begin The Simulation
            _PlaySim = true;
                //Prevent repeated button press
            _PlayPressed = true;
        }
    }

    public void UnpressButton()
    {
        //Called from the SettingsMenu.cs when the settings button is pressed - allows pressing play again
        _PlayPressed = false;
    }

    public void StopEveryCoroutine()
    {
        //Full stop all spawning coroutines - called from SettingsMenu.cs
        StopAllCoroutines();
    }

    //This could be an unnecessary method
    public void BeginSpawning()
    {
        StartCoroutine("SpawnBalls");
    }

    //Called from SettingsMenu.cs - instantiates all pegs and racks
    public void BeginSpawningCloseSettings()
    {
        StartCoroutine("SpawnRacks");
        StartCoroutine("SpawnPegs");
    }

    //Ball spawner
    IEnumerator SpawnBalls()
    {
        for (int BallCount = 0; BallCount < _BallsToSpawn; BallCount++)
        {
            float ballScale = _BallScaleMultiplier / 10f;
            _Scale = new Vector3(ballScale, ballScale, ballScale);
            Ball.transform.localScale = _Scale;

            Instantiate(Ball, new Vector3(Random.Range(-4f, 4f), Random.Range(22f, 26f), 0), Quaternion.identity);
            _Spawned++;
            _ballsSpawned.text = _Spawned.ToString();
            yield return new WaitForSeconds(ballScale/5);
        }
    }

    //Peg spawner
    IEnumerator SpawnPegs()
    {
        //Initial y value for pegs to spawn
        int yStartValue = 18;
        //Switch case changer
        int switchCase;

        float pegScale = _BallBouncinessToChange;
        _Scale = new Vector3(pegScale, pegScale, pegScale);
        Peg.transform.localScale = _Scale;

        if (_PegRowsToSpawn == 0)
        {
            switchCase = 0;
        } else
        {
            switchCase = 1;
        }

        switch (switchCase)
        {
            //Do nothing
            case 0:
                Debug.Log("Case 0");
                break;

            //Do something
            case 1:
                for (int PegRow = 0; PegRow < _PegRowsToSpawn + 1; PegRow++)
                {
                    int yPegPosition = yStartValue - (PegRow / 2);
                    int xPegPosition = 5 + (PegRow / 2);
                    float xOddPegPosition = 4.5f + (PegRow / 2);
                    int pegsInRow = 10 + (PegRow);
                    Debug.Log("Peg Columns: " + PegRow);

                    //spawn odd number rows
                    if (PegRow == 1 || PegRow == 3 || PegRow == 5 || PegRow == 7 || PegRow == 9 || PegRow == 11 || PegRow == 13 || PegRow == 15 || PegRow == 17)
                    {
                        for (int CurrentPeg = 0; CurrentPeg < pegsInRow; CurrentPeg++)
                        {
                            Debug.Log("Case 1 - odd number of rows");
                            Instantiate(Peg, new Vector3(-xPegPosition, yPegPosition, 0), Quaternion.identity);
                            xPegPosition--;
                        }
                    }

                    //Spawn even number rows (had to be seperated due to an offset in instantiation)
                    if (PegRow == 2 || PegRow == 4 || PegRow == 6 || PegRow == 8 || PegRow == 10 || PegRow == 12 || PegRow == 14 || PegRow == 16)
                    {
                        for (int CurrentPeg = 0; CurrentPeg < pegsInRow; CurrentPeg++)
                        {
                            Debug.Log("Case 2 - even number of rows");
                            Instantiate(Peg, new Vector3(-xPegPosition + 0.5f, yPegPosition + 0.5f, 0), Quaternion.identity);
                            xPegPosition--;
                        }
                    }
                }
            break;
        }

    Debug.Log("Done");
    return null;
    }


    //Rack & Detector spawner
    IEnumerator SpawnRacks()
    {
        float wallToWall = 28;
        float xDistanceBetweenRacks;
        xDistanceBetweenRacks = wallToWall / (_RacksToSpawn + 1);
        float commonDistance = xDistanceBetweenRacks;
        Debug.Log(xDistanceBetweenRacks);

        //Dectectors
        for (int Detectors = 0; Detectors <= _RacksToSpawn; Detectors++)
        {
            Detector.transform.localScale = new Vector3((28 / _RacksToSpawn), .1f, .1f);
            Instantiate(Detector, new Vector3((14 - xDistanceBetweenRacks + 0.66666f), 8, 0), Quaternion.Euler(-90f, 0f, 0f));
            xDistanceBetweenRacks = xDistanceBetweenRacks + commonDistance;
        }

        //Resets instantiation position
        xDistanceBetweenRacks = wallToWall / (_RacksToSpawn + 1);

        //Racks
        for (int Racks = 1; Racks <= _RacksToSpawn; Racks++)
        {
            Instantiate(Rack, new Vector3((14 - xDistanceBetweenRacks), 4, 0), Quaternion.Euler(-90f, 0f, 0f));
            xDistanceBetweenRacks = xDistanceBetweenRacks + commonDistance;
        }
        return null;
    }
}
