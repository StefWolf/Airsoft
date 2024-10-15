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
    [SerializeField] private float delay = 0.22f;
    private bool waitingDelay, invokeFlag, fullAuto;
    private Weapon weapon;

    void Start()
    {
        fullAuto = false;
        waitingDelay = false;
        invokeFlag = false;
        weapon = GetComponent<Weapon>();
    }

    void Update()
    {
        if((Input.GetMouseButton(0) || fullAuto) && weapon.CanShoot() && !waitingDelay)
        {
            this.Shoot();
        }

        if (invokeFlag && waitingDelay) {
            invokeFlag = false;
            Invoke("SetWaitingDelayFalse", delay);
        }

        if (Input.GetKeyDown(KeyCode.F)){
            fullAuto = !fullAuto;
        }

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput != 0)
        {
            backspinDrag += scrollInput * scrollSensitivity;
            backspinDrag = Mathf.Max(0f, backspinDrag);
            Debug.Log("Backspin Drag atualizado: " + backspinDrag);
        }
    }

    private void SetWaitingDelayFalse() {
        this.waitingDelay = false;
    }

    public void Shoot()
    {
        invokeFlag = true;
        waitingDelay = true;
        GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        Rigidbody bulletRb = bulletInstance.GetComponent<Rigidbody>();
        
        float initialVelocity = Mathf.Sqrt((2 * energyInJoules) / bulletRb.mass);

        bulletRb.velocity = firePoint.transform.forward * initialVelocity;
        bulletInstance.GetComponent<BB>().SetBackspinDrag(backspinDrag);
        bulletInstance.GetComponent<BB>().SetGunTransform(transform);
        weapon.DecreaseBBs();

        // Debug.Log("initialVelocity: " + initialVelocity);
    }
}
