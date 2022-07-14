using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 자기가 향하는 방향으로 이동하고 싶다. 

public class EnemyBullet : MonoBehaviour
{

    public float speed = 5;
    public GameObject explosionFactory;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 자기가 향하는 방향으로 이동하고 싶다. 
        // P - P0 + vt
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyBullet.CollisionEnemy(explosionFactory, transform, collision, gameObject);
    }

    public static void CollisionEnemy(GameObject explosionFactory, Transform transform, Collision collision, 
        GameObject gameObject)
        
    {
        GameObject explosion = Instantiate(explosionFactory);
        explosion.transform.position = transform.position;

        if (collision.gameObject.name.Contains("Bullet"))
        {
            GameObject target = GameObject.Find("Player");
            if (target)
            {
                PlayerFire player = target.GetComponent<PlayerFire>();
                player.bulletPool.Add(collision.gameObject);
                collision.gameObject.SetActive(false);
            }
        }
        else
        {
            PlayerHealth.Instance.HP--;
        }


        Destroy(gameObject);
    }


}
