using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    public bool PauseGame = true;
    private UIManager uiManager;
    private GameObject[] enemys;
    private GameObject[] powerUps;

    void Start()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Update()
    {
        if (PauseGame)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Return))
            {
                PauseGame = false;
                uiManager.Score = 0;
                uiManager.StartGame();
                Instantiate(player, Vector3.zero, Quaternion.identity);
            }
        }
    }

    public void FindAndDestroyObjects()
    {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemys)
        {
            enemy.GetComponent<EnemyIA>().EnemyDestroy();
        }

        powerUps = GameObject.FindGameObjectsWithTag("PowerUp");
        foreach (GameObject powerUp in powerUps)
        {
            powerUp.GetComponent<PowerUp>().PowerUpDestroy();
        }
    }
}
