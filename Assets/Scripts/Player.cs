using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    public string namePlayer = "player";

    public TextMeshProUGUI textAmmunition;

    public TextMeshProUGUI textInfoWeapon;

    public TextMeshProUGUI textMass;

    public Weapon currentWeapon;
    
    void Start()
    {
        
    }

   
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (currentWeapon != null)
            {
                if (currentWeapon.CanShoot())
                {
                    currentWeapon.bullet.Shoot();
                    currentWeapon.GetCharger().ShootBullet();
                    textAmmunition.text = currentWeapon.GetCharger().currentValue.ToString();
                }
            }
        }
    }
    public Camera playerCamera; // Refer�ncia � c�mera do player

    public void OnTriggerStay(Collider collision)
    {
        if (Input.GetKeyDown(KeyCode.E)) // Pegando uma arma
        {
            if (collision.gameObject.CompareTag("Weapon"))
            {
                currentWeapon = collision.gameObject.GetComponent<Weapon>();
                if (currentWeapon.GetCharger() != null)
                {
                    textAmmunition.text = currentWeapon.GetCharger().currentValue.ToString();
                }
                else
                {
                    textAmmunition.text = "0";
                }

                Rigidbody weaponRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                if (weaponRigidbody != null)
                {
                    weaponRigidbody.isKinematic = true; // Desabilitar completamente a f�sica
                }

                // Definir a arma como filha da c�mera do jogador
                collision.transform.SetParent(playerCamera.transform);

                // Ajustar a posi��o e rota��o da arma em rela��o � c�mera
                collision.transform.localPosition = new Vector3(-0.3225671f, -0.21f, 1.73f); // Ajuste conforme necess�rio
                collision.transform.localRotation = Quaternion.identity;

                textInfoWeapon.text = currentWeapon.name;
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) // Recarregando arma
        {
            if (currentWeapon != null)
            {
                Charger c = collision.gameObject.GetComponent<Charger>();
                if (currentWeapon.CanSetCharger(c))
                {
                    Debug.Log("� o carregador certo");
                    currentWeapon.SetCharger(c);
                    textAmmunition.text = currentWeapon.GetCharger().currentValue.ToString();

                    Rigidbody chargerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                    if (chargerRigidbody != null)
                    {
                        chargerRigidbody.isKinematic = true; // Desabilitar completamente a f�sica
                    }

                    collision.transform.SetParent(playerCamera.transform.Find("MP5").transform);
                }
            }
        }
    }



}
