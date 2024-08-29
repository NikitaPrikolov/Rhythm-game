using System.Collections;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    // Префаб для создания плитки
    public GameObject tilePrefab;
    // Точка спауна плиток
    public Transform spawnPoint;
    // Оригинальные таймкоды в секундах
    public float[] originalTimeCodes;
    // Модифицируемые таймкоды в секундах
    public float[] timeCodes;
    public AudioSource audioSource; // Аудио источник

    private bool timerActive = false; // Статус таймера

    private void Start()
    {
        // Клонируем оригинальные таймкоды для дальнейшего использования
        originalTimeCodes = (float[])timeCodes.Clone();
    }

    public void ButtonATouched()
    {
        // Запускаем аудиотрек и активируем таймер
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
            // Проверяем, играет ли аудио и достаточно ли времени для спауна
            if (audioSource.isPlaying && audioSource.time >= timeCodes[i] - 2f)
            {
                SpawnTile();
                timeCodes[i] = float.MaxValue; // Убрать таймкод после спауна плитки
            }
        }
    }

    private void SpawnTile()
    {
        // Создаем плитку и запускаем ее движение
        GameObject tile = Instantiate(tilePrefab, spawnPoint.position, Quaternion.identity);
        Vector3 direction = spawnPoint.forward;
        StartCoroutine(MoveTile(tile, direction));
    }

    private IEnumerator MoveTile(GameObject tile, Vector3 direction)
    {
        float duration = 7f; // Время движения
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
                yield break; // Прерываем корутину, если плитка была уничтожена
            }

            elapsedTime += Time.deltaTime;
            yield return null; // Ждем следующего кадра
        }

        // Устанавливаем окончательную позицию и уничтожаем плитку
        tile.transform.position = targetPosition;
        Destroy(tile);
    }

    public void ArrayReturner()
    {
        // Возвращаем модифицированные таймкоды к оригинальным значениям
        timeCodes = (float[])originalTimeCodes.Clone();
    }
}
