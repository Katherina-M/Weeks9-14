using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InvisibilityItem : MonoBehaviour
{
    public GameObject orb;
    public float spawnInterval = 5f;

    private Camera mainCam;
    private Vector2 minBound;
    private Vector2 maxBound;

    void Start()
    {
        mainCam = Camera.main;

        //Get Camera boundry
        Vector3 bottomLeft = mainCam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = mainCam.ViewportToWorldPoint(new Vector3(1, 1, 0));
        minBound = new Vector2(bottomLeft.x, bottomLeft.y);
        maxBound = new Vector2(topRight.x, topRight.y);

        // start the Coroutine
        StartCoroutine(SpawnOrbRoutine());
    }

    IEnumerator SpawnOrbRoutine()
    {
        while (true)
        {
            //Wait for the interval
            yield return new WaitForSeconds(spawnInterval);

            //Generate random position
            float randomX = Random.Range(minBound.x, maxBound.x);
            float randomY = Random.Range(minBound.y, maxBound.y);
            Vector2 spawnPosition = new Vector2(randomX, randomY);

            //Random orb position
            Instantiate(orb, spawnPosition, Quaternion.identity);
        }   
    }
}
