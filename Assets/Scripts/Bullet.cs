using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private float energyInJoules = 1.49f;
    [SerializeField] private float backspinDrag = 0.001f;

    void Start()
    {
        
    }

    void Update()
    {

        // Lógica para alterar o backspinDrag
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            backspinDrag += 0.0001f;
           // Debug.Log("Backspin Drag aumentado: " + backspinDrag);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            backspinDrag = Mathf.Max(0f, backspinDrag - 0.0001f);
           // Debug.Log("Backspin Drag diminuído: " + backspinDrag);
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
