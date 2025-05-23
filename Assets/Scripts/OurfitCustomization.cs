using UnityEngine;
using UnityEngine.UI;

public class OutfitCustomizationManager : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject mainPanel; // Panel padre que contiene los 3 botones iniciales
    public GameObject camisetaMenu;
    public GameObject pantalonMenu;
    public GameObject zapatosMenu;

    [Header("Botones de Prendas")]
    public Button camisetaButton;
    public Button pantalonButton;
    public Button zapatosButton;

    [Header("Botón de Regreso")]
    public Button backButton;

    void Start()
    {
        // Configurar listeners de los botones
        camisetaButton.onClick.AddListener(() => ShowCustomizationMenu(camisetaMenu));
        pantalonButton.onClick.AddListener(() => ShowCustomizationMenu(pantalonMenu));
        zapatosButton.onClick.AddListener(() => ShowCustomizationMenu(zapatosMenu));

        // Configurar botón de regreso
        backButton.onClick.AddListener(ShowMainMenu);

        // Mostrar solo el menú principal al inicio
        ShowMainMenu();
    }

    void ShowCustomizationMenu(GameObject menuToShow)
    {
        // 1. Desactivar el panel principal COMPLETAMENTE
        mainPanel.SetActive(false);

        // 2. Ocultar todos los menús de personalización (por si acaso)
        camisetaMenu.SetActive(false);
        pantalonMenu.SetActive(false);
        zapatosMenu.SetActive(false);

        // 3. Activar solo el menú seleccionado
        menuToShow.SetActive(true);

        // 4. Mostrar el botón de regreso
        backButton.gameObject.SetActive(true);
    }

    void ShowMainMenu()
    {
        // 1. Reactivar el panel principal
        mainPanel.SetActive(true);

        // 2. Ocultar todos los menús de personalización
        camisetaMenu.SetActive(false);
        pantalonMenu.SetActive(false);
        zapatosMenu.SetActive(false);

        // 3. Ocultar el botón de regreso
        backButton.gameObject.SetActive(false);
    }
}