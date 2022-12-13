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
    private float      Elapsed => ticks % 60;

    private void Start()
    {
        player = GameObject.Find(nameof(Player))?.GetComponent<Player>();
        var playerposition = player.transform.position;

        middleX = Math.Abs(playerposition.x);
        middleY = Math.Abs(playerposition.y);
    }

    private void Update()
    {
        if (!firstWaveWasSpawned && Elapsed >= timeToSpawnFirstWave)
        {
            SpawnEnemies();
        }
        else if (Elapsed >= timeToSpawnConsecutiveWaves)
        {
            SpawnEnemies();
            ticks = 0;
        }
    }

    private void SpawnEnemies()
    {
        var distanceModifier = Random.Range(1f, 3f);
        var firstSpawnpoint  = new Vector3(middleX * distanceModifier * GetVorzeichen(), middleY * distanceModifier * GetVorzeichen(), -1);

        Instantiate(enemyNullreferenceExceptionPrefab, firstSpawnpoint, Quaternion.identity);

        for (var i = 0; i < amountOfEnemiesToSpawn - 1; i++)
        {
            //Einmal bestimmen, ob der gegner links, recht oder gemischt vom firstSpawn spawnt
            var enemyPosition = GetEnemyPosition(Random.Range(1, 3), Random.Range(0.5f, 1.01f), firstSpawnpoint, GetVorzeichen());
            Instantiate(enemyNullreferenceExceptionPrefab, enemyPosition, Quaternion.identity);
        }

        firstWaveWasSpawned = true;
        ticks               = 0;
    }

    private void FixedUpdate() => ticks += Time.deltaTime;

    private static int GetVorzeichen()
    {
        var vorzeichen = Random.Range(0, 2) == 0 ? 1 : -1;
        return vorzeichen;
    }

    private Vector3 GetEnemyPosition(int spawnIndex, float distanceModifier, Vector3 originalPosition, int vorzeichen) => spawnIndex switch
    {
        1 => originalPosition + new Vector3(spawnDistanceBetweenEnemiesOfGroup * distanceModifier * vorzeichen, 0),
        2 => originalPosition + new Vector3(0, spawnDistanceBetweenEnemiesOfGroup * distanceModifier * vorzeichen),
        3 => originalPosition + new Vector3(spawnDistanceBetweenEnemiesOfGroup * 0.7f * distanceModifier * vorzeichen, spawnDistanceBetweenEnemiesOfGroup * 0.7f * distanceModifier * vorzeichen),
        _ => throw new ArgumentException(nameof(spawnIndex))
    };
}