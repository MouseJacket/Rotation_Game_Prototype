using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class DownSync : MonoBehaviour
{
    public GameManager gameMan;
    public float switchTime = 2f;
    public string state = "local";
    void Start()
    {
        gameMan = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        switch (state)
        {
            case "local":
                transform.up = gameMan.worldRotate;
                break;
            case "global":
                transform.up = Vector3.up;
                break;
        }
    }
    public void SwitchMode(bool endGame = false)
    {
        
        if (!endGame)
        {
            if (state != "toggle")
            {
                StartCoroutine(Switch(state == "local"));
            }
        }
        else
        {
            StartCoroutine(Switch(true));
        }
    }
    public IEnumerator Switch(bool toGlobal)
    {
        state = "toggle";
        float elapsed = 0f;
        Debug.Log(switchTime);
        while (elapsed < switchTime)
        {
            if (toGlobal)
            {
                transform.up = Vector3.Slerp(gameMan.worldRotate,Vector3.up,elapsed/switchTime);
            }
            else
            {
                transform.up = Vector3.Slerp(Vector3.up,gameMan.worldRotate,elapsed/switchTime);
            }
            elapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if (toGlobal)
        {
            transform.up = Vector3.up;
            state = "global";
        }
        else
        {
            transform.up = gameMan.worldRotate;
            state = "local";
        }
    }
}
