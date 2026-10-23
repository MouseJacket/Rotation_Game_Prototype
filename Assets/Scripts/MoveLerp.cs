using System.Collections;
using UnityEngine;

public class MoveLerp : MonoBehaviour
{
    public Vector3 pos1;
    public Vector3 pos2;
    public float speed;
    public float waitTime;
    public string status;
    public bool waiting;
    public float percent;
    public float waitDelta;
    public GameManager gameMan;
    void Start()
    {
        transform.localPosition = pos1;
        status = "forward";
        gameMan = FindFirstObjectByType<GameManager>().GetComponent<GameManager>();
    }

    void Update()
    {
        switch (status)
        {
            case "forward":
                percent += Time.deltaTime*speed;
                transform.localPosition = Vector3.Lerp(pos1,pos2,percent);
                if (percent >= 1)
                {
                    percent = 1f;
                    status="wait1";
                }
                break;
            case "backward":
                percent -= Time.deltaTime*speed;
                transform.localPosition = Vector3.Lerp(pos1,pos2,percent);
                if (percent <= 0)
                {
                    percent = 0f;
                    status="wait2";
                }
                break;
            case "wait1" or "wait2":
                if (!waiting)
                {
                    StartCoroutine("Wait");
                }
                waitDelta += Time.deltaTime;
                break;
        }
    }
    IEnumerator Wait()
    {
        waiting = true;
        yield return new WaitForSeconds(waitTime);
        switch (status)
        {
            case "wait1":
                status = "backward";
                break;
            case "wait2":
                status = "forward";
                break;
        }
        waiting = false;
        waitDelta = 0f;
    }
}
