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

    public Slider sliderR_Sup, sliderG_Sup, sliderB_Sup;
    public Slider sliderR_com, sliderG_com, sliderB_com;
    public Slider sliderR_Inf, sliderG_Inf, sliderB_Inf;
    public Slider sliderR_Calz, sliderG_Calz, sliderB_Calz;

    private GameObject prendaSuperiorActual;
    private GameObject prendaComplementariaActual;
    private GameObject prendaInferiorActual;
    private GameObject calzadoActual;

    private Material materialActual;
    private string categoriaActual;

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

    public void TogglePanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(!panel.activeSelf);
        }
    }

    public void MostrarPanelExclusivo(GameObject panelAMostrar)
    {
        OcultarTodosLosPanelesColor();
        if (panelAMostrar != null)
        {
            panelAMostrar.SetActive(true);
        }
    }

    public void OcultarPanelExclusivo(GameObject panelAMostrar)
    {
        if (panelAMostrar != null)
        {
            panelAMostrar.SetActive(false);
        }
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
                MostrarPanelCorrespondiente(categoria);
            });
        }
    }

    void MostrarPanelCorrespondiente(string categoria)
    {
        OcultarTodosLosPanelesColor();

        switch (categoria)
        {
            case "superior":
                panelColorSuperiores.SetActive(true);
                break;
            case "complementaria":
                panelColorComplementario.SetActive(true);
                break;
            case "inferior":
                panelColorInferiores.SetActive(true);
                break;
            case "calzado":
                panelColorCalzado.SetActive(true);
                break;
        }
    }

    void ConfigurarSliders()
    {
        ConfigurarSliderGrupo(sliderR_Sup, sliderG_Sup, sliderB_Sup);
        ConfigurarSliderGrupo(sliderR_com, sliderG_com, sliderB_com);
        ConfigurarSliderGrupo(sliderR_Inf, sliderG_Inf, sliderB_Inf);
        ConfigurarSliderGrupo(sliderR_Calz, sliderG_Calz, sliderB_Calz);
    }

    void ConfigurarSliderGrupo(Slider r, Slider g, Slider b)
    {
        r.onValueChanged.AddListener((v) => ActualizarColor());
        g.onValueChanged.AddListener((v) => ActualizarColor());
        b.onValueChanged.AddListener((v) => ActualizarColor());
    }

    public void SeleccionarPrenda(GameObject nuevaPrenda, string categoria)
    {
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

        nuevaPrenda.SetActive(true);
        categoriaActual = categoria;

        Renderer rend = nuevaPrenda.GetComponent<Renderer>();
        if (rend != null)
        {
            materialActual = new Material(rend.material);
            rend.material = materialActual;
            ActualizarValoresSliders();
        }
    }

    void ActualizarValoresSliders()
    {
        switch (categoriaActual)
        {
            case "superior":
                SetSliderValues(sliderR_Sup, sliderG_Sup, sliderB_Sup);
                break;
            case "complementaria":
                SetSliderValues(sliderR_com, sliderG_com, sliderB_com);
                break;
            case "inferior":
                SetSliderValues(sliderR_Inf, sliderG_Inf, sliderB_Inf);
                break;
            case "calzado":
                SetSliderValues(sliderR_Calz, sliderG_Calz, sliderB_Calz);
                break;
        }
    }

    void SetSliderValues(Slider r, Slider g, Slider b)
    {
        if (materialActual != null)
        {
            r.value = materialActual.color.r;
            g.value = materialActual.color.g;
            b.value = materialActual.color.b;
        }
    }

    void ActualizarColor()
    {
        if (materialActual == null) return;

        switch (categoriaActual)
        {
            case "superior":
                materialActual.color = new Color(sliderR_Sup.value, sliderG_Sup.value, sliderB_Sup.value);
                break;
            case "complementaria":
                materialActual.color = new Color(sliderR_com.value, sliderG_com.value, sliderB_com.value);
                break;
            case "inferior":
                materialActual.color = new Color(sliderR_Inf.value, sliderG_Inf.value, sliderB_Inf.value);
                break;
            case "calzado":
                materialActual.color = new Color(sliderR_Calz.value, sliderG_Calz.value, sliderB_Calz.value);
                break;
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