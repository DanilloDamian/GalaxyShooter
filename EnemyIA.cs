using UnityEngine;

public class EnemyIA : MonoBehaviour
{    
    [SerializeField]
    private GameObject enemyExplosionPrefab;
    [SerializeField]
    private AudioClip audioClip;
    public UIManager UiManager;
    private float speedEnemy = 5f;

    void Start()
    {
        UiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        transform.Translate(Vector3.down * speedEnemy * Time.deltaTime);

        if (transform.position.y < -5)
        {
            float randomX = Random.Range(-8.75f, 8.75f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
            Destroy(this.gameObject);
            UiManager.UpdateScore();

        }
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage();
                UiManager.UpdateScore();
                Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
                AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
                Destroy(this.gameObject);
            }
        }
    }

    public void EnemyDestroy()
    {
        Destroy(this.gameObject);
    }

}
