using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    public Shader replacementShaderTxtr;
    public Shader replacementShaderClr;
    public Camera MiniMapCam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  
    void OnValidate()
    {
        
        GetComponent<Camera>().SetReplacementShader(replacementShaderTxtr, "RenderType");
        GetComponent<Camera>().SetReplacementShader(replacementShaderClr, "RenderType");

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
