using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private Vector3 lookingAt = new Vector3(0, 0, 0);
    private Transform playerPosition;
    private Camera cam;
    private Player player;

    public GameObject bulletPrefab;
    public float timeToDestroyBullet;

    private void Awake()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        playerPosition = GetComponent<Transform>();
        player = GetComponent<Player>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Fire.performed += Shoot;
    }

    private void Update()
    {
        Vector2 input = playerInputActions.Player.Look.ReadValue<Vector2>();
        lookingAt.x = input.x;
        lookingAt.y = input.y;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        Vector3 shootDirection = lookingAt - cam.WorldToScreenPoint(playerPosition.position);
        shootDirection.z = 0;
        GameObject bullet = Instantiate(
            bulletPrefab,
            playerPosition.position + new Vector3(shootDirection.x, 0, shootDirection.y).normalized,
            Quaternion.FromToRotation(new Vector3(1, 0, 0), shootDirection)
        );
        bullet.GetComponent<Bullet>().bulletDamage = player.damage;
        StartCoroutine(DestroyBullet(bullet));
    }
    IEnumerator DestroyBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(timeToDestroyBullet);
        Destroy(bullet);
    }
}