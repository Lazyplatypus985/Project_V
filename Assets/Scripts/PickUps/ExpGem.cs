using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpGem : PickUp, ICollectable
{
    public int expGranted;

    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseExp(expGranted);
    }

}
