using UnityEngine;

public class EnemyNullreferenceException : BaseUnit
{
    private Player player;

    // Start is called before the first frame update
    private void Start()
    {
        player            = GameObject.Find(nameof(Player))?.GetComponent<Player>();
        expGrantedOnDeath = 2;
    }

    // Update is called once per frame
    private void Update() { }
}