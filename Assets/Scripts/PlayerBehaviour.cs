using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject[] projectiles;

    public void LaunchProjectile()
    {
        Instantiate(
            projectiles[Random.Range(0, projectiles.Length)],
            transform.position,
            transform.rotation);
    }
}
