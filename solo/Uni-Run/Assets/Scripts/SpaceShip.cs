using UnityEngine;

public class SpaceShip : MonoBehaviour
{   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
    }
}