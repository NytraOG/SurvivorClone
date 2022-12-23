using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleService : MonoBehaviour
{
    public  GameObject enemyNullreferenceExceptionPrefab;
    public  float      timeToSpawnFirstWave;
    public  float      timeToSpawnConsecutiveWaves;
    public  int        amountOfEnemiesToSpawn;
    public  int        spawnSafetyRadius;
    public  float      spawnDistanceBetweenEnemiesOfGroup;
    private bool       firstWaveWasSpawned;
    private float      middleX;
    private float      middleY;
    private Player     player;
    private float      ticks;
    private float      ElapsedSeconds => ticks % 60;

    private void Start()
    {
        player = GameObject.Find(nameof(Player))?.GetComponent<Player>();
        var playerposition = player.transform.position;

        middleX = Math.Abs(playerposition.x);
        middleY = Math.Abs(playerposition.y);
    }

    private void Update()
    {
        if (!firstWaveWasSpawned && ElapsedSeconds >= timeToSpawnFirstWave)
            SpawnEnemies();
        else if (ElapsedSeconds >= timeToSpawnConsecutiveWaves)
        {
            SpawnEnemies();
            ticks = 0;
        }
    }

    private void FixedUpdate() => ticks += Time.deltaTime; 

    private void SpawnEnemies()
    {
        var distanceModifier = Random.Range(1f, 2f);
        var firstSpawnpoint  = new Vector3(middleX * 1 * GetVorzeichen(), middleY * 1 * GetVorzeichen(), 0);

        Instantiate(enemyNullreferenceExceptionPrefab, firstSpawnpoint, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);

        for (var i = 0; i < amountOfEnemiesToSpawn - 1; i++) 
        {
            //Einmal bestimmen, ob der gegner links, recht oder gemischt vom firstSpawn spawnt
            var enemyPosition = GetEnemyPosition(Random.Range(1, 4), firstSpawnpoint);

            Instantiate(enemyNullreferenceExceptionPrefab, enemyPosition, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
        }

        firstWaveWasSpawned = true;
        ticks               = 0;
    }

    private static int GetVorzeichen() => Random.Range(0, 2) == 0 ? 1 : 1;

    private static float GetDistanceModifiert() => Random.Range(0.5f, 1.01f);

    private Vector3 GetEnemyPosition(int spawnIndex, Vector3 originalPosition) => spawnIndex switch
    {
        1 => originalPosition + new Vector3(spawnDistanceBetweenEnemiesOfGroup * GetDistanceModifiert() * GetVorzeichen(), 0),
        2 => originalPosition + new Vector3(0, spawnDistanceBetweenEnemiesOfGroup * GetDistanceModifiert() * GetVorzeichen()),
        3 => originalPosition + new Vector3(spawnDistanceBetweenEnemiesOfGroup * Random.Range(0.1f, 1f) * GetDistanceModifiert() * GetVorzeichen(), spawnDistanceBetweenEnemiesOfGroup * Random.Range(0.1f, 1f) * GetDistanceModifiert() * GetVorzeichen()),
        _ => throw new ArgumentException(nameof(spawnIndex))
    };
}