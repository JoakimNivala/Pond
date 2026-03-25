using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPCMasterScript : MonoBehaviour
{
    public static NPCMasterScript Instance;
    [SerializeField]
    private Queue<PathRequest> requestQueue = new Queue<PathRequest>();
    private bool isProcessing;

    struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public NavMeshQueryFilter filter;
        public System.Action<NavMeshPath, bool> callback;

        public PathRequest(Vector3 start, Vector3 end, NavMeshQueryFilter filter, System.Action<NavMeshPath, bool> cb)
        {
            this.pathStart = start;
            this.pathEnd = end;
            this.filter = filter;
            this.callback = cb;
        }
    }

    void Awake() => Instance = this;

    public void RequestPath(Vector3 start, Vector3 end, NavMeshQueryFilter filter, System.Action<NavMeshPath, bool> callback)
    {
        requestQueue.Enqueue(new PathRequest(start, end, filter, callback));
       
        TryProcessNext();
    }

  
    void TryProcessNext()
    {
        if (!isProcessing && requestQueue.Count > 0)
        {
            PathRequest currentRequest = requestQueue.Dequeue();
            isProcessing = true;
            StartCoroutine(CalculatePathRoutine(currentRequest));
        }
    }

    System.Collections.IEnumerator CalculatePathRoutine(PathRequest request)
    {
       
        NavMeshPath path = new NavMeshPath();

        NavMesh.CalculatePath(request.pathStart, request.pathEnd, request.filter, path);

        isProcessing = false;
        request.callback(path, path.status == NavMeshPathStatus.PathComplete);

        TryProcessNext();
        yield return null;
    }

    
}