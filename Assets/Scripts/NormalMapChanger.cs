using UnityEngine;

public class NormalMapChanger : MonoBehaviour
{
    [Header("Prendas superiores (camisas, chaquetas, etc.)")]
    public GameObject[] prendasSuperiores;

    [Header("Prendas inferiores (pantalones, faldas, etc.)")]
    public GameObject[] prendasInferiores;

    [Header("Calzado")]
    public GameObject[] calzado;

    [Header("Texturas Normal Map disponibles")]
    public Texture[] normalMapsSuperiores;
    public Texture[] normalMapsInferiores;
    public Texture[] normalMapsCalzado;

    // Cambia el normal map de prendas superiores
    public void CambiarNormalMapSuperior(int index)
    {
        CambiarNormalMapEnObjetos(prendasSuperiores, normalMapsSuperiores, index);
    }

    // Cambia el normal map de prendas inferiores
    public void CambiarNormalMapInferior(int index)
    {
        CambiarNormalMapEnObjetos(prendasInferiores, normalMapsInferiores, index);
    }

    // Cambia el normal map del calzado
    public void CambiarNormalMapCalzado(int index)
    {
        CambiarNormalMapEnObjetos(calzado, normalMapsCalzado, index);
    }

    // Función común para aplicar el normal map a un conjunto de objetos
    private void CambiarNormalMapEnObjetos(GameObject[] objetos, Texture[] normalMaps, int index)
    {
        if (index < 0 || index >= normalMaps.Length)
        {
            Debug.LogWarning("Índice fuera de rango para el normal map");
            return;
        }

        foreach (GameObject obj in objetos)
        {
            Renderer rend = obj.GetComponent<Renderer>();
            if (rend != null)
            {
                Material mat = rend.material; // Instancia única
                mat.EnableKeyword("_NORMALMAP");
                mat.SetTexture("_BumpMap", normalMaps[index]);
            }
        }
    }
}
