using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string nameOfWeapon = "";

    public string nameOfCharger = "";

    public Charger currentCharger;

    public Bullet bullet;

    public bool CanShoot() {
        if (nameOfCharger == currentCharger.type && currentCharger.IsWithBbs()) return true;

        return false;
    }

    public bool CanSetCharger(Charger charger) {
        if(charger != null)
        {
            if (nameOfCharger == charger.type) return true;
            return false;
        }

        return false;
    }

    public void SetCharger(Charger charger) {
        currentCharger = charger;
    }

    public Charger GetCharger()
    {
        return currentCharger;
    }

    public void DecreaseBBs() {
        currentCharger.ShootBullet();
    }
}
