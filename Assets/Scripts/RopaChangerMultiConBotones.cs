using UnityEngine;
using UnityEngine.UI;

public class RopaChangerMultiConBotones : MonoBehaviour
{
    [Header("Prendas")]
    public GameObject[] prendasSuperiores;
    public GameObject[] prendasComplementarias;
    public GameObject[] prendasInferiores;
    public GameObject[] calzados;

    [Header("Paneles de Botones")]
    public GameObject panelSuperiores;
    public GameObject panelComplementarias;
    public GameObject panelInferiores;
    public GameObject panelCalzado;

    private GameObject ropaActualSuperior;
    private GameObject ropaActualComplementaria;
    private GameObject ropaActualInferior;
    private GameObject calzadoActual;

    void Start()
    {
        // Apagar todas las prendas al inicio
        ApagarGrupo(prendasSuperiores);
        ApagarGrupo(prendasComplementarias);
        ApagarGrupo(prendasInferiores);
        ApagarGrupo(calzados);

        // Configurar botones dinámicamente (opcional, si no quieres asignarlos manualmente)
        ConfigurarBotones(panelSuperiores, prendasSuperiores, CambiarSuperior);
        ConfigurarBotones(panelComplementarias, prendasComplementarias, CambiarComplementaria);
        ConfigurarBotones(panelInferiores, prendasInferiores, CambiarInferior);
        ConfigurarBotones(panelCalzado, calzados, CambiarCalzado);
    }

    void ConfigurarBotones(GameObject panel, GameObject[] prendas, UnityEngine.Events.UnityAction<int> accion)
    {
        if (panel == null) return;

        Button[] botones = panel.GetComponentsInChildren<Button>();
        for (int i = 0; i < botones.Length && i < prendas.Length; i++)
        {
            int index = i; // Capturar el índice para el listener
            botones[i].onClick.AddListener(() => accion(index));
        }
    }

    void ApagarGrupo(GameObject[] grupo)
    {
        foreach (var prenda in grupo)
            prenda.SetActive(false);
    }

    public void CambiarSuperior(int index)
    {
        if (ropaActualSuperior != null)
            ropaActualSuperior.SetActive(false);

        ropaActualSuperior = prendasSuperiores[index];
        ropaActualSuperior.SetActive(true);
    }

    public void CambiarComplementaria(int index)
    {
        if (ropaActualComplementaria != null)
            ropaActualComplementaria.SetActive(false);

        ropaActualComplementaria = prendasComplementarias[index];
        ropaActualComplementaria.SetActive(true);
    }

    public void CambiarInferior(int index)
    {
        if (ropaActualInferior != null)
            ropaActualInferior.SetActive(false);

        ropaActualInferior = prendasInferiores[index];
        ropaActualInferior.SetActive(true);
    }

    public void CambiarCalzado(int index)
    {
        if (calzadoActual != null)
            calzadoActual.SetActive(false);

        calzadoActual = calzados[index];
        calzadoActual.SetActive(true);
    }
}