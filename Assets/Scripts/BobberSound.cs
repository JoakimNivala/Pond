using UnityEngine;

public class BobberSound : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip audioClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount > 0)
        {
            AudioSource.PlayOneShot(audioClip);        
        }
    }
}
