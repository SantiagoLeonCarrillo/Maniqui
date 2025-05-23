using UnityEngine;
using UnityEngine.UI;

public class CamisetaEstampadoControl : MonoBehaviour
{
    [System.Serializable]
    public class EstampadoItem
    {
        public Button boton;
        public Texture2D estampado;
    }

    public EstampadoItem[] estampados;

    public Material camisetaMaterial;
    public GameObject panelEstampados;
    public GameObject panelSliders;

    public Slider offsetXSlider, offsetYSlider, scaleXSlider, scaleYSlider, opacitySlider, rotationSlider;

    public Button quitarEstampadoButton;  // Botón para quitar el estampado

    void Start()
    {
        // Asegurarse de que la camiseta inicie sin estampado
        QuitarEstampado();

        // Configuración de los botones de estampados
        foreach (var item in estampados)
        {
            item.boton.onClick.AddListener(() => AsignarEstampado(item.estampado));
        }

        // Configuración inicial de los valores de los sliders
        offsetXSlider.value = -0.67f;
        offsetYSlider.value = -0.37f;
        scaleXSlider.value = -4f;
        scaleYSlider.value = 4f;
        opacitySlider.value = 0f;
        rotationSlider.value = 0f;

        // Escuchar cambios de los sliders
        offsetXSlider.onValueChanged.AddListener(CambiarOffsetX);
        offsetYSlider.onValueChanged.AddListener(CambiarOffsetY);
        scaleXSlider.onValueChanged.AddListener(CambiarEscalaX);
        scaleYSlider.onValueChanged.AddListener(CambiarEscalaY);
        opacitySlider.onValueChanged.AddListener(CambiarOpacidad);
        rotationSlider.onValueChanged.AddListener(CambiarRotacion);

        // Asignar la función al botón de quitar estampado
        quitarEstampadoButton.onClick.AddListener(QuitarEstampado);
    }

    // Función para mostrar el panel de estampados
    public void MostrarPanelEstampados()
    {
        panelEstampados.SetActive(true);
        panelSliders.SetActive(false);
    }

    // Función para asignar un estampado y mostrar el panel de sliders
    public void AsignarEstampado(Texture2D estampado)
    {
        camisetaMaterial.SetTexture("_SecondaryTex", estampado);
        camisetaMaterial.SetFloat("_SecondaryTexOpacity", 1f);  // Asegurarse de que la opacidad esté al máximo cuando se asigna el estampado
        panelEstampados.SetActive(false);
        panelSliders.SetActive(true);
    }

    // Función para aceptar los valores y cerrar el panel de sliders
    public void AceptarSliders()
    {
        panelSliders.SetActive(false);
    }

    // Función para manejar la visualización del panel extra
    public void BotonExtraPresionado()
    {
        MostrarPanelEstampados();
    }

    // Función para quitar el estampado
    public void QuitarEstampado()
    {
        camisetaMaterial.SetTexture("_SecondaryTex", null);  // Eliminar la textura secundaria
        camisetaMaterial.SetFloat("_SecondaryTexOpacity", 0f);  // Poner la opacidad a 0 para que sea invisible
        panelEstampados.SetActive(false);  // Cerrar el panel de estampados
        panelSliders.SetActive(false);  // Cerrar el panel de sliders
    }

    // Funciones para cambiar las propiedades de los estampados
    public void CambiarOffsetX(float value)
    {
        Vector4 offset = camisetaMaterial.GetVector("_SecondaryTexOffset");
        offset.x = value;
        camisetaMaterial.SetVector("_SecondaryTexOffset", offset);
    }

    public void CambiarOffsetY(float value)
    {
        Vector4 offset = camisetaMaterial.GetVector("_SecondaryTexOffset");
        offset.y = value;
        camisetaMaterial.SetVector("_SecondaryTexOffset", offset);
    }

    public void CambiarEscalaX(float value)
    {
        Vector4 scale = camisetaMaterial.GetVector("_SecondaryTexScale");
        scale.x = value;
        camisetaMaterial.SetVector("_SecondaryTexScale", scale);
    }

    public void CambiarEscalaY(float value)
    {
        Vector4 scale = camisetaMaterial.GetVector("_SecondaryTexScale");
        scale.y = value;
        camisetaMaterial.SetVector("_SecondaryTexScale", scale);
    }

    public void CambiarOpacidad(float value)
    {
        camisetaMaterial.SetFloat("_SecondaryTexOpacity", value);
    }

    public void CambiarRotacion(float value)
    {
        camisetaMaterial.SetFloat("_SecondaryTexRotation", value);
    }
}
