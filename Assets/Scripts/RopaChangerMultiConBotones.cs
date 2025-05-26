using UnityEngine;
using UnityEngine.UI;

public class RopaChangerMultiConBotones : MonoBehaviour
{
    [Header("Asignar Prendas")]
    public GameObject[] prendasSuperiores;
    public GameObject[] prendasComplementarias;
    public GameObject[] prendasInferiores;
    public GameObject[] calzados;

    [Header("Paneles de Botones")]
    public GameObject panelBotonesSuperiores;
    public GameObject panelBotonesComplementarias;
    public GameObject panelBotonesInferiores;
    public GameObject panelBotonesCalzado;

    [Header("Paneles de Color")]
    public GameObject panelColorSuperiores;
    public GameObject panelColorComplementario;
    public GameObject panelColorInferiores;
    public GameObject panelColorCalzado;

    [Header("Sliders de Color")]
    public Slider sliderR_Sup, sliderG_Sup, sliderB_Sup;
    public Slider sliderR_com, sliderG_com, sliderB_com;
    public Slider sliderR_Inf, sliderG_Inf, sliderB_Inf;
    public Slider sliderR_Calz, sliderG_Calz, sliderB_Calz;

    // Variables para rastrear las prendas actuales
    private GameObject prendaSuperiorActual;
    private GameObject prendaComplementariaActual;
    private GameObject prendaInferiorActual;
    private GameObject calzadoActual;

    // Materiales actuales por grupo
    private Material materialSuperiorActual;
    private Material materialComplementarioActual;
    private Material materialInferiorActual;
    private Material materialCalzadoActual;

    void Start()
    {
        InicializarSistema();
    }

    void InicializarSistema()
    {
        ApagarTodasLasPrendas();
        ConfigurarBotonesPrendas();
        ConfigurarSliders();
        OcultarTodosLosPanelesColor();
    }

    void ConfigurarBotonesPrendas()
    {
        ConfigurarBotonPrenda(panelBotonesSuperiores, prendasSuperiores, "superior");
        ConfigurarBotonPrenda(panelBotonesComplementarias, prendasComplementarias, "complementaria");
        ConfigurarBotonPrenda(panelBotonesInferiores, prendasInferiores, "inferior");
        ConfigurarBotonPrenda(panelBotonesCalzado, calzados, "calzado");
    }

    void ConfigurarBotonPrenda(GameObject panelBotones, GameObject[] prendas, string categoria)
    {
        if (panelBotones == null || prendas == null) return;

        Button[] botones = panelBotones.GetComponentsInChildren<Button>();
        for (int i = 0; i < botones.Length && i < prendas.Length; i++)
        {
            int index = i;
            botones[i].onClick.AddListener(() => {
                SeleccionarPrenda(prendas[index], categoria);
                MostrarPanelExclusivo(GetPanelCorrespondiente(categoria));
            });
        }
    }

    GameObject GetPanelCorrespondiente(string categoria)
    {
        switch (categoria)
        {
            case "superior": return panelColorSuperiores;
            case "complementaria": return panelColorComplementario;
            case "inferior": return panelColorInferiores;
            case "calzado": return panelColorCalzado;
            default: return null;
        }
    }

    public void MostrarPanelExclusivo(GameObject panelAMostrar)
    {
        // Ocultar todos los paneles primero
        OcultarTodosLosPanelesColor();

        // Mostrar solo el panel solicitado
        if (panelAMostrar != null)
        {
            panelAMostrar.SetActive(true);
        }
    }

    public void OcultarPanelExclusivo()
    {
        // Ocultar todos los paneles primero
        OcultarTodosLosPanelesColor();
    }

    void ConfigurarSliders()
    {
        ConfigurarSliderGrupo(sliderR_Sup, sliderG_Sup, sliderB_Sup, "superior");
        ConfigurarSliderGrupo(sliderR_com, sliderG_com, sliderB_com, "complementaria");
        ConfigurarSliderGrupo(sliderR_Inf, sliderG_Inf, sliderB_Inf, "inferior");
        ConfigurarSliderGrupo(sliderR_Calz, sliderG_Calz, sliderB_Calz, "calzado");
    }

    void ConfigurarSliderGrupo(Slider r, Slider g, Slider b, string categoria)
    {
        r.onValueChanged.AddListener((v) => ActualizarColor(categoria));
        g.onValueChanged.AddListener((v) => ActualizarColor(categoria));
        b.onValueChanged.AddListener((v) => ActualizarColor(categoria));
    }

    public void SeleccionarPrenda(GameObject nuevaPrenda, string categoria)
    {
        // Manejar la prenda según su categoría
        switch (categoria)
        {
            case "superior":
                if (prendaSuperiorActual != null)
                    prendaSuperiorActual.SetActive(false);
                prendaSuperiorActual = nuevaPrenda;
                break;

            case "complementaria":
                if (prendaComplementariaActual != null)
                    prendaComplementariaActual.SetActive(false);
                prendaComplementariaActual = nuevaPrenda;
                break;

            case "inferior":
                if (prendaInferiorActual != null)
                    prendaInferiorActual.SetActive(false);
                prendaInferiorActual = nuevaPrenda;
                break;

            case "calzado":
                if (calzadoActual != null)
                    calzadoActual.SetActive(false);
                calzadoActual = nuevaPrenda;
                break;
        }

        // Activar la nueva prenda
        nuevaPrenda.SetActive(true);

        // Configurar material para el control de color
        ConfigurarMaterial(nuevaPrenda, categoria);
    }

    void ConfigurarMaterial(GameObject prenda, string categoria)
    {
        Renderer rend = prenda.GetComponent<Renderer>();
        if (rend != null)
        {
            Material nuevoMaterial = new Material(rend.material);
            rend.material = nuevoMaterial;

            switch (categoria)
            {
                case "superior":
                    materialSuperiorActual = nuevoMaterial;
                    ActualizarValoresSliders(sliderR_Sup, sliderG_Sup, sliderB_Sup, nuevoMaterial);
                    break;

                case "complementaria":
                    materialComplementarioActual = nuevoMaterial;
                    ActualizarValoresSliders(sliderR_com, sliderG_com, sliderB_com, nuevoMaterial);
                    break;

                case "inferior":
                    materialInferiorActual = nuevoMaterial;
                    ActualizarValoresSliders(sliderR_Inf, sliderG_Inf, sliderB_Inf, nuevoMaterial);
                    break;

                case "calzado":
                    materialCalzadoActual = nuevoMaterial;
                    ActualizarValoresSliders(sliderR_Calz, sliderG_Calz, sliderB_Calz, nuevoMaterial);
                    break;
            }
        }
    }

    void ActualizarValoresSliders(Slider r, Slider g, Slider b, Material material)
    {
        r.value = material.color.r;
        g.value = material.color.g;
        b.value = material.color.b;
    }

    void ActualizarColor(string categoria)
    {
        Material materialActual = null;
        Slider r = null, g = null, b = null;

        // Obtener material y sliders correspondientes
        switch (categoria)
        {
            case "superior":
                materialActual = materialSuperiorActual;
                r = sliderR_Sup;
                g = sliderG_Sup;
                b = sliderB_Sup;
                break;

            case "complementaria":
                materialActual = materialComplementarioActual;
                r = sliderR_com;
                g = sliderG_com;
                b = sliderB_com;
                break;

            case "inferior":
                materialActual = materialInferiorActual;
                r = sliderR_Inf;
                g = sliderG_Inf;
                b = sliderB_Inf;
                break;

            case "calzado":
                materialActual = materialCalzadoActual;
                r = sliderR_Calz;
                g = sliderG_Calz;
                b = sliderB_Calz;
                break;
        }

        // Actualizar color si el material existe
        if (materialActual != null)
        {
            materialActual.color = new Color(r.value, g.value, b.value);
        }
    }

    void OcultarTodosLosPanelesColor()
    {
        panelColorSuperiores.SetActive(false);
        panelColorComplementario.SetActive(false);
        panelColorInferiores.SetActive(false);
        panelColorCalzado.SetActive(false);
    }

    void ApagarTodasLasPrendas()
    {
        ApagarGrupo(prendasSuperiores);
        ApagarGrupo(prendasComplementarias);
        ApagarGrupo(prendasInferiores);
        ApagarGrupo(calzados);
    }

    void ApagarGrupo(GameObject[] grupo)
    {
        if (grupo == null) return;

        foreach (var prenda in grupo)
        {
            if (prenda != null)
                prenda.SetActive(false);
        }
    }
}