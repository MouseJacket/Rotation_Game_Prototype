using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public int pointsValue = 10;
    public bool inGame = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&inGame)
        {
            Debug.Log("Pickup");
            FindFirstObjectByType<GameManager>().AddScore(pointsValue);
            GetComponent<MeshRenderer>().enabled = false;
            inGame = false;
        }
    }
}