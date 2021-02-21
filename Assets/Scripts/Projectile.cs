using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float explosionForce;
    [SerializeField] private float explosionRasius;
    [SerializeField] private float destroyRadius;

    public float ExplosionRadius { get => explosionRasius; }
    public float ExplosionForce { get => explosionForce; }

    private void Start()
    {
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        GetComponent<Rigidbody>().AddForce(direction * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var destroySphere = Physics.OverlapSphere(transform.position, destroyRadius);

        foreach (var col in destroySphere)
            Destroy(col.gameObject);

        collision.gameObject.GetComponentInParent<Target>().Impact(this);
        TargetGenerator.Instance.CreateTarget();
    }
}
