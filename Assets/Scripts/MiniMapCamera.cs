using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    public RectTransform marker; //player pointer image
    public RectTransform mapImage;//Map screenshot used in canvas
    public Transform playerReference;//player
    public Transform[] mapEdges;//4 edges, make sure its in rectangle.
    public Vector2 offset;//Adjust the value to match you map

    private Vector2 mapDimentions;
    private Vector2 areaDimentions;

    private void Start()
    {
        if (mapEdges == null || mapEdges.Length < 2)
        {
            Debug.LogError("Assign at least 2 edges (Bottom-Left and Top-Right)!");
            return;
        }

        // Initialize min/max with the first point's values
        float minX = mapEdges[0].position.x;
        float maxX = mapEdges[0].position.x;
        float minZ = mapEdges[0].position.z;
        float maxZ = mapEdges[0].position.z;

        // Find the actual boundaries regardless of array order
        foreach (Transform edge in mapEdges)
        {
            if (edge.position.x < minX) minX = edge.position.x;
            if (edge.position.x > maxX) maxX = edge.position.x;
            if (edge.position.z < minZ) minZ = edge.position.z;
            if (edge.position.z > maxZ) maxZ = edge.position.z;
        }

        areaDimentions.x = maxX - minX;
        areaDimentions.y = maxZ - minZ;

        // Store the bottom-left corner to use as the "Zero" point
        // We'll repurpose mapEdges[0] as our reference origin
        mapEdges[0].position = new Vector3(minX, 0, minZ);

        mapDimentions = new Vector2(mapImage.sizeDelta.x, mapImage.sizeDelta.y);

        if (areaDimentions.x == 0 || areaDimentions.y == 0)
        {
            Debug.LogError("Edges are in the same spot! Move them in the Scene view.");
        }
    }

    private void Update()   
    {
        SetMarketPosition();
    }

    private void SetMarketPosition()
    {
        Vector3 distance = playerReference.position - mapEdges[0].position;
        Vector2 coordinates = new Vector2(distance.x / areaDimentions.x, distance.z / areaDimentions.y);
        marker.anchoredPosition = new Vector2(coordinates.x * mapDimentions.x, coordinates.y * mapDimentions.y) + offset;
        //marker.rotation = Quaternion.Euler(new Vector3(0, 0, -playerReference.eulerAngles.y));
        if (areaDimentions.x == 0 || areaDimentions.y == 0)
        {
            Debug.LogError("Map area dimensions are zero! Check your edge transforms.");
            return;
        }
        }
}
