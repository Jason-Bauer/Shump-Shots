using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShoot : MonoBehaviour {

    public GameObject bulletPrefab, laserPrefab, shieldPrefab, bombPrefab;
    public Transform bulletSpawn;
    public GameObject playButton;
    public float coolDown, coolDown2;

    //private string color;
    private ShipColor shipColor;

    private bool shoot, shoot2, shot, shot2;
    public float axis;
    private float x, y, z;
    private Material explosmat;
    private Color exploscol;

    //public Material currentColor;

    // Use this for initialization
    void Start()
    {
        shot = false;
        shot2 = false;
        shoot = true;
        shoot2 = true;
        coolDown = 15;
        coolDown2 = 45;

        shipColor = gameObject.GetComponent<ShipColor>();
    }

    void createExplosion()
    {
        GameObject bullet = Instantiate(
            bombPrefab,
            bulletSpawn.position,
            bombPrefab.transform.rotation);


        bullet.gameObject.tag = "Explosion";
        bullet.transform.position = new Vector3(x+0.2f, y, z);

        bullet.GetComponent<Renderer>().material = explosmat;
        bullet.GetComponent<Renderer>().material.color = exploscol;
        bullet.GetComponent<BulletMove>().playButton = playButton;
        Destroy(bullet, 2);
    }

    void Fire_Bomb()
    {
        // Create the Bullet from the Bullet Prefab
        GameObject bullet = Instantiate(
            bombPrefab,
            bulletSpawn.position,
            bombPrefab.transform.rotation);

        
        bullet.gameObject.tag = "PreBomb";
        bullet.transform.position = new Vector3(bullet.transform.position.x + 0.2f, 0.0f, bullet.transform.position.z);

        bullet.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
        bullet.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
        bullet.GetComponent<BulletMove>().playButton = playButton;
        x = bulletSpawn.position.x;
        y = bulletSpawn.position.y;
        z = bulletSpawn.position.z;
        explosmat = GetComponent<Renderer>().material;
        exploscol = GetComponent<Renderer>().material.color;
        Destroy(bullet, 1);
        Invoke("createExplosion", 1);
    }

    // Fire a 3-shot spread
    void Fire_Spread()
    {
        // Create the Bullet from the Bullet Prefab
        GameObject[] bullets = new GameObject[3];
        for(int i = 0; i < 3; i++)
        {
            bullets[i] = Instantiate(
                bulletPrefab,
                bulletSpawn.position,
                bulletPrefab.transform.rotation);

            if (i == 0)
            {
                bullets[i].transform.position = new Vector3(bullets[i].transform.position.x + 0.5f, 0.0f, bullets[i].transform.position.z -0.2f);
                bullets[i].gameObject.tag = "Bullet2right";
                Destroy(bullets[i], 2);
            }
            if (i == 1)
            {
                bullets[i].transform.position = new Vector3(bullets[i].transform.position.x + 0.5f, 0.0f, bullets[i].transform.position.z);
                bullets[i].gameObject.tag = "Bullet2";
                Destroy(bullets[i], 2);
            }
            if (i == 2)
            {
                bullets[i].transform.position = new Vector3(bullets[i].transform.position.x + 0.5f, 0.0f, bullets[i].transform.position.z + 0.2f);
                bullets[i].gameObject.tag = "Bullet2left";
                Destroy(bullets[i], 2);
            }
            bullets[i].GetComponent<Renderer>().material = GetComponent<Renderer>().material;
            bullets[i].GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
            bullets[i].GetComponent<BulletMove>().playButton = playButton;
        }

        // Destroy the bullet after 2 seconds
    }

    void Fire_Laser()
    {
        // Create the Bullet from the Bullet Prefab
        GameObject[] bullets = new GameObject[2];
        for (int i = 0; i < 2; i++)
        {
            bullets[i] = Instantiate(
                laserPrefab,
                bulletSpawn.position,
                laserPrefab.transform.rotation);

            if (i == 0)
            {
                bullets[i].transform.position = new Vector3(bullets[i].transform.position.x + 0.25f, 0.0f, bullets[i].transform.position.z - 0.2f);
                bullets[i].gameObject.tag = "Bullet3right";
                Destroy(bullets[i], 1);
            }
            if (i == 1)
            {
                bullets[i].transform.position = new Vector3(bullets[i].transform.position.x + 0.25f, 0.0f, bullets[i].transform.position.z + 0.2f);
                bullets[i].gameObject.tag = "Bullet3left";
                Destroy(bullets[i], 1);
            }
            bullets[i].GetComponent<Renderer>().material = GetComponent<Renderer>().material;
            bullets[i].GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
            bullets[i].GetComponent<BulletMove>().playButton = playButton;
        }

        // Destroy the bullet after 2 seconds
    }

    void Fire_Shield()
    {
        // Create the Bullet from the Bullet Prefab
        GameObject bullet = Instantiate(
            shieldPrefab,
            bulletSpawn.position,
            shieldPrefab.transform.rotation);


        bullet.gameObject.tag = "Shield";
        bullet.transform.position = new Vector3(bullet.transform.position.x+0.2f, 0.0f, bullet.transform.position.z);

        bullet.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
        bullet.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
        bullet.GetComponent<BulletMove>().playButton = playButton;
        Destroy(bullet, 0.4f);
    }

    // Update is called once per frame
    void Update () {
        axis = Input.GetAxis("Triggers");
        if (shot)
        {
            coolDown-= 1;
        }

        if (shot2)
        {
            coolDown2 -= 1;
        }


        // Cooldown is up
        if (coolDown < 0)
        {
            shot = false;
            shoot = true;
            coolDown = 15;
        }

        // While waiting for shoot cooldown
        else if (coolDown < 15 && coolDown > 0)
        {
            shoot = false;
        }

        if (coolDown2 < 0)
        {
            shot2 = false;
            shoot2 = true;
            coolDown2 = 45;
        }

        else if (coolDown2 < 45 && coolDown > 0)
        {
            shoot2 = false;
        }

        // If it's time to shoot
        if (shoot)
        {
            if (Input.GetKey(KeyCode.Space)|| Input.GetKey(KeyCode.Mouse0) || Input.GetAxis("Triggers") == 1 || Input.GetAxis("Triggers") == - 1)
            {
                shot = true;

                //switch()
                //Debug.Log("Shoot!");

                if (shipColor.currColor == GameColor.red)
                {
                    Fire_Spread();
                }
                else if (shipColor.currColor == GameColor.blue)
                {
                    Fire_Laser();
                }
                else if (shipColor.currColor == GameColor.green)
                {
                    Fire_Shield();
                }
            }
        }
        if (shoot2)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0) || Input.GetAxis("Triggers") == 1 || Input.GetAxis("Triggers") == -1)
            {
                shot2 = true;
                if (shipColor.currColor == GameColor.yellow)
                {
                    Fire_Bomb();
                }
            }
        }
    }
}
