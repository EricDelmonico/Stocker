using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIManager : UIManagerBase
{
    public GameObject warningPanel;
    public float warningDuration;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        warningPanel.SetActive(true);
        StartCoroutine(HideWarning());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Shows the warning message briefly
    IEnumerator HideWarning()
    {
        yield return new WaitForSeconds(warningDuration);

        warningPanel.SetActive(false);
        FadeIn();
    }
}
