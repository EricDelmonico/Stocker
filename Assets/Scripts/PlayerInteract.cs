using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public static readonly string dropSpotTag = "DropSpot";

    [SerializeField]
    private float linecastDistance = 1;

    [SerializeField]
    private GameObject heldItem;

    public static readonly string[] holdableTags = 
    {
        "Banana",
        "Pork",
        "Gum"
    };

    void Update()
    {
        // linecast start and end points
        //
        // campos ---> campos + forward vec with a length of linecastDistance
        Vector3 lcStart = Camera.main.transform.position;
        Vector3 lcEnd = lcStart + Camera.main.transform.forward * linecastDistance;

        // Do the linecast
        Debug.DrawLine(lcStart, lcEnd, Color.cyan);
        RaycastHit hit;
        if (Physics.Linecast(lcStart, lcEnd, out hit, ~(68)))
        {
            // Interact with e
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Pick up the item if it has a holdable tag and the player is not already holding an item
                if (heldItem == null)
                {
                    for (int i = 0; i < holdableTags.Length; i++)
                    {
                        if (hit.collider.tag.Equals(holdableTags[i]))
                        {
                            PickupItem(hit.collider.gameObject);
                        }
                    }
                }
                // Otherwise check if it's a drop spot
                else if (hit.collider.tag.Equals(dropSpotTag + heldItem.tag))
                {
                    DropItem(hit.collider.transform.position);
                }
            }
        }
    }

    private void DropItem(Vector3 dropSpot)
    {
        ToggleActive(heldItem);
        heldItem.transform.position = dropSpot;
        heldItem.GetComponent<Item>().DropItem();
        heldItem = null;
    }

    private void PickupItem(GameObject item)
    {
        heldItem = item;
        ToggleActive(heldItem);
    }

    private void ToggleActive(GameObject item) => item.SetActive(!item.activeSelf);
}
