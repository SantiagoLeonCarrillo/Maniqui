using UnityEngine;
using UnityEngine.EventSystems;

public class CameraOrbit : MonoBehaviour
{
    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = 0.5f;
    public float distanceMax = 15f;

    public float panSpeed = 0.0005f; // Velocidad de desplazamiento

    private float x = 0.0f;
    private float y = 0.0f;

    private Vector3 panOffset = Vector3.zero;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        if (target == null)
        {
            GameObject mannequin = GameObject.FindGameObjectWithTag("Mannequin");
            if (mannequin != null)
            {
                target = mannequin.transform;
            }
        }
    }

    void LateUpdate()
    {
        if (target)
        {
            // Rotación con clic izquierdo
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                y = ClampAngle(y, yMinLimit, yMaxLimit);
            }

            // Zoom con la rueda
            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            // Pan con clic central (scroll click)
            if (Input.GetMouseButton(2) && !EventSystem.current.IsPointerOverGameObject())
            {
                float panX = -Input.GetAxis("Mouse X") * panSpeed * distance * 100f;
                float panY = -Input.GetAxis("Mouse Y") * panSpeed * distance * 100f;

                // Mover el target en el plano horizontal y vertical de la cámara
                Vector3 right = transform.right;
                Vector3 up = transform.up;

                panOffset += (right * panX) + (up * panY);
            }

            // Calcular posición y rotación de la cámara
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position + panOffset;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
