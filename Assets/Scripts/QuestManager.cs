using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField]
    private int RequiredFish;

    [SerializeField]
    private int CurrentFishes;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RequiredFish = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateFishCount()
    {
        CurrentFishes++;
    }
}
