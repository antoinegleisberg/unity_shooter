using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletDamage = 10;
    public float speed = 10;

    private Vector3 direction;

    private void Start()
    {
        Transform transform = GetComponent<Transform>();
        float angle = transform.rotation.eulerAngles.z * Mathf.PI / 180;
        direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)).normalized;
    }

    void FixedUpdate()
    {
        transform.position += direction * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Ennemy>()?.takeDamage(bulletDamage);
        Destroy(this.gameObject);
    }
}
