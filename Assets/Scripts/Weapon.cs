using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string nameOfWeapon = "";

    public string nameOfCharger = "";

    private Charger currentCharger;

    public bool CanShoot() {
        if (nameOfCharger == currentCharger.name && currentCharger.IsWithBbs()) return true;

        return false;
    }

    public bool CanSetCharger(Charger charger) {
        if (nameOfCharger == charger.name) return true;
        return false;
    }

    public void SetCharger(Charger charger) {
        currentCharger = charger;
    }

    public Charger GetCharger()
    {
        return currentCharger;
    }
}
