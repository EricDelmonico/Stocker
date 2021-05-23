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

        blackPanel.SetActive(true);

        warningPanel.SetActive(true);
        StartCoroutine(HideWarning());
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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

    public void TransitionToPlay()
    {
        blackPanelAnim.SetBool("FadeOut", true);

        while (true)
        {
            if (blackPanelAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !blackPanelAnim.IsInTransition(0))
            {
                Play();
                break;
            }

        }

    }
        
}
