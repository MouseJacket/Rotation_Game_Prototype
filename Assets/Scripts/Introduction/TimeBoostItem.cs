using UnityEngine;

public class TimerBoostItem : MonoBehaviour
{
    public float timeBoost = 10f;
    public bool inGame = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&inGame)
        {
            FindFirstObjectByType<GameManager>().timeLeft += timeBoost;
            GetComponent<MeshRenderer>().enabled = false;
            inGame = false;
        }
    }
}