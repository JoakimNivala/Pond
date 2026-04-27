using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    public Shader replacementShader;
    public Camera MiniMapCam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  
    void OnValidate()
    {
        
        GetComponent<Camera>().SetReplacementShader(replacementShader, "RenderType");
        

    }

    void OnDisable()
    {
        GetComponent<Camera>().ResetReplacementShader();
    }

    private void Update()
    {
       MiniMapCam.orthographicSize += Input.mouseScrollDelta.y * 5.5f;
    }
}
