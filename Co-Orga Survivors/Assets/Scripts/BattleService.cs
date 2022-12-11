using UnityEngine;

public class BattleService : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyNullreferenceExceptionPrefab;
    public GameObject sexyGymGirlPrefab;
    
    // Start is called before the first frame update
    private void Start()
    {
        //Instantiate(playerPrefab);
        var sexyGirl =Instantiate(sexyGymGirlPrefab, new Vector3(0,0,-1), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
        sexyGirl.name = "SexyGymGirl";
        // sexyGirl.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
    }

    // Update is called once per frame
    private void Update() { }
}