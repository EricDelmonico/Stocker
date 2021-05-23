using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerBase : MonoBehaviour
{
    public GameObject blackPanel;
    protected Animator blackPanelAnim;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        blackPanelAnim = blackPanel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Black transitions panel functionality
    public void FadeIn() => blackPanelAnim.SetBool("FadeIn", true);

    public void FadeOut() => blackPanelAnim.SetBool("FadeOut", true);
    
    //Play game
    public void Play() => SceneManager.LoadScene(1);

}
