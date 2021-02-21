using UnityEngine;

public class TargetGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] targetParts;
    [SerializeField] private float distance;
    [SerializeField] private int startingTargets;
    [SerializeField] GameObject target;

    public static TargetGenerator Instance { get; private set; }

    void Start()
    {
        if (!Instance)
            Instance = this;

        for (int i = 0; i < startingTargets; i++)
            CreateTarget();
    }

    public void CreateTarget()
    {
        GameObject go = Instantiate(target, Random.onUnitSphere * distance, Quaternion.identity);
        go.transform.LookAt(transform);
        go.GetComponent<Target>().Generate(
            targetParts[Random.Range(0, targetParts.Length)],
            Random.value > .5);
    }
}
