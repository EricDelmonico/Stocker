using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gum : Item
{
    public static bool dropped = false;
    public override void DropItem()
    {
        dropped = true;
    }
}