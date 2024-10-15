using UnityEngine;

public class Target : MonoBehaviour
{
    private int hitCount = 0;
    public int maxHits = 3;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BB"))
        {
            hitCount++;

            if (hitCount >= maxHits)
            {
                Destroy(gameObject);
            }
            Destroy(collision.gameObject);
        }
    }
}
