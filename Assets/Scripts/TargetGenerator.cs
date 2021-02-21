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
        Vector3 spawnPosition = Vector3.zero;

        while (isOccupied)
        {
            spawnPosition = Random.onUnitSphere * distance;
            isOccupied = Physics.CheckSphere(spawnPosition, 15);
            yield return new WaitForSeconds(delay);
            print("Doing check for empty location");
        }

        GameObject go = Instantiate(target, spawnPosition, Quaternion.identity);
        go.transform.LookAt(transform);
        go.GetComponent<Target>().Generate(
            targetParts[Random.Range(0, targetParts.Length)],
            Random.value > .5);
    }
}
