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

    public AudioSource walking;

    public AudioSource reloadWeapon;

    public TextMeshProUGUI fullautoText;

    void Start()
    {
        
    }

   
    void Update()
    {
        if (currentWeapon != null)
        {
            if(currentWeapon.currentCharger != null) {
                textAmmunition.text = currentWeapon.GetCharger().currentValue.ToString();
            }

            if (!currentWeapon.bullet.fullAuto)
            {
                fullautoText.text = "INACTIVE";
            }
            else
            {
                fullautoText.text = "ACTIVE";
            }
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (!walking.isPlaying) // Verifica se o som já está tocando
            {
                walking.Play();
            }
        }
        else
        {
            if (walking.isPlaying) // Para o som apenas se estiver tocando
            {
                walking.Stop();
            }
        }

    }
    public Camera playerCamera; // Refer�ncia � c�mera do player

    public void OnTriggerStay(Collider collision)
    {
        
        if(collision.tag == "Weapon") {

            if(currentWeapon == null || collision.name != currentWeapon.name)
                ChangeTheInfoGrab("Press E to pick up the weapon");

            if (Input.GetKeyDown(KeyCode.E)) // Pegando uma arma
            {
                if (collision.gameObject.CompareTag("Weapon"))
                {
                    if (currentWeapon != null) //Se tiver uma arma em m�os, solta ela para petgar a outra
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
                        weaponRigidbody.isKinematic = true; // Desabilitar completamente a f�sica
                    }

                    // Definir a arma como filha da c�mera do jogador
                    collision.transform.SetParent(playerCamera.transform);
                    textInfoGrab.gameObject.SetActive(false);

                    // Ajustar a posi��o e rota��o da arma em rela��o � c�mera

                    if(currentWeapon.name == "Pistola")
                    {
                        collision.transform.localPosition = new Vector3(0.025f, -0.171f, 0.418f);
                        collision.transform.localRotation = Quaternion.Euler(-2.592f, -186.5f, 0.147f);
                    }

                    if (currentWeapon.name == "MP5")
                    {
                        collision.transform.localPosition = new Vector3(0.141f, -0.186f, 1.45f);
                        collision.transform.localRotation = Quaternion.identity;
                    }


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
                        Debug.Log("� o carregador certo");
                        if (currentWeapon.GetCharger() != null)
                        {
                            Rigidbody lastCharger = currentWeapon.GetCharger().gameObject.GetComponent<Rigidbody>();
                            lastCharger.isKinematic = false;
                            currentWeapon.GetCharger().transform.SetParent(null);
                        }

                        textInfoGrab.gameObject.SetActive(false);
                        currentWeapon.SetCharger(c);
                        textAmmunition.text = currentWeapon.GetCharger().currentValue.ToString();
                        reloadWeapon.Play();
                        Rigidbody chargerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                        if (chargerRigidbody != null)
                        {
                            chargerRigidbody.isKinematic = true; // Desabilitar completamente a f�sica
                        }

                        //Uma gambiarra logo abaixo que precisa de solu��o
                     
                            collision.gameObject.SetActive(false);

                      
                           
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
