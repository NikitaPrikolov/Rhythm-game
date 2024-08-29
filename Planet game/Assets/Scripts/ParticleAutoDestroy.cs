using System.Collections;
using UnityEngine;

public class ParticleAutoDestroy : MonoBehaviour
{

    // Время, через которое будет удален объект (в секундах)
    [SerializeField] private float lifetime = 0.5f;

    private void Awake()
    {
        StartDestructionCoroutine();
    }

    private void StartDestructionCoroutine()
    {
        StartCoroutine(DestroyAfterLifetime(lifetime));
    }

    private IEnumerator DestroyAfterLifetime(float time)
    {
        yield return new WaitForSeconds(time); // Ждем заданное время
        Destroy(gameObject); // Удаляем объект
    }
}
