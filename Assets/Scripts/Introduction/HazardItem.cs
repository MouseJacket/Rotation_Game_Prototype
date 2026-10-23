using UnityEngine;

public class HazardItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hazard");
            GameManager gameMan = FindFirstObjectByType<GameManager>();
            gameMan.ResetPositions();
            gameMan.deaths++;
        }
    }
}