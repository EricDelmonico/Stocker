using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIManager : MonoBehaviour
{
    
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    public void MoveToPosition(Vector3 position)
    {
        agent.destination = position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
