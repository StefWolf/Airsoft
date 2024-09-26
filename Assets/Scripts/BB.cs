using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BB : MonoBehaviour
{
    private Rigidbody rb;
    private float backspinDrag;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(this.gameObject, 5f);
    }

    public void SetBackspinDrag(float newBackspinDrag)
    {
        backspinDrag = newBackspinDrag; // Atualiza o backspinDrag
    }

    void FixedUpdate()
    {
        ApplyMagnusEffect();
    }

    private void ApplyMagnusEffect()
    {
        float speed = rb.velocity.magnitude;
        Vector3 velocityDirection = rb.velocity.normalized;
        Vector3 perpendicularDirection = Vector3.Cross(velocityDirection, Vector3.up).normalized;

        float liftForce = Mathf.Sqrt(speed) * backspinDrag;

        rb.AddForce(perpendicularDirection * liftForce * Time.fixedDeltaTime, ForceMode.Force);
    }
}
