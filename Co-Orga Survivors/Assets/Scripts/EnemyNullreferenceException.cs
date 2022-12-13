using UnityEngine;

public class EnemyNullreferenceException : BaseUnit
{
    private Player player;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.Find(nameof(Player))?.GetComponent<Player>();
    }

    // Update is called once per frame
    private void Update()
    {
        var body = GetComponent<Rigidbody2D>();
        var targetPosition  = player.transform.position;
        var thisPosition    = transform.position;
        var directionToMove = targetPosition - thisPosition;

        directionToMove = directionToMove.normalized * Time.deltaTime * moveSpeed;

        body.transform.position = thisPosition += directionToMove * moveSpeed * Time.deltaTime;
        //transform.position      = thisPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if (collision.gameObject.name.Contains("Sexy"))
        //     return;
        // // var enemy =  collision.gameObject. as BaseUnit;
        // var a = GetComponent<AudioSource>();
        // a.Play();
    }
}