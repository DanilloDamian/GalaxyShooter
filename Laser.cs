using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float laserSpeed = 10;

    void Update()
    {
        transform.Translate(Vector3.up * laserSpeed * Time.deltaTime);

        if (transform.position.y > 5)
        {
            Destroy(this.gameObject);
        }
    }

}
