using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunStatus : MonoBehaviour
{
    public Text changeText;
    public int ammoLeft = 0;
    public int maxAmmo = 0;

    void Start()
    {
        
    }

    public void updateText(int ammoLeft, int maxAmmo)
    {
        changeText.text = ammoLeft + "/" + maxAmmo;
    }
}