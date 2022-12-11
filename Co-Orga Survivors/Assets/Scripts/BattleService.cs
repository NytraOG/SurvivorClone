using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleService : MonoBehaviour
{
    private bool       spawnSexyGymGirl = true;
    public  GameObject playerPrefab;
    public  GameObject enemyNullreferenceExceptionPrefab;
    public  GameObject sexyGymGirlPrefab;
    
    // Start is called before the first frame update
    private void Start()
    {
        
        //Instantiate(playerPrefab);
        // sexyGirl.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
    }

    private void OnDied(object sender, EventArgs e)
    {
        ((BaseUnit)sender).Died -= OnDied;
        spawnSexyGymGirl        =  true;
    }

    // Update is called once per frame
    private void Update() 
    {
        if (spawnSexyGymGirl)
        {
            var randomX = Random.Range(-4.0f, 3.01f);
            var randomy = Random.Range(-2.0f, 2.01f);
            
            spawnSexyGymGirl = false;
            var sexyGirl = Instantiate(sexyGymGirlPrefab, new Vector3(randomX, randomy, -1), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            sexyGirl.name                          =  "SexyGymGirl";
            sexyGirl.GetComponent<BaseUnit>().Died += OnDied;
        }
    }
}