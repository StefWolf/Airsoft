using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BB : MonoBehaviour
{
    private float backspinDrag;
    private Rigidbody rb;
    private Transform gunTransform;
    [SerializeField] private float lifeTime = 20f;
    

    private LineRenderer lineRenderer;
    private List<Vector3> positions = new List<Vector3>();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(this.gameObject, lifeTime);

        // LineRenderer
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.red;
    }

    public void SetBackspinDrag(float newBackspinDrag)
    {
        backspinDrag = newBackspinDrag;
    }

    public void SetGunTransform(Transform gun)
    {
        gunTransform = gun;
    }

    void FixedUpdate()
    {
        ApplyMagnusEffect();
        UpdateLineRenderer();
    }

    private void ApplyMagnusEffect()
    {
        float speed = rb.velocity.magnitude;
        float liftForce = Mathf.Sqrt(speed) * backspinDrag;

        if (gunTransform != null)
        {
            Vector3 forwardDirection = gunTransform.forward;
            Vector3 gunUp = gunTransform.up;
            Vector3 perpendicularDirection = gunUp.normalized;
            rb.AddForce(perpendicularDirection * liftForce * Time.fixedDeltaTime, ForceMode.Force);
        }
    }

    private void UpdateLineRenderer()
    {
        positions.Add(transform.position);

        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());
    }
}
