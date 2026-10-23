using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    private bool canJump = true;
    private Rigidbody _rb;
    private LevelRotate levRot;
    public GameObject playerCam;
    public GameObject projectionCam;
    public float sprint;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        levRot = FindFirstObjectByType<LevelRotate>();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        _rb.linearVelocity = new Vector3(
        move*speed*sprint*(1-Math.Abs(levRot.rotate))+_rb.linearVelocity.x*Time.deltaTime, 
        _rb.linearVelocity.y, 0f);
        
        if (canJump && Input.GetButtonDown("Jump"))
        {
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
        }
        if (Input.GetKeyDown(KeyCode.Tab)) //Just going to hard-code these for now
        {
            if (projectionCam.GetComponent<DownSync>().state != "toggle")
            {
                playerCam.SetActive(!playerCam.activeInHierarchy);
                projectionCam.GetComponent<DownSync>().SwitchMode();
            }
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprint = 2f;
        }
        else
        {
            sprint = 1f;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            FindFirstObjectByType<GameManager>().ResetProgress();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            canJump = true;
        }
    }
}