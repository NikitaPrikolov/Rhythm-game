using UnityEngine;

public class MissedBase : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, столкнулись ли с шаром
        if (IsSphere(other))
        {
            DestroySphere(other);
        }
    }

    private bool IsSphere(Collider other)
    {
        return other.CompareTag("Sphere");
    }

    private void DestroySphere(Collider sphere)
    {
        Destroy(sphere.gameObject);
    }
}

