using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsPassiveItem : Passive_Item
{
    protected override void AplyModifier()
    {
        player.currentMoveSpeed *= 1 + PassiveItemData.Multiplier /100f;
    }
}
