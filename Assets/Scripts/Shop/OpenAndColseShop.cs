using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndColseShop : MonoBehaviour
{
    public GameObject Shop;

    public void ToggleCanvas()
    {
        if (Shop != null)
        {
            Shop.SetActive(!Shop.activeSelf);
        }
    }
}


