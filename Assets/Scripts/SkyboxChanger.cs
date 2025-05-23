using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material[] skyboxes;
    public Camera targetCamera;

    public void SetSkyboxByIndex(int index)
    {
        if (index >= 0 && index < skyboxes.Length)
        {
            RenderSettings.skybox = skyboxes[index];
            DynamicGI.UpdateEnvironment();
        }
        else
        {
            Debug.LogWarning("Índice de skybox fuera de rango: " + index);
        }
    }
}
