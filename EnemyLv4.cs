using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLv4 : MonoBehaviour
{
    public float speed = 3;
    public GameObject explosionFactory;
    public Transform target;
    Vector3 dir;
    public float createTime = 1; 
    float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        explosionFactory = (GameObject)Resources.Load("Prefabs/Explosion");
    }

    void SetDirection()
    {
        GameObject player = GameObject.Find("Player");

        if (player)
        {
            target = player.transform;
            int randomNum = Random.Range(0, 10);

            if (randomNum < 7)
            {
                dir = Vector3.down;
            }
            else
            {
                dir = player.transform.position - transform.position;
                dir.Normalize();
            }
        }
        else
        {
            dir = Vector3.down;
        }
    }

    public GameObject bulletFactory;

    float deltaAngle = 0;

    void ShootBullet()
    {  
        // 1. 총알이 필요하다.
        GameObject bullet = Instantiate(bulletFactory);
        // 2. 각도를 업데이트한다. 
        deltaAngle += 15;
        // 2-1. 각도가 360도가 되면 각도를 초기화시킨다. 
        if (deltaAngle >= 360)
        {
            print("reset angle");
            deltaAngle = 0;
        }
        // 3. 발사할 방향을 정하고 싶다.
        bullet.transform.eulerAngles = new Vector3(0, 0, deltaAngle) ;

        // 4. 총알 하나 발사하고 싶다.
        bullet.transform.position = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        //transform.position += dir * speed * Time.deltaTime;
        currentTime += Time.deltaTime;
        // 일정시간이 되었을때
        if (currentTime >= createTime)
        {
            ShootBullet();
            currentTime = 0;
        }
        // 총알을 만들고
        // 각도를 계속해서 더해서 업데이트하고
        // 그 각도로 총알을 발사한다. 
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyBullet.CollisionEnemy(explosionFactory, transform, collision, gameObject);     
    }


}
