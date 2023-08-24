using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grape : MonoBehaviour,IEnemy
{
    [SerializeField] private GameObject grapeProjectilePerfab;

    private Animator myAnimator;
    private SpriteRenderer spriteRenderer;

    readonly int ATTACK_HASH = Animator.StringToHash("Attack");

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Attack()
    {
        myAnimator.SetTrigger(ATTACK_HASH);

        if (transform.position.x - PlayerController.Instance.transform.position.x < 0)
        {
            spriteRenderer.flipX = false;
        }else
        {
            spriteRenderer.flipX = true;
        }
        FindObjectOfType<AudioManager>().Play("Spit");
    }

    public void SpawnProjectileAnimEvent()
    {
        Instantiate(grapeProjectilePerfab,transform.position,Quaternion.identity);
    }
}
