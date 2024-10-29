using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class Drone : MonoBehaviour
{
    [SerializeField] private UnityEngine.AI.NavMeshAgent nmAgent;
    [SerializeField] private MovePlayerNavMesh Player;
    // Start is called before the first frame update
    void Start()
    {
        nmAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>(); 
    }

    // Update is called once per frame
    void Update()
    {
        nmAgent.SetDestination(Player.transform.position);

    }
}
