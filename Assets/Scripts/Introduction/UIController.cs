using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public GameObject ui;
    public TextMeshProUGUI keyE;
    public TextMeshProUGUI keyQ;
    public TextMeshProUGUI keyTab;
    public Slider fillSlider;
    public GameManager gameMan;
    void Start()
    {
        gameMan = FindFirstObjectByType<GameManager>();
        fillSlider = FindFirstObjectByType<Slider>();
    }
    public void UpdateScore(int score)
    {
        scoreText.text = $"Pickups: {score}/{GameObject.FindGameObjectsWithTag("Pickup").Length}";
    }

    public void UpdateTimer(float time)
    {
        timerText.text = "Time: " + Mathf.Round(time).ToString();
    }
    void Update() //I know that there is a much better way of doing this but this works for now
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            keyE.color = Color.aquamarine;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            keyE.color = Color.lightSalmon;
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            keyQ.color = Color.aquamarine;
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            keyQ.color = Color.lightSalmon;
        }        

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            keyTab.color = Color.cyan;
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            keyTab.color = Color.white;
        }
        fillSlider.value = gameMan.rotPower;
    }
}