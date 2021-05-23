using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : Item
{
    public static bool dropped = false;
    protected override bool itemDropped => dropped;
    public void Start()
    {
        dropped = false;
    }

    public override void DropItem()
    {
        dropped = true;
    }
}
