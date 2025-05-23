using UnityEngine;
using TMPro;

public class MannequinSizeSelector : MonoBehaviour
{
    public Transform maniqui;
    public TMP_Dropdown sizeDropdown;

    void Start()
    {
        // Opciones del dropdown (en caso de no tenerlas desde el Inspector)
        if (sizeDropdown.options.Count == 0)
        {
            sizeDropdown.options.Add(new TMP_Dropdown.OptionData("S"));
            sizeDropdown.options.Add(new TMP_Dropdown.OptionData("M"));
            sizeDropdown.options.Add(new TMP_Dropdown.OptionData("L"));
            sizeDropdown.options.Add(new TMP_Dropdown.OptionData("XL"));
        }

        // Asignar listener
        sizeDropdown.onValueChanged.AddListener(OnSizeChanged);

        // Talla inicial: S
        sizeDropdown.value = 0;
        SetScale(0);
    }

    void OnSizeChanged(int index)
    {
        SetScale(index);
    }

    void SetScale(int index)
    {
        Vector3 newScale = Vector3.one;

        switch (index)
        {
            case 0:
                newScale = new Vector3(0.85f, 0.88f, 0.7f);
                break;
            case 1:
                newScale = new Vector3(0.95f, 0.92f, 0.85f);
                break;
            case 2:
                newScale = new Vector3(1.05f, 0.96f, 1f);
                break;
            case 3:
                newScale = new Vector3(1.15f, 1f, 1.15f);
                break;
        }

        maniqui.localScale = newScale;
        Debug.Log($"Talla seleccionada: {sizeDropdown.options[index].text} | Nueva escala: {newScale}");
    }
}
