using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float speedPowerUp = 3;
    [SerializeField]
    private int powerupID;
    [SerializeField]
    private AudioClip audioClip;
   
    void Update()
    {
        transform.Translate(Vector3.down * speedPowerUp * Time.deltaTime);

        if (transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position, 1f);
                if (powerupID == 0)
                {
                    player.TripleShotPowerUpOn();
                }
                if (powerupID == 1)
                {
                    player.SpeedPowerUpOn();
                }
                if (powerupID == 2)
                {
                    player.ShieldPowerUpOn();
                }
            }
            PowerUpDestroy();
        }
    }

    public void PowerUpDestroy()    {        
        
        Destroy(this.gameObject);
    }

}
