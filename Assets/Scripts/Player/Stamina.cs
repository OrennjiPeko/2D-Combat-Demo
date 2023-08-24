using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : Singletoon<Stamina>
{
   public int currentStamina { get; private set; }
    [SerializeField] private Sprite fullStaminaImage, emptyStaminaImage;
    [SerializeField] private int timeBetweenStaminaRefresh = 3;

    private Transform staminaContainer;
    private int startingStamina = 3;
    private int maxStamina;
    const string STAMINA_CONTAINER_TEXT = "StaminaContainer";

    protected override void Awake()
    {
        base.Awake();

        maxStamina = startingStamina;
        currentStamina = startingStamina;
    }
    private void Start()
    {
        staminaContainer = GameObject.Find(STAMINA_CONTAINER_TEXT).transform;
    }
    public void UseStamina()
    {
        currentStamina--;
        UpdateStaminaImages();
        StopAllCoroutines();
        StartCoroutine(RefreashStaminaRoutine());
    }

    public void RefreshStamina()
    {
        if (currentStamina < maxStamina && !PlayerHealth.Instance.IsDead)
        {
            FindObjectOfType<AudioManager>().Play("RestoreStamina");
            currentStamina++;
        }
        UpdateStaminaImages();
    }

    public void ReplenishStaminaOnDeath()
    {
        currentStamina = startingStamina;
        UpdateStaminaImages();
    }
   
    private IEnumerator RefreashStaminaRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenStaminaRefresh);
            RefreshStamina();
        }
    }

    private void UpdateStaminaImages()
    {
        for(int i = 0; i < maxStamina; i++)
        {
            Transform child =staminaContainer.GetChild(i);
            Image image = child?.GetComponent<Image>();
            
            if (i <= currentStamina - 1)
            {
                image.sprite = fullStaminaImage;
            }
            else
            {
                image.sprite = emptyStaminaImage;
            }
        }    
        
    }
}
