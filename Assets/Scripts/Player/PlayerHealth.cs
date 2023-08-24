using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : Singletoon<PlayerHealth>
{
    public bool IsDead { get; private set; }
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;
    [SerializeField] private Sprite fullHeartImage, emptyHeartImage;

    private Transform healthContainer;
    private int currentHealth;
    private bool canTakeDamage=true;
    private Knockback knockback;
    private Flash flash;
    private AudioManager audioManager;
    const string HEALTH_CONTAINER_TEXT = "HeartContainer";
    const string Town_TEXT = "Town";
    readonly int DEATH_HASH = Animator.StringToHash("Death");

    protected override void Awake()
    {
        base.Awake();

        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
        audioManager = FindObjectOfType<AudioManager>();
        currentHealth = maxHealth;
    }

    private void Start()
    {
        IsDead = false;
        healthContainer = GameObject.Find(HEALTH_CONTAINER_TEXT).transform;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyAI enemy=other.gameObject.GetComponent<EnemyAI>();
        if (enemy&& canTakeDamage)
        {
            TakeDamage(1,other.transform);
        }
    }

    public void HealPlayer()
    {
        if(currentHealth< maxHealth)
        {
            currentHealth+=1;
            audioManager.Play("Heal");
        }
        UpdateHeartIamages();
        
    }
    public void ReplenishHealthOnDeath()
    {
        currentHealth = maxHealth;
        UpdateHeartIamages();
    }



    public void TakeDamage(int damageAmount,Transform hitTransform)
    {
        if (!canTakeDamage) { return; }

        ScreenShakeManager.Instance.ShakeScreen();
        knockback.GetKnockedBack(hitTransform, knockBackThrustAmount);
        audioManager.Play("PlayerDamaged");
        StartCoroutine(flash.FlashRoutine());
        canTakeDamage = false;
        currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());
        UpdateHeartIamages();
        CheckIfPlayerDeath();
    }

    private void CheckIfPlayerDeath()
    {
        if (currentHealth <= 0 && !IsDead)
        {
            IsDead =true;
            Destroy(ActiveWeapon.Instance.gameObject);
            currentHealth=0;
            audioManager.Play("PlayerDeath");
            GetComponent<Animator>().SetTrigger(DEATH_HASH);
            StartCoroutine(DeathLoadSceneRoutine());
        }
    }

    private void UpdateHeartIamages()
    {
       for (int i = 0; i < maxHealth; i++)
        {
            Transform child = healthContainer.GetChild(i);
            Image image = child?.GetComponent<Image>();
            
            if (i <= currentHealth - 1)
            {
                image.sprite = fullHeartImage;
            }
            else
            {
                image.sprite = emptyHeartImage;
            }
        }
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    private IEnumerator DeathLoadSceneRoutine()
    {
        yield return new WaitForSeconds(3f);
        FindObjectOfType<EnemyHealth>().HideEnemyHealthSlider();
        Destroy(gameObject);
        Stamina.Instance.ReplenishStaminaOnDeath();
        ReplenishHealthOnDeath();
        FindAnyObjectByType<AudioManager>().StopMusic();
        FindObjectOfType<AudioManager>().Play("Town");
        SceneManager.LoadScene(Town_TEXT);
    }
}
