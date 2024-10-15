using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private float energyInJoules = 1.49f;
    [SerializeField] private float backspinDrag = 0.001f;
    [SerializeField] private float scrollSensitivity = 0.01f;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            Debug.Log("Atirou");
            this.Shoot();
        }

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput != 0)
        {
            backspinDrag += scrollInput * scrollSensitivity;
            backspinDrag = Mathf.Max(0f, backspinDrag);
            Debug.Log("Backspin Drag atualizado: " + backspinDrag);
        }
    }

    public void Shoot()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        Rigidbody bulletRb = bulletInstance.GetComponent<Rigidbody>();
        
        float initialVelocity = Mathf.Sqrt((2 * energyInJoules) / bulletRb.mass);

        bulletRb.velocity = firePoint.transform.forward * initialVelocity;
        bulletInstance.GetComponent<BB>().SetBackspinDrag(backspinDrag);
        bulletInstance.GetComponent<BB>().SetGunTransform(transform);

        // Debug.Log("initialVelocity: " + initialVelocity);
    }
}
