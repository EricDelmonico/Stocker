using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gum : Item
{
    public static bool dropped = false;
    protected override bool itemDropped => dropped;
    public override void DropItem()
    {
        dropped = true;
    }
}
