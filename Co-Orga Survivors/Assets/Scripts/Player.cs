using ScriptableObjects;
using UnityEngine;

public class Player : MonoBehaviour
{
    public         BaseSurvivor survivor;
    public         float        moveSpeed;
    private static float        HorizontalInput => Input.GetAxis("Horizontal");

    // Start is called before the first frame update
    private void Start()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = survivor.sprite;
    }

    // Update is called once per frame
    private void Update()
    {
        var rigidBodyComponent = GetComponent<Rigidbody2D>();

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            rigidBodyComponent.transform.position = transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            rigidBodyComponent.transform.position = transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            rigidBodyComponent.transform.position = transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            rigidBodyComponent.transform.position = transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D col) => Debug.Log($"Collision with {col.gameObject.name}");
}