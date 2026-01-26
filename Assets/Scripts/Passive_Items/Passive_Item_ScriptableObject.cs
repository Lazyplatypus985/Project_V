using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PassiveItemScriptableObject", menuName ="ScriptableObjects/Passive Item")]

public class Passive_Item_ScriptableObject : ScriptableObject
{
    [SerializeField]
    float multiplier;
    public float Multiplier {  get =>  multiplier; private set => multiplier = value;}

}
