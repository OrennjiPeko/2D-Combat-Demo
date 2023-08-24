using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject destoryVFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<DamageSource>()|| other.gameObject.GetComponent<Projectile>())
        {
            PickUpSpawner pickUpSpawner = GetComponent<PickUpSpawner>();
            pickUpSpawner?.DropItem();
            Instantiate(destoryVFX,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
