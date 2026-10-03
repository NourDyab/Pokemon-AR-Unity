using UnityEngine;
using System.Collections;

public class PokemonCatch : MonoBehaviour
{
    private bool isCatching = false;

    [Range(0f, 1f)]
    public float catchChance = 0.5f;

    void OnCollisionEnter(Collision collision)
    {
        if (isCatching)
            return;

        if (!collision.gameObject.CompareTag("Pokeball"))
            return;

        StartCoroutine(CatchSequence());
    }

    IEnumerator CatchSequence()
    {
        isCatching = true;

        PokemonMovement movement =
            GetComponent<PokemonMovement>();

        if (movement != null)
            movement.enabled = false;

        Debug.Log("HIT! CATCHING POKEMON");

        Vector3 originalPosition = transform.position;

        // Shake
        float timer = 0f;

        while (timer < 0.4f)
        {
            transform.position =
                originalPosition +
                Random.insideUnitSphere * 0.05f;

            timer += Time.deltaTime;

            yield return null;
        }

        transform.position = originalPosition;

        yield return new WaitForSeconds(0.5f);

        // 50% Success / 50% Fail
        bool caught = Random.value < catchChance;

        Debug.Log("CATCH RESULT = " + caught);

        CatchUI ui =
            FindAnyObjectByType<CatchUI>();

        // SUCCESS
        if (caught)
        {
            Debug.Log("SUCCESS TRIGGERED");

            if (ui != null)
                ui.ShowSuccess();

            yield return new WaitForSeconds(0.5f);

            Destroy(gameObject);

            SpawnPokemon spawner =
                FindAnyObjectByType<SpawnPokemon>();

            if (spawner != null)
            {
                spawner.SpawnWithDelay();
            }
        }

        // FAIL
        else
        {
            Debug.Log("FAIL TRIGGERED");

            if (ui != null)
                ui.ShowFail();

            yield return new WaitForSeconds(0.3f);

            transform.position = originalPosition;

            if (movement != null)
                movement.enabled = true;

            isCatching = false;

            Debug.Log("POKEMON ESCAPED - STILL ALIVE");
        }
    }
}

