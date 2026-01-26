using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinachPassiveItem : Passive_Item
{
    protected override void AplyModifier()
    {
        player.currentMight *= 1 + PassiveItemData.Multiplier / 100;
    }
}
