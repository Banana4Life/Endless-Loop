using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShotBouncyBall : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float impulseStrength = 10f;
    public float height = 0.9f;
    public float offset = 5;
    private bool _didShoot = false;
    private GameObject _projectile = null;
    public float projectileLifetime = 5f;
    public float projectileRotationImpulse = 5f;
    public AudioSource audioSource;

    // Update is called once per frame
    void Update()
    {
        bool shoot;
        var shootKeyDown = Input.GetButtonDown("Fire1");
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

        if (shoot && !_projectile)
        {
            _projectile = Instantiate(projectilePrefab);
            var playerPos = transform.position;


            var forward = transform.forward;
            _projectile.transform.position = new Vector3(playerPos.x, height, playerPos.z) + forward * offset;
            var body = _projectile.GetComponentInChildren<Rigidbody>();
            body.AddForce(forward * impulseStrength, ForceMode.Impulse);
            body.AddTorque(Vector3.up * projectileRotationImpulse, ForceMode.Impulse);
            
            audioSource.Play();
            
            Invoke(nameof(KillProjectile), projectileLifetime);
        }
    }

    private void KillProjectile()
    {
        if (_projectile)
        {
            Destroy(_projectile);
            _projectile = null;
        }
    }
}
