using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gum : Item
{
    [SerializeField]
    private Transform manager;
    [SerializeField]
    private Transform finalChaseLocation;

    public static bool dropped = false;
    protected override bool itemDropped => dropped;
    public override void DropItem()
    {
        dropped = true;
        manager.position = new Vector3(finalChaseLocation.position.x, manager.position.y, finalChaseLocation.position.z);
    }
}
