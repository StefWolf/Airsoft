using TMPro;
using UnityEngine;

public class Target : MonoBehaviour
{
    private int hitCount = 0;
    public int maxHits = 3;
    public TextMeshProUGUI textCount;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BB"))
        {
            hitCount++;
            textCount.text = hitCount.ToString();
            if (hitCount >= maxHits)
            {
                Destroy(gameObject);
            }
            Destroy(collision.gameObject);
        }
    }
}
