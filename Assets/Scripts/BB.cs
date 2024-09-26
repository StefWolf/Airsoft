using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BB : MonoBehaviour
{
    private Rigidbody rb;
    private float backspinDrag;
    [SerializeField] private float lifeTime = 20f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(this.gameObject, lifeTime);
    }

    public void SetBackspinDrag(float newBackspinDrag)
    {
        backspinDrag = newBackspinDrag;
    }

    void FixedUpdate()
    {
        ApplyMagnusEffect();
    }

    private void ApplyMagnusEffect()
    {
        float speed = rb.velocity.magnitude;
        float liftForce = Mathf.Sqrt(speed) * backspinDrag;
        
        Vector3 perpendicularDirection = Vector3.up.normalized;

        rb.AddForce(perpendicularDirection * liftForce * Time.fixedDeltaTime, ForceMode.Force);
    }
}
