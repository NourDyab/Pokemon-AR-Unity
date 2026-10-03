using UnityEngine;
using System.Collections;

public class Pokeball : MonoBehaviour
{
    private bool alreadyHit = false;

    private CatchUI catchUI;
    private SpawnPokemon spawnPokemon;

    void Start()
    {
        catchUI = FindAnyObjectByType<CatchUI>();
        spawnPokemon = FindAnyObjectByType<SpawnPokemon>();

        Debug.Log("🟡 Pokeball Ready");

        if (catchUI == null)
            Debug.LogError("❌ CatchUI NOT FOUND");
        else
            Debug.Log("✔ CatchUI FOUND");

        if (spawnPokemon == null)
            Debug.LogError("❌ SpawnPokemon NOT FOUND");
        else
            Debug.Log("✔ SpawnPokemon FOUND");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (alreadyHit)
            return;

        if (!collision.gameObject.CompareTag("Pokemon"))
        {
            Debug.Log("⚠ Hit non-pokemon: " + collision.gameObject.name);
            return;
        }

        alreadyHit = true;

        Debug.Log("💥 HIT POKEMON: " + collision.gameObject.name);

        // Ball entfernen
        Destroy(gameObject);

        StartCoroutine(CatchSequence(collision.gameObject));
    }

    IEnumerator CatchSequence(GameObject pokemon)
    {
        Debug.Log("🧪 CatchSequence START");

        if (pokemon == null)
        {
            Debug.LogError("❌ Pokemon NULL");
            yield break;
        }

        // Bewegung stoppen
        PokemonMovement move =
            pokemon.GetComponent<PokemonMovement>();

        if (move != null)
            move.enabled = false;

        Vector3 originalPos = pokemon.transform.position;

        // -------------------------
        // SHAKE
        // -------------------------

        Debug.Log("📳 SHAKE START");

        yield return Shake(
            pokemon,
            originalPos,
            0.25f,
            0.08f
        );

        yield return new WaitForSeconds(0.3f);

        // -------------------------
        // CATCH RESULT
        // -------------------------

        bool caught = Random.value > 0.3f;

        Debug.Log("🎲 CATCH RESULT = " + caught);

        // -------------------------
        // SUCCESS
        // -------------------------

        if (caught)
        {
            Debug.Log("🟢 SUCCESS TRIGGERED");

            if (catchUI != null)
                catchUI.ShowSuccess();

            yield return new WaitForSeconds(0.8f);

            // Pokémon verschwindet NUR bei SUCCESS
            pokemon.SetActive(false);

            Debug.Log("👾 POKEMON CAUGHT");

            // Neues Pokémon
            if (spawnPokemon != null)
            {
                Debug.Log("🔄 REQUEST NEW POKEMON");

                spawnPokemon.PokemonCaught();
            }
            else
            {
                Debug.LogError("❌ SpawnPokemon NOT FOUND");
            }
        }

        // -------------------------
        // FAIL
        // -------------------------

        else
        {
            Debug.Log("🔴 FAIL TRIGGERED");

            if (catchUI != null)
                catchUI.ShowFail();

            yield return new WaitForSeconds(0.3f);

            // Position zurücksetzen
            pokemon.transform.position = originalPos;

            // Pokémon bleibt sichtbar
            pokemon.SetActive(true);

            // Bewegung wieder aktivieren
            if (move != null)
                move.enabled = true;

            Debug.Log("👾 POKEMON ESCAPED - STILL ACTIVE");
        }
    }

    IEnumerator Shake(
        GameObject obj,
        Vector3 origin,
        float duration,
        float strength)
    {
        float t = 0f;

        while (t < duration)
        {
            obj.transform.position =
                origin + Random.insideUnitSphere * strength;

            t += Time.deltaTime;

            yield return null;
        }

        obj.transform.position = origin;
    }
}