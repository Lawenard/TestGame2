using UnityEngine;
using System.Collections;

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
            StartCoroutine(CreateTarget(0));
    }

    public void CreateTarget()
    {
        StartCoroutine(CreateTarget(1));
    }

    private IEnumerator CreateTarget(float delay)
    {
        bool isOccupied = true;
        Vector3 spawnPosition;

        while (isOccupied)
        {
            spawnPosition = Random.onUnitSphere * distance;
            if (!Physics.CheckSphere(spawnPosition, 16))
                isOccupied = false;
            yield return new WaitForSeconds(delay);
        }

        GameObject go = Instantiate(target, Random.onUnitSphere * distance, Quaternion.identity);
        go.transform.LookAt(transform);
        go.GetComponent<Target>().Generate(
            targetParts[Random.Range(0, targetParts.Length)],
            Random.value > .5);
    }
}
