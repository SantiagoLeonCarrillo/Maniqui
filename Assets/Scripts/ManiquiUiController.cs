using UnityEngine;
using UnityEngine.UI;

public class ManiquiUIControl : MonoBehaviour
{
    public Animator animator;

    public Button botonIdle;
    public Button botonRun;
    public Button botonDance;

    void Start()
    {
        // Asignar listeners a los botones
        botonIdle.onClick.AddListener(() => CambiarAnimacion(0));
        botonRun.onClick.AddListener(() => CambiarAnimacion(1));
        botonDance.onClick.AddListener(() => CambiarAnimacion(2));

        // Estado inicial en Idle
        CambiarAnimacion(0);
    }

    public void CambiarAnimacion(int estado)
    {
        if (animator == null)
        {
            Debug.LogError("Animator no asignado en ManiquiUIControl.");
            return;
        }

        animator.SetInteger("state", estado);
        Debug.Log("Se cambió el parámetro 'state' del Animator a: " + estado);
    }

    void Update()
    {
        if (animator != null)
        {
            int currentState = animator.GetInteger("state");
            Debug.Log("Estado actual del Animator: " + currentState);
        }
    }
}
