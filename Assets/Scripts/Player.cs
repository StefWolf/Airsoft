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

    public GameObject textInfoGrab;

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
                if(currentWeapon.currentCharger != null) {
                    if (currentWeapon.CanShoot())
                    {
                        currentWeapon.bullet.Shoot();
                        currentWeapon.GetCharger().ShootBullet();
                        textAmmunition.text = currentWeapon.GetCharger().currentValue.ToString();
                    }
                }
            }
        }
    }
    public Camera playerCamera; // Referência à câmera do player

    public void OnTriggerStay(Collider collision)
    {
        
        if(collision.tag == "Weapon") {

            if(currentWeapon == null || collision.name != currentWeapon.name)
                ChangeTheInfoGrab("Press E to pick up the weapon");

            if (Input.GetKeyDown(KeyCode.E)) // Pegando uma arma
            {
                if (collision.gameObject.CompareTag("Weapon"))
                {
                    if (currentWeapon != null) //Se tiver uma arma em mãos, solta ela para petgar a outra
                    {
                        Rigidbody lastWeapon = currentWeapon.gameObject.GetComponent<Rigidbody>();
                        lastWeapon.isKinematic = false;
                        currentWeapon.transform.SetParent(null);
                    }

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
                        weaponRigidbody.isKinematic = true; // Desabilitar completamente a física
                    }

                    // Definir a arma como filha da câmera do jogador
                    collision.transform.SetParent(playerCamera.transform);
                    textInfoGrab.gameObject.SetActive(false);

                    // Ajustar a posição e rotação da arma em relação à câmera
                    collision.transform.localPosition = new Vector3(0.141f, -0.186f, 1.45f); // Ajuste conforme necessário
                    collision.transform.localRotation = Quaternion.identity;

                    textInfoWeapon.text = currentWeapon.name;
                    textMass.text = currentWeapon.bullet.bulletPrefab.gameObject.GetComponent<Rigidbody>().mass.ToString();
                }
            }

        } else if(collision.tag == "Charger") {

            if (currentWeapon == null || currentWeapon.GetCharger() == null || collision.name != currentWeapon.GetCharger().name)
                ChangeTheInfoGrab("Press R to reload the weapon");

            if (Input.GetKeyDown(KeyCode.R)) // Recarregando arma
            {
                if (currentWeapon != null)
                {
                    Charger c = collision.gameObject.GetComponent<Charger>();
                    if (currentWeapon.CanSetCharger(c))
                    {
                        Debug.Log("É o carregador certo");
                        if (currentWeapon.GetCharger() != null)
                        {
                            Rigidbody lastCharger = currentWeapon.GetCharger().gameObject.GetComponent<Rigidbody>();
                            lastCharger.isKinematic = false;
                            currentWeapon.GetCharger().transform.SetParent(null);
                        }

                        textInfoGrab.gameObject.SetActive(false);
                        currentWeapon.SetCharger(c);
                        textAmmunition.text = currentWeapon.GetCharger().currentValue.ToString();

                        Rigidbody chargerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                        if (chargerRigidbody != null)
                        {
                            chargerRigidbody.isKinematic = true; // Desabilitar completamente a física
                        }

                        //Uma gambiarra logo abaixo que precisa de solução
                        collision.transform.SetParent(playerCamera.transform.Find("MP5").transform);
                    }
                }
            }
        }       
    }

    public void OnTriggerExit(Collider other)
    {
        textInfoGrab.gameObject.SetActive(false);
    }

    private void ChangeTheInfoGrab(string text) {

        textInfoGrab.gameObject.SetActive(true);
        TextMeshProUGUI t = textInfoGrab.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
        t.text = text;
   
    }

    

    

}
