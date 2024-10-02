using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        
    }
    public void OnTriggerStay(Collider collision)
    {
        if (Input.GetKeyDown(KeyCode.E)) //Pegando uma arma
        {
            if (collision.gameObject.CompareTag("Weapon"))
            {
                currentWeapon = collision.gameObject.GetComponent<Weapon>();
                if(currentWeapon.GetCharger() != null) {
                    textAmmunition.text = currentWeapon.GetCharger().currentValue.ToString();
                } else
                {
                    textAmmunition.text = "0";
                }

                collision.transform.SetParent(transform);
                textInfoWeapon.text = currentWeapon.name;
                Debug.Log("Pegou a arma: " + currentWeapon.name);
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) { //Recarregando arma
            
            if (currentWeapon != null) {
                if(currentWeapon.CanSetCharger(collision.gameObject.GetComponent<Charger>())) {
                    currentWeapon.SetCharger(collision.gameObject.GetComponent<Charger>());
                }
            }
        }
    }


}
