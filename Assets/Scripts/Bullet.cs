using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private float energyInJoules = 1.49f;
    [SerializeField] private float backspinDrag = 0.001f; // Valor inicial do backspin

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

        // Lógica para alterar o backspinDrag
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            backspinDrag += 0.0001f; // Aumenta o backspin
            Debug.Log("Backspin Drag aumentado: " + backspinDrag);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            backspinDrag = Mathf.Max(0f, backspinDrag - 0.0001f); // Diminui o backspin, sem deixar negativo
            Debug.Log("Backspin Drag diminuído: " + backspinDrag);
        }
    }

    void Shoot()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        Rigidbody bulletRb = bulletInstance.GetComponent<Rigidbody>();
        
        // Calcula a velocidade inicial
        float initialVelocity = Mathf.Sqrt((2 * energyInJoules) / bulletRb.mass);
        
        // Define a velocidade inicial da BB
        bulletRb.velocity = firePoint.transform.forward * initialVelocity;

        // Passa o valor de backspinDrag para a BB
        bulletInstance.GetComponent<BB>().SetBackspinDrag(backspinDrag);

        Debug.Log("Initial Velocity is: " + initialVelocity); // Verifica no Console
    }
}
