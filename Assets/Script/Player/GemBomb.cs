using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBomb : MonoBehaviour
{
    public GameObject explosionPrefab; // The prefab of the object to spawn upon explosion
    public float bombDelay;

    private void Start()
    {
        StartCoroutine(ExplodeAfterDelay(bombDelay));
    }

    private IEnumerator ExplodeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Spawn the explosionPrefab at the current position and rotation
        Instantiate(explosionPrefab, transform.position, transform.rotation);

        // Destroy the GemBomb object
        Destroy(gameObject);
    }
}
