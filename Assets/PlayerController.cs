using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    //wartosc wczytana z klawiatury/pada
    Vector2 input;

    //mnoznik przyspieszenia statjy
    public float enginePower = 10;

    //mnoznik sterowania
    public float gyroPower = 2;

    private CameraScript cs;


    //pocisk prefab
    public GameObject bulletPrefab;
    //okresl miejsca spawnowania pociskow
    public Transform gunLeft, gunRight;

    //
    public float bulletSpeed = 5;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cs = Camera.main.transform.GetComponent<CameraScript>();
        input = Vector2.zero;
        gunLeft = transform.Find("GunLeft").transform;
        gunRight = transform.Find("GunRight").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //sterowanie w poziomie a/d
        float x = Input.GetAxis("Horizontal");
        //sterowanie w pionie w/s
        float y = Input.GetAxis("Vertical");

        input.x = x;
        input.y = y;

        //teleportuj statek jesli wyjdzie z ekranu
        if(Mathf.Abs(transform.position.x) > cs.gameWidth / 2 )
        {
            //wylecielismy z ekranu w poziomie
            Vector3 newPosition = new Vector3(transform.position.x * (-0.99f), 
                                                0,
                                                transform.position.z);
            transform.position = newPosition;
        }
        if (Mathf.Abs(transform.position.z) > cs.gameHeight / 2)
        {
            //wylecieliœmy z ekranu w poziomie - teleportuj na druga strone
            Vector3 newPosition = new Vector3(transform.position.x,
                                                0,
                                                transform.position.z * (-0.99f));
            transform.position = newPosition;
        }
        if (Input.GetKeyDown(KeyCode.Space))
            Fire();


    }
    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * input.y * enginePower, ForceMode.Acceleration);
        rb.AddTorque(transform.up * input.x * gyroPower, ForceMode.Acceleration);
    }
    void Fire()
    {
        //zmien predkosc pocisku
        GameObject leftBullet = Instantiate(bulletPrefab, gunLeft.position, Quaternion.identity);
        leftBullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed, ForceMode.VelocityChange);
        //zniszcz poscisk po 5sec
        Destroy(leftBullet, 5);


        //zmien predkosc pocisku
        GameObject rightBullet = Instantiate(bulletPrefab, gunRight.position, Quaternion.identity);
        rightBullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed, ForceMode.VelocityChange);
        //zniszcz poscisk po 5sec
        Destroy(rightBullet, 5);

    }
}
