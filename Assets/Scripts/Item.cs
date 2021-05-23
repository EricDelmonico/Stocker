using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField]
    protected GameObject[] barriersToLift;

    /// <summary>
    /// Will return the static dropped variable from each Banana/Pork/Gum
    /// </summary>
    protected abstract bool itemDropped { get; }
    
    public abstract void DropItem();

    private void Update()
    {
        if (barriersToLift != null && itemDropped)
        {
            for (int i = 0; i < barriersToLift.Length; i++)
            {
                barriersToLift[i].SetActive(false);
            }
            barriersToLift = null;
        }
    }
}
