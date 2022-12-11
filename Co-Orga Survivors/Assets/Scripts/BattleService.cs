using UnityEngine;

public class BattleService : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyNullreferenceExceptionPrefab;

    // Start is called before the first frame update
    private void Start()
    {
        //Instantiate(playerPrefab);
        //Instantiate(enemyNullreferenceExceptionPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    private void Update() { }
}