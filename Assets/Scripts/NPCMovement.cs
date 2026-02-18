using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 
    public NavMeshAgent agent;
    public float range; //radius of sphere
    public Transform centerPoint; //centre of the area the agent wants to move around in
    public Vector3 distanceToPlayer;
    public GameObject Player;
    public AudioSource AudioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        distanceToPlayer = Player.transform.position - transform.position;
        if (distanceToPlayer.magnitude <= 170)
        {
            AudioSource.enabled = true;
            agent.SetDestination(Player.transform.position);
            return;
        }
        else
        {
            AudioSource.enabled = false;
        }
        
        if (agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(centerPoint.position, range, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);
            }
           
    }
            
      


    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Debug.Log("lol");
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
  
}
