using System;
using System.Collections;
using System.Diagnostics;
using System.Numerics;
using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public bool isGameOver = false;
    
    public float startingTime;
    public float timeLeft = 60f;
    public GameObject level;
    
    public UIController uiController;
    
    public GameObject player;
    public GameObject playerRespawn;
    public GameObject pickupPrefab;
    public UnityEngine.Vector3 worldRotate;
    public GameObject endScreen;
    public GameObject projectionCam;
    public float rotPower;
    public float rechargeSpeed;
    public float decaySpeed;
    public int deaths;
    
    private void Start()
    {
        startingTime = 60f;
    }

    void Update()
    {
        //update timer
        uiController.UpdateTimer(timeLeft);
        //Update score
        uiController.UpdateScore(score);


        worldRotate = level.transform.up;

        if (level.GetComponent<LevelRotate>().rotate != 0)
        {
            rotPower -= Time.unscaledDeltaTime*decaySpeed;
        }
        else
        {
            rotPower += Time.unscaledDeltaTime*rechargeSpeed;
        }
        if (rotPower < 0)
        {
            rotPower = 0;
        }
        if(rotPower>1)
        {
            rotPower = 1;
        }
        
        
        if (!isGameOver)//
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                EndGame(false);
            }
            if (score == GameObject.FindGameObjectsWithTag("Pickup").Length)
            {
                EndGame(true);
            }
        }
    }

    public void AddScore(int points)
    {
        score += points;
    }
    
    public void ResetProgress()
    {
        // Reset player's score to 0
        score = 0;
        uiController.UpdateScore(score);

        // Reset the timer to the starting time
        timeLeft = startingTime;
        uiController.UpdateTimer(timeLeft);

        isGameOver = false;
        FindFirstObjectByType<LevelRotate>().enabled = true;
        endScreen.SetActive(false);
        uiController.ui.SetActive(true);

        ResetPositions();
        deaths = 0;
        FindFirstObjectByType<LevelRotate>().GetComponent<LevelRotate>().rotate=0;



        // Reset All pickup items
        Array items = GameObject.FindGameObjectsWithTag("Pickup");
        foreach (GameObject pickup in items)
        {
            pickup.GetComponent<MeshRenderer>().enabled = true;
            pickup.GetComponent<PickupItem>().inGame = true;
        }
        items = GameObject.FindGameObjectsWithTag("TimePickup");
        foreach (GameObject pickup in items)
        {
            pickup.GetComponent<MeshRenderer>().enabled = true;
            pickup.GetComponent<TimerBoostItem>().inGame = true;
        }
        items = GameObject.FindGameObjectsWithTag("Roller");
        foreach (GameObject roller in items)
        {
            roller.transform.GetChild(0).transform.position = roller.transform.position;
        }
        //projectionCam.GetComponent<DownSync>().SwitchMode(false);
    }
    public void ResetPositions()
    {
        level.transform.localRotation = new UnityEngine.Quaternion(0,0,0,0);
        player.transform.position = playerRespawn.transform.position;
    }

    void EndGame(bool win)
    {
        isGameOver = true;
        FindFirstObjectByType<LevelRotate>().enabled = false;
        GameObject.Find("RotateEffects").GetComponent<Volume>().weight = 1;
        Time.timeScale=0.05f;
        endScreen.SetActive(true);
        uiController.ui.SetActive(false);
        //projectionCam.GetComponent<DownSync>().SwitchMode(true);
        TextMeshProUGUI statText = endScreen.transform.Find("StatText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI resultText = endScreen.transform.Find("ResultText").GetComponent<TextMeshProUGUI>();

        if (win)
        {
            resultText.text = "WIN";
            resultText.color = Color.aquamarine;
            statText.text = $"Time Left - {timeLeft} seconds"
            +$"\nDeaths - {deaths}";
            statText.color = Color.cyan;
        }
        else
        {
            resultText.text = "LOSE";
            resultText.color = Color.red;
            statText.text = $"Pickups Gathered - {score}/{GameObject.FindGameObjectsWithTag("Pickup").Length}"
            +$"\nDeaths - {deaths}";
            statText.color = Color.indianRed;
        }
        
    }
}