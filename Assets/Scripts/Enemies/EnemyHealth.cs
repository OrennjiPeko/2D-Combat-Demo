using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int MaxHealth = 3;
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private float knockBackThrust = 15f;
    [SerializeField] private bool isBoss = false;
    
    private GameObject enemyHealthContainer;
    private int currentHealth;
    private Knockback knockback;
    private Flash flash;
    private Slider healthSlider;
    const string ENEMY_HEALTH_CONTAINER_TEXT = "EnemyHeartContainer";

    private void Awake()
    {
        flash=GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
        enemyHealthContainer = GameObject.Find("UI_Canvas").transform.Find(ENEMY_HEALTH_CONTAINER_TEXT).gameObject;
    }
    private void Start()
    {
        currentHealth = MaxHealth; 
        if (isBoss == true)
        {
            enemyHealthContainer.SetActive(true);
            UpdateHealthSlider();
        }
        else{
            HideEnemyHealthSlider();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth-=damage;
        if(isBoss==true) UpdateHealthSlider();
        knockback.GetKnockedBack(PlayerController.Instance.transform, knockBackThrust);
        FindObjectOfType<AudioManager>().Play("Impact");
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());
    }

    private IEnumerator CheckDetectDeathRoutine()
    {
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        DetectDeath();
    }

    private void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            Instantiate(deathVFXPrefab,transform.position,Quaternion.identity);
            enemyHealthContainer.SetActive(false);
            GetComponent<PickUpSpawner>().DropItem();
            FindObjectOfType<AudioManager>().Play("EnemyDeath");
            if(isBoss == true)
            {
                FindObjectOfType<AudioManager>().Stop("BossTheme");
                FindObjectOfType<AudioManager>().Play("CaveMusic");
            }
            Destroy(gameObject);
        }
        
    }

    private void UpdateHealthSlider()
    {
        if (healthSlider == null)
        {
            healthSlider = GameObject.Find("EnemyHealth").GetComponent<Slider>();
        }

        healthSlider.maxValue = MaxHealth;
        healthSlider.value = currentHealth;
    }

    public void HideEnemyHealthSlider()
    {
        enemyHealthContainer.SetActive(false);
    }
}
