using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShotBouncyBall : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float impulseStrength = 10f;
    public float height = 0.9f;
    private bool _didShoot = false;

    // Update is called once per frame
    void Update()
    {
        bool shoot;
        var shootKeyDown = Input.GetButtonDown("Jump");
        if (shootKeyDown && _didShoot)
        {
            shoot = false;
        }
        else if (shootKeyDown && !_didShoot)
        {
            shoot = true;
            _didShoot = true;
        }
        else if (!shootKeyDown && _didShoot)
        {
            _didShoot = false;
            shoot = false;
        }
        else
        {
            shoot = false;
        }

        if (shoot)
        {
            Debug.LogWarning("shot!");
            var projectile = Instantiate(projectilePrefab);
            var playerRot = transform.rotation.eulerAngles;
            var playerPos = transform.position;

            projectile.transform.position = new Vector3(playerPos.x, height, playerPos.z);
            var body = projectile.GetComponentInChildren<Rigidbody>();
            var impulseDirection = new Vector3(0, playerRot.y, 0);
            body.AddForce(impulseDirection, ForceMode.Impulse);
        }
    }
}
