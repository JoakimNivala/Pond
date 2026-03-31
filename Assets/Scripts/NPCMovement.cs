
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds5 = new WaitForSeconds(5);

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public NavMeshAgent agent;
    public float range; //radius of sphere
    public Transform centerPoint; //centre of the area the agent wants to move around in
    public Vector3 distanceToPlayer;
    public GameObject Player;
    public AudioSource AudioSource;
    private bool shot;
    public bool isWaitingForPath;
    private Animator animator;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        shot = false;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (shot)
        {
            int RandomAnim = Random.Range(1, 3);
            animator.SetBool("Shot"+RandomAnim, true);
            //Might want to delete the script entirely after this as well
            agent.enabled = false;
            
            StartCoroutine(DestroyScript());
            return;
        }


        float distance = Vector3.Distance(Player.transform.position, transform.position);
        if (distance <= 170)
        {
            AudioSource.enabled = true;
            // Optimization: Only request a new path if we aren't already waiting for one
            if (!isWaitingForPath)
            {
                
                RequestMove(Player.transform.position);
            }
            if (distance < 4)
            {
                animator.SetBool("Idle",true);
                agent.isStopped = true;
                AudioSource.enabled = false;
               
            }
            else
            {
                animator.SetBool("Idle", false);
                agent.isStopped = false;   
                AudioSource.enabled=true;
            }
        }
        else
        {
            AudioSource.enabled = false;
            
        }
        
        if (!isWaitingForPath && agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(centerPoint.position, range, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.red, 20.0f); //so you can see with gizmos
                RequestMove(point);
            }      
        }
    }

    void RequestMove(Vector3 target)
    {
        isWaitingForPath = true;
        NavMeshQueryFilter filter = new NavMeshQueryFilter();
        filter.agentTypeID = agent.agentTypeID;
        filter.areaMask = NavMesh.AllAreas;

        NPCMasterScript.Instance.RequestPath(transform.position, target, filter, OnPathFound
    );
    }

    // This is the "Callback" the Manager triggers
    void OnPathFound(NavMeshPath path, bool success)
    {
        Debug.Log("path found for:" + gameObject.name);
        isWaitingForPath = false; 
        if (success && agent.enabled)
        {
            agent.SetPath(path);
        }
    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
      
        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 20.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Pellet"))
        {
           
            shot = true;
        }
    }

    IEnumerator DestroyScript()
    {
        yield return _waitForSeconds5;
        animator.enabled = false;
        AudioSource.enabled = false;
        Destroy(this);

    }
  
}
