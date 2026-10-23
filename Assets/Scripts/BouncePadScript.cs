using UnityEngine;

public class BouncePadScript : MonoBehaviour
{
    public float bounceThrust;
    void OnCollisionEnter(Collision other)
    {
    if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody>().
            AddRelativeForce(transform.up*bounceThrust, ForceMode.Impulse);
        }
    }
  private void On(Collider other)
    {
        
    }
}
