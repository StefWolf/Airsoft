using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private GameObject firePoint;

    private float energyInJoules = 1.49f;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            Debug.Log("Atirou");
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        float initialVelocity = Mathf.Sqrt((2 * energyInJoules) /  bulletInstance.GetComponent<Rigidbody>().mass);
        Debug.Log("Initial Velocity is: " + initialVelocity); //convert to FPS
    }
}
