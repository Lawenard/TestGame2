using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Vector3Int targetSize;
    [SerializeField] private float lifetimeMin, lifetimeMax;
    private bool alive = true;

    public void Generate(GameObject targetPart, bool isPyramid)
    {
        Vector3 v = targetSize / 2;

        for (int i = 0; i < 3; i++)
            if (v[i] < 0)
                return;

        if (isPyramid)
            v *= 1.5f;
        for (float y = -v.y; y <= v.y; y++)
        {
            for (float x = -v.x; x <= v.x; x++)
            {
                for (float z = -v.z; z <= v.z; z++)
                {
                    Instantiate(targetPart, transform).
                        transform.localPosition = new Vector3(x, y, z);
                }
            }
            if (isPyramid)
            {
                v -= new Vector3(.5f, 0, .5f);
                if (v.x < 1 || v.z < 1)
                    return;
            }
        }
    }

    public void Impact(Projectile proj)
    {
        if (!alive)
            return;

        alive = false;
        foreach (var rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
            rb.AddExplosionForce(
                proj.ExplosionForce,
                proj.transform.position,
                proj.ExplosionRadius);
            Destroy(rb.gameObject, Random.Range(lifetimeMin, lifetimeMax));
        }
        TargetGenerator.Instance.CreateTarget();
    }
}
