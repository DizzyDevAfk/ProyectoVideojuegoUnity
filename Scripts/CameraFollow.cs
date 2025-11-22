using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset;

    [Header("Restricciones del Nivel")]
    [SerializeField] private float minX = 0f; // Posición X mínima
    [SerializeField] private float maxX = 80f; 

    void LateUpdate()
    {
       
        Vector3 desiredPosition = target.position + offset;
        desiredPosition.y = transform.position.y;
        desiredPosition.z = transform.position.z;
        
        //Limitar la Posición X 
        desiredPosition.x = Mathf.Max(desiredPosition.x, minX);

       
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
       
        transform.position = smoothedPosition;
    }
}