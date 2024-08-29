using System.Collections;
using UnityEngine;

public class ParticleAutoDestroy : MonoBehaviour
{

    // �����, ����� ������� ����� ������ ������ (� ��������)
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
        yield return new WaitForSeconds(time); // ���� �������� �����
        Destroy(gameObject); // ������� ������
    }
}
