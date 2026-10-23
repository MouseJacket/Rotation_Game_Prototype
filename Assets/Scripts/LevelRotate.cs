using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class LevelRotate : MonoBehaviour
{
    public float rotate;
    public float speed = 0.5f;
    public Volume rotatePost;
    public GameObject player;
    public float timeScaleVal;
    public GameManager gameMan;
    public bool used = false;
    void Start()
    {
        gameMan = FindFirstObjectByType<GameManager>();
    }
    void Update()
    {
        if (gameMan.rotPower>0 && !used)
        {
            rotate = Input.GetAxis("RotateLevel");
            if (gameMan.rotPower < 0.3)
            {
                rotate *= gameMan.rotPower / 0.3f;
            }
        }
        else
        {
            rotate = 0;
            used = true;
        }
        if (gameMan.rotPower>0.15)
        {
            used = false;
        }

        transform.RotateAround(player.transform.position,Vector3.forward,rotate*speed);
        rotatePost.weight = Math.Abs(rotate);
        Time.timeScale = 1-(Math.Abs(rotate)*timeScaleVal);
        player.GetComponent<Rigidbody>().linearVelocity *= 1-Math.Abs(rotate);
    }
}
