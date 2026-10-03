using UnityEngine;


public class PokemonMovement : MonoBehaviour
{
    public float speed = 0.5f;
    public float range = 0.3f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float x = Mathf.Sin(Time.time * speed) * range;
        float z = Mathf.Cos(Time.time * speed) * range;

        transform.position = startPos + new Vector3(x, 0, z);
    }
}
/*public class PokemonMovement : MonoBehaviour
{
    public float speed = 0.5f;
    public float moveRange = 0.5f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float moveX = Mathf.Sin(Time.time * speed) * moveRange;
        float moveZ = Mathf.Cos(Time.time * speed) * moveRange;

        transform.position = startPos + new Vector3(moveX, 0, moveZ);
    }
}*/
