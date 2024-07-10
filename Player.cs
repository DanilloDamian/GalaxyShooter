using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public int Life = 3;
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private GameObject shieldGameObject;
    [SerializeField]
    private float PowerUpSpeed = 2.5f;
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float fireRate = 0.25f;
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject tripleLaserPrefab;
    [SerializeField]
    private GameObject[] engines;

    public bool CanTripleShot = false;
    public bool IsSpeedBoostActive = false;
    public bool IsShieldActive = false;
    public float CanFire = 0f;
    public UIManager UiManager;

    private GameManager gameManager;
    private SpanwManager spanwManager;
    private AudioSource audioSource;
    private int hitCount = 0;

    void Start()
    {
        shieldGameObject = this.transform.GetChild(0).gameObject;
        UiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spanwManager = GameObject.Find("Spawn_Manager").GetComponent<SpanwManager>();
        audioSource = GetComponent<AudioSource>();
        if (UiManager)
        {
            UiManager.UpdateLives(Life);
        }
        if (spanwManager)
        {
            spanwManager.StartSpawnRoutines();
        }
        hitCount = 0;
    }

    void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time > CanFire)
        {
            audioSource.Play();
            if (CanTripleShot)
            {
                Instantiate(tripleLaserPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            }
            CanFire = Time.time + fireRate;
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (IsSpeedBoostActive)
        {
            transform.Translate(new Vector3(horizontalInput, verticalInput) * speed * PowerUpSpeed * Time.deltaTime);

        }
        else
        {
            transform.Translate(new Vector3(horizontalInput, verticalInput) * speed * Time.deltaTime);
        }

        if (transform.position.x < -9.5)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
        if (transform.position.x > 9.5)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        if (transform.position.y < -4)
        {
            transform.position = new Vector3(transform.position.x, -4, 0);
        }
        if (transform.position.y > 4)
        {
            transform.position = new Vector3(transform.position.x, 4, 0);
        }
    }

    public void TripleShotPowerUpOn()
    {
        CanTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        CanTripleShot = false;
    }

    public void SpeedPowerUpOn()
    {
        IsSpeedBoostActive = true;
        StartCoroutine(SpeedPowerDownRoutine());
    }

    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        IsSpeedBoostActive = false;
    }

    public void ShieldPowerUpOn()
    {
        IsShieldActive = true;
        shieldGameObject.SetActive(true);
    }

    public void TakeDamage()
    {
        if (IsShieldActive)
        {
            IsShieldActive = false;
            shieldGameObject.gameObject.SetActive(false);
            return;
        }
        hitCount++;

        if (hitCount == 1)
        {
            engines[0].SetActive(true);
        }
        else if (hitCount == 2)
        {
            engines[1].SetActive(true);
        }

        Life--;
        UiManager.UpdateLives(Life);

        if (Life <= 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameManager.PauseGame = true;
            gameManager.FindAndDestroyObjects();
            UiManager.GameOver();
            Destroy(this.gameObject);
        }
    }
}
