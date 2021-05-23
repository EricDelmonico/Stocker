using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCondition : MonoBehaviour
{
    [SerializeField]
    private AIManager manager;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Detection") || other.CompareTag("Player"))
        {
            StartCoroutine(PlayDeathSoundThenGameOver());
        }
    }

    private IEnumerator PlayDeathSoundThenGameOver()
    {
        manager.PlaySound(Sounds.Found);
        manager.LockSounds();
        yield return new WaitUntil(() => !manager.jaredSource.isPlaying);
        SceneManager.LoadScene("LoseScreen");
    }
}
