using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class WendigoScript : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds5 = new WaitForSeconds(5);

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public NavMeshAgent agent;
    public float range; //radius of sphere
    public Transform centerPoint; //centre of the area the agent wants to move around in
    public Vector3 distanceToPlayer;
    public GameObject Player;
    public AudioSource AudioSource;
    public bool shot;
    private bool backStab;
    private bool isWaitingForPath;
    private Animator animator;
    private Vector3 WendigoRot;
    private Vector3 PlayerRot;


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
            return;


        }
        float distance = Vector3.Distance(Player.transform.position, transform.position);

        if (distance <= 170 && !shot)
        {
            agent.isStopped = false;
            AudioSource.enabled = true;
            animator.SetBool("Idle", false);
            // Optimization: Only request a new path if we aren't already waiting for one
            if (!isWaitingForPath)
            {
                RequestMove(Player.transform.position);
            }
        }
      
        if (distance < 4 && !shot)
        {
            PlayerRot = Player.transform.forward;
            WendigoRot = transform.forward;
            float fotProd = Vector3.Dot(PlayerRot, WendigoRot);
            if (fotProd > 0) {
                backStab = true;
            }
            else {
                backStab = false;
            }
            switch (backStab)
            {
                case true:
                    animator.SetBool("Idle", false);
                    agent.isStopped = false;
                    agent.stoppingDistance = 0;
                    AudioSource.enabled = true;
                    break;
                case false:
                    animator.SetBool("Idle", true);
                    agent.isStopped = true;
                    AudioSource.enabled = false;
                    break;
            }
            

            
           
        }
        



        if (agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(centerPoint.position, range, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
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

        NPCMasterScript.Instance.RequestPath(transform.position, target, filter, OnPathFound);
    }

    // This is the "Callback" the Manager triggers
    void OnPathFound(NavMeshPath path, bool success)
    {
        isWaitingForPath = false; // We got an answer, we can request again later
        if (success && agent.enabled)
        {
            agent.SetPath(path);
        }
    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1000.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
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
            Vector3 point;
            Debug.Log("Wendigo shot");
            if (RandomPoint(centerPoint.position, range, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.speed = 100f;
                RequestMove(point);

                animator.SetBool("Run", true);
                StartCoroutine(RunAway());
            }
        }
        IEnumerator RunAway()
        {
            yield return _waitForSeconds5;
            {
                Debug.Log("Hello?");
                agent.speed = 4f;
                shot = false;
                animator.SetBool("Run", false);

            }


        }

    }

}
