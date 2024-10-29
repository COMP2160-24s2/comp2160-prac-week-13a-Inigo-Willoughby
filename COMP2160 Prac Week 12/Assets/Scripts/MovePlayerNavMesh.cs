using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MovePlayerNavMesh : MonoBehaviour
{
    private PlayerActions playerActions;
    private InputAction mousePosition;
    private InputAction mouseClick;

    [SerializeField] private NavMeshAgent nmAgent;

    [SerializeField] private float speed = 5;
    [SerializeField] private LayerMask layerMask;
    private Vector3 destination;

    float PathLength(NavMeshPath path) {
        if (path.corners.Length < 2)
            return 0;
        
        float lengthSoFar = 0.0F;
        for (int i = 1; i < path.corners.Length; i++) {
            lengthSoFar += Vector3.Distance(path.corners[i - 1], path.corners[i]);
        }
        return lengthSoFar;
    }

    NavMeshPath path = nmAgent.path;

    void Awake()
    {
        playerActions = new PlayerActions();
        mousePosition = playerActions.Movement.Position;
        mouseClick = playerActions.Movement.Click;
    }

    void OnEnable()
    {
        mousePosition.Enable();
        mouseClick.Enable();
    }

    void OnDisable()
    {
        mousePosition.Disable();
        mouseClick.Disable();
    }

    void Start()
    {
        mouseClick.performed += GetDestination;
        destination = transform.position;
        nmAgent = this.GetComponent<NavMeshAgent>();
    }

    private void GetDestination(InputAction.CallbackContext context)
    {
        Vector2 position = mousePosition.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            destination = hit.point;
        }
        nmAgent.SetDestination(destination);
    }   

    void OnDrawGizmos(){
        if(path.corners!=null){
        for(int i=1; i<=path.corners.Length; i++){
          Gizmos.DrawLine(path.corners[i -1], path.corners[i])  ;        
        }
        }
    }     
}
