using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpanwManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyShipPrefab;
    [SerializeField]
    private GameObject[] powerUps;
    [SerializeField]
    private int secondsCreateEnemy = 2;
    [SerializeField]
    private int secondsCreatePowerUp = 5;
    [SerializeField]
    private int NumberMaxPowerUps = 2;
    public int NumberPowerUpsInGame = 0;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void FixUpdate()
    {
        GameObject[] powerUpsInGame = GameObject.FindGameObjectsWithTag("PowerUp");
        NumberPowerUpsInGame = powerUpsInGame.Length;
    }

    public void StartSpawnRoutines()
    {
        StartCoroutine(GenerateEnemyRoutine());
        StartCoroutine(GeneratePowerUpRoutine());
    }

    IEnumerator GenerateEnemyRoutine()
    {
        while (!gameManager.PauseGame)
        {
            CreateEnemy();
            yield return new WaitForSeconds(secondsCreateEnemy);
        }
    }

    private void CreateEnemy()
    {
        Instantiate(enemyShipPrefab, new Vector3(Random.Range(-8.75f, 8.75f), 7, 0), Quaternion.identity);
    }

    IEnumerator GeneratePowerUpRoutine()
    {
        while (!gameManager.PauseGame)
        {
            int randomPowerUp = Random.Range(0, 3);
            CreatePowerUp(randomPowerUp);
            NumberPowerUpsInGame = 0;
            yield return new WaitForSeconds(secondsCreatePowerUp);
        }
    }

    private void CreatePowerUp(int index)
    {
        if (NumberPowerUpsInGame < NumberMaxPowerUps)
        {
            Instantiate(powerUps[index], new Vector3(Random.Range(-8.75f, 8.75f), 7, 0), Quaternion.identity);
            NumberPowerUpsInGame++;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
    }

}
