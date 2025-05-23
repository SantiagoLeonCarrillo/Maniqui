using UnityEngine;
using TMPro;

public class RopaChangerMulti : MonoBehaviour
{
    [Header("Dropdowns")]
    public TMP_Dropdown dropdownSuperiores;
    public TMP_Dropdown dropdownInferiores;
    public TMP_Dropdown dropdownCalzado;

    [Header("Prendas")]
    public GameObject[] prendasSuperiores;
    public GameObject[] prendasInferiores;
    public GameObject[] calzados;

    private GameObject ropaActualSuperior;
    private GameObject ropaActualInferior;
    private GameObject calzadoActual;

    void Start()
    {
        // Apaga todo al inicio
        ApagarGrupo(prendasSuperiores);
        ApagarGrupo(prendasInferiores);
        ApagarGrupo(calzados);

        // Asignar listeners para cada dropdown
        dropdownSuperiores.onValueChanged.AddListener(CambiarSuperior);
        dropdownInferiores.onValueChanged.AddListener(CambiarInferior);
        dropdownCalzado.onValueChanged.AddListener(CambiarCalzado);

        // Opcional: inicializar dropdowns en -1 o 0 para que no haya prendas activas al inicio
        dropdownSuperiores.value = -1;
        dropdownSuperiores.RefreshShownValue();

        dropdownInferiores.value = -1;
        dropdownInferiores.RefreshShownValue();

        dropdownCalzado.value = -1;
        dropdownCalzado.RefreshShownValue();
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

        if (index >= 0 && index < prendasSuperiores.Length)
        {
            ropaActualSuperior = prendasSuperiores[index];
            ropaActualSuperior.SetActive(true);
        }
        else
        {
            ropaActualSuperior = null;
        }
    }

    public void CambiarInferior(int index)
    {
        if (ropaActualInferior != null)
            ropaActualInferior.SetActive(false);

        if (index >= 0 && index < prendasInferiores.Length)
        {
            ropaActualInferior = prendasInferiores[index];
            ropaActualInferior.SetActive(true);
        }
        else
        {
            ropaActualInferior = null;
        }
    }

    public void CambiarCalzado(int index)
    {
        if (calzadoActual != null)
            calzadoActual.SetActive(false);

        if (index >= 0 && index < calzados.Length)
        {
            calzadoActual = calzados[index];
            calzadoActual.SetActive(true);
        }
        else
        {
            calzadoActual = null;
        }
    }
}
