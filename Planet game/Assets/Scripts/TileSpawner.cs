using System.Collections;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    // ������ ��� �������� ������
    public GameObject tilePrefab;
    // ����� ������ ������
    public Transform spawnPoint;
    // ������������ �������� � ��������
    public float[] originalTimeCodes;
    // �������������� �������� � ��������
    public float[] timeCodes;
    public AudioSource audioSource; // ����� ��������

    private bool timerActive = false; // ������ �������

    private void Start()
    {
        // ��������� ������������ �������� ��� ����������� �������������
        originalTimeCodes = (float[])timeCodes.Clone();
    }

    public void ButtonATouched()
    {
        // ��������� ��������� � ���������� ������
        audioSource.Play();
        timerActive = true;
    }

    void Update()
    {
        if (timerActive)
        {
            CheckAndSpawnTiles();
        }
    }

    private void CheckAndSpawnTiles()
    {
        for (int i = 0; i < timeCodes.Length; i++)
        {
            // ���������, ������ �� ����� � ���������� �� ������� ��� ������
            if (audioSource.isPlaying && audioSource.time >= timeCodes[i] - 2f)
            {
                SpawnTile();
                timeCodes[i] = float.MaxValue; // ������ ������� ����� ������ ������
            }
        }
    }

    private void SpawnTile()
    {
        // ������� ������ � ��������� �� ��������
        GameObject tile = Instantiate(tilePrefab, spawnPoint.position, Quaternion.identity);
        Vector3 direction = spawnPoint.forward;
        StartCoroutine(MoveTile(tile, direction));
    }

    private IEnumerator MoveTile(GameObject tile, Vector3 direction)
    {
        float duration = 7f; // ����� ��������
        float elapsedTime = 0f;

        Vector3 startPosition = tile.transform.position;
        Vector3 targetPosition = startPosition + direction * 280f;

        while (elapsedTime < duration)
        {
            if (tile != null)
            {
                tile.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            }
            else
            {
                yield break; // ��������� ��������, ���� ������ ���� ����������
            }

            elapsedTime += Time.deltaTime;
            yield return null; // ���� ���������� �����
        }

        // ������������� ������������� ������� � ���������� ������
        tile.transform.position = targetPosition;
        Destroy(tile);
    }

    public void ArrayReturner()
    {
        // ���������� ���������������� �������� � ������������ ���������
        timeCodes = (float[])originalTimeCodes.Clone();
    }
}
