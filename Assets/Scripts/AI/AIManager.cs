using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Sounds
{
    Chase,
    Search,
    Patrol,
    Hear,
    Found
}

public class AIManager : MonoBehaviour
{

    NavMeshAgent agent;

    //Player information
    public GameObject player;
    Transform playerPos;
    MeshRenderer playerMesh;

    public Camera cam;

    public float hearingRange;

    //Animator
    Animator managerAnimator;

    //Conditions
    bool playerSpotted;

    // AudioSource
    public AudioSource jaredSource;

    //Waypoints
    public GameObject[] produceWP;
    public GameObject[] meatWP;
    public GameObject[] aisleWP;

    [SerializeField]
    private AudioClip[] searchSounds;
    [SerializeField]
    private AudioClip[] patrolSounds;
    [SerializeField]
    private AudioClip[] chaseSounds;
    [SerializeField]
    private AudioClip[] hearSounds;
    [SerializeField]
    private AudioClip[] gameOverSounds;

    private Dictionary<Sounds, AudioClip[]> allSounds;

    public float secondsBetweenPatrolSounds = 5;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        playerMesh = player.GetComponent<MeshRenderer>();
        playerPos = player.transform;

        managerAnimator = gameObject.GetComponent<Animator>();

        jaredSource = GetComponent<AudioSource>();

        allSounds = new Dictionary<Sounds, AudioClip[]>()
        {
            { Sounds.Chase, chaseSounds },
            { Sounds.Hear, hearSounds },
            { Sounds.Patrol, patrolSounds },
            { Sounds.Search, searchSounds },
            { Sounds.Found, gameOverSounds }
        };
    }

    public void MoveToPosition(Vector3 position)
    {
        agent.destination = position;
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if the manager can see the player
        Vector3 viewportPos = cam.WorldToViewportPoint(player.transform.position);
        if (viewportPos.x < 1 && viewportPos.x > 0
            && viewportPos.y < 1 && viewportPos.y > 0
            && viewportPos.z > 0)
        {
            Debug.DrawRay(transform.position, playerPos.position - transform.position, Color.red);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, playerPos.position - transform.position, out hit))
            {
                if (hit.collider.CompareTag("Detection"))
                {
                    playerSpotted = true;
                }
                else
                {
                    playerSpotted = false;
                }
            }
        }
        else
        {
            playerSpotted = false;
        }

        //Triggers the animator

        if (Gum.dropped)
        {
            if (!managerAnimator.GetBool("PlayerSpotted") && !jaredSource.isPlaying)
            {
                managerAnimator.SetBool("PlayerSpotted", true);
                PlaySound(Sounds.Chase);
            }
        }
        else if (playerSpotted)
        {
            if (!managerAnimator.GetBool("PlayerSpotted") && !jaredSource.isPlaying)
            {
                managerAnimator.SetBool("PlayerSpotted", true);
                PlaySound(Sounds.Chase);
            }
        }
        else if ( PlayerStealth.running || 
            (PlayerStealth.walking && Vector3.Distance(transform.position, playerPos.position) < hearingRange))
        {
            if (!managerAnimator.GetBool("PlayerSpotted") && !jaredSource.isPlaying)
            {
                managerAnimator.SetBool("PlayerSpotted", true);
                PlaySound(Sounds.Hear);
            }
        }
        else
        {
            managerAnimator.SetBool("PlayerSpotted", false);
        }
    }

    public void PlaySound(Sounds sound)
    {
        if (!lockSounds)
        {
            jaredSource.Stop();
            var soundsBank = allSounds[sound];
            jaredSource.PlayOneShot(soundsBank[Random.Range(0, soundsBank.Length)]);
        }
    }

    private bool lockSounds = false;
    public void LockSounds()
    {
        lockSounds = true;
    }

    public IEnumerator PlayPatrolSoundsPeriodically()
    {
        PlaySound(Sounds.Patrol);
        yield return new WaitForSeconds(secondsBetweenPatrolSounds);
        StartCoroutine(PlayPatrolSoundsPeriodically());
    }
}
