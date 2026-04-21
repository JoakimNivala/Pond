using UnityEngine;
using UnityEngine.UI;

public class MapScript : MonoBehaviour
{
    [SerializeField]
    private Terrain terrain;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Image playerSprite;
    [SerializeField]
    private Image map;
    [SerializeField]
    private Vector2 mapSize;

    public Vector2 playerPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        terrain = FindFirstObjectByType<Terrain>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        playerPos = new Vector2(player.position.x, player.position.y);
        playerSprite.rectTransform.position = playerPos;
    }
}
