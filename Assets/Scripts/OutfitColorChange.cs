using UnityEngine;
using UnityEngine.UI;

public class ColorSelector : MonoBehaviour
{
    [System.Serializable]
    public class OutfitPart
    {
        public string name;
        public Button colorButton;
        public Button extraButton;
        public GameObject slidersPanel;
        public Slider sliderR;
        public Slider sliderG;
        public Slider sliderB;
        public Button aceptarButton;
        public Image colorPreview;
        public Material material;
    }

    public OutfitPart[] outfitParts;

    void Start()
    {
        foreach (var part in outfitParts)
        {
            // Botón de color abre sliders
            part.colorButton.onClick.AddListener(() => OpenSliders(part));

            // Botón aceptar cierra sliders
            part.aceptarButton.onClick.AddListener(() => CloseSliders(part));

            // Listeners de sliders para actualizar color en tiempo real
            part.sliderR.onValueChanged.AddListener((value) => UpdateColor(part));
            part.sliderG.onValueChanged.AddListener((value) => UpdateColor(part));
            part.sliderB.onValueChanged.AddListener((value) => UpdateColor(part));

            // Ocultar panel de sliders al inicio
            part.slidersPanel.SetActive(false);
        }
    }

    void OpenSliders(OutfitPart part)
    {
        part.slidersPanel.SetActive(true);
        part.extraButton.interactable = false;

        // Cargar color actual en sliders desde _Color del shader
        Color currentColor = part.material.GetColor("_Color");
        part.sliderR.value = currentColor.r;
        part.sliderG.value = currentColor.g;
        part.sliderB.value = currentColor.b;

        part.colorPreview.color = currentColor;
    }

    void CloseSliders(OutfitPart part)
    {
        part.slidersPanel.SetActive(false);
        part.extraButton.interactable = true;
    }

    void UpdateColor(OutfitPart part)
    {
        Color newColor = new Color(
            part.sliderR.value,
            part.sliderG.value,
            part.sliderB.value
        );

        // Actualizar color en el material usando _Color del shader personalizado
        part.material.SetColor("_Color", newColor);

        // Actualizar preview de color
        part.colorPreview.color = newColor;
    }
}
