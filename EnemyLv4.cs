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
        // 1. �Ѿ��� �ʿ��ϴ�.
        GameObject bullet = Instantiate(bulletFactory);
        // 2. ������ ������Ʈ�Ѵ�. 
        deltaAngle += 15;
        // 2-1. ������ 360���� �Ǹ� ������ �ʱ�ȭ��Ų��. 
        if (deltaAngle >= 360)
        {
            print("reset angle");
            deltaAngle = 0;
        }
        // 3. �߻��� ������ ���ϰ� �ʹ�.
        bullet.transform.eulerAngles = new Vector3(0, 0, deltaAngle) ;

        // 4. �Ѿ� �ϳ� �߻��ϰ� �ʹ�.
        bullet.transform.position = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        //transform.position += dir * speed * Time.deltaTime;
        currentTime += Time.deltaTime;
        // �����ð��� �Ǿ�����
        if (currentTime >= createTime)
        {
            ShootBullet();
            currentTime = 0;
        }
        // �Ѿ��� �����
        // ������ ����ؼ� ���ؼ� ������Ʈ�ϰ�
        // �� ������ �Ѿ��� �߻��Ѵ�. 
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyBullet.CollisionEnemy(explosionFactory, transform, collision, gameObject);     
    }


}
