using UnityEngine;

public class Catcher : MonoBehaviour
{
    public GameController gameController;
    public GameObject particleSystemPrefab;

    private const string SphereTag = "Sphere";
    private const string ButtonATag = "ButtonA";
    private const string HomeButtonTag = "HomeButton";

    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;

        switch (other.tag)
        {
            case SphereTag:
                HandleSphereCollision(other);
                break;
            case ButtonATag:
                gameController.ButtonAPressed();
                break;
            case HomeButtonTag:
                gameController.GoHome();
                break;
        }
    }

    private void HandleSphereCollision(Collider sphere)
    {
        // Получаем позицию шара перед уничтожением
        Vector3 position = sphere.transform.position;

        // Уничтожаем шар
        Destroy(sphere.gameObject);

        // Спавним Particle System в позиции шара
        Instantiate(particleSystemPrefab, position, Quaternion.identity);

        // Уведомляем GameController
        gameController.BallHit();
    }
}
