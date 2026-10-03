using UnityEngine;
using System.Collections;

public class SpawnPokemon : MonoBehaviour
{
    public GameObject pokemonPrefab;

    public Transform arCamera;

    public float spawnDistance = 3f;

    public float spawnDelay = 1.5f;

    void Start()
    {
        Debug.Log("SPAWN POKEMON STARTED");

        Spawn();
    }

    public void PokemonCaught()
    {
        Debug.Log("POKEMON CAUGHT -> NEW SPAWN");

        SpawnWithDelay();
    }

    public void SpawnWithDelay()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        Debug.Log("WAITING FOR NEW POKEMON...");

        yield return new WaitForSeconds(spawnDelay);

        Spawn();
    }

    public void Spawn()
    {
        if (pokemonPrefab == null)
        {
            Debug.LogError("Pokemon Prefab NOT ASSIGNED!");
            return;
        }

        if (arCamera == null)
        {
            Debug.LogError("AR Camera NOT ASSIGNED!");
            return;
        }

        Vector3 spawnPos =
            arCamera.position +
            arCamera.forward * spawnDistance;

        Instantiate(
            pokemonPrefab,
            spawnPos,
            Quaternion.identity
        );

        Debug.Log("NEW POKEMON SPAWNED");
    }
}
