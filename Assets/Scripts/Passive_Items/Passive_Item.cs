using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passive_Item : MonoBehaviour
{
    protected PlayerStats player;
    public Passive_Item_ScriptableObject PassiveItemData;

    protected virtual void AplyModifier()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        AplyModifier();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
