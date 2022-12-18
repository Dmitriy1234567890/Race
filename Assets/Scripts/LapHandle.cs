using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapHandle : MonoBehaviour
{
    [SerializeField] int laps;
    [SerializeField] Checkpoint[] checkpoints;
    [SerializeField] Text lapText;


    int currentLap = 1;
    internal int finalCheckpoint;
    Car car;

    // Start is called before the first frame update
    void Start()
    {
        finalCheckpoint = checkpoints.Length - 1;

        for (int i = 0; i<=finalCheckpoint; i++) 
        {
            checkpoints[i].number = i;
        }
        car = FindObjectOfType<Car>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextCheckpoint(int number)
    {
        if (car.currentCheckpoint != number - 1)
        {
            return;
        }
        car.currentCheckpoint = number;
        checkpoints[number].Show(false);
        if (number==finalCheckpoint)
        {
            NextLap();
        }
        checkpoints[number+1].Show(true);
    }
    public void NextLap()
    {
        currentLap++;
        lapText.text = $"Круги: {currentLap}/2";
        car.currentCheckpoint = -1;
        checkpoints[0].Show(true);
        return;
    }
}
