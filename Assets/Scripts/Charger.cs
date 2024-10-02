using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{
    public string type = "MP5";

    public int capacity = 0;

    public int currentValue = 0;

    public void ShootBullet() {
        currentValue -= 1;
    }

    public bool IsWithBbs() {
        if(currentValue <= 0) return false;
        return true;
    }
}
