using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class FishMovement : MonoBehaviour
{
    public GameObject Bobber;
    public NavMeshAgent agent;
    public float range; //radius of sphere
    public Transform centerPoint; //centre of the area the agent wants to move around in
  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        centerPoint = GameObject.Find("PondCenter").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {


        if (Bobber == null)
        {
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
        else
        {
            Debug.Log("ok");
            transform.SetParent(Bobber.transform);
            transform.position = Bobber.transform.position;
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
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
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Bobber")
        {
            Bobber = other.gameObject;
        }
    }
}
