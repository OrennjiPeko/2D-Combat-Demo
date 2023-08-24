using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject goldCoinPrefab, HealthGlobePrefab, staminaGlobePreFab;

    public void DropItem()
    {
       int RandomNum=Random.Range(1,5);
        if (RandomNum == 1)
        {
            Instantiate(HealthGlobePrefab, transform.position, Quaternion.identity);
        }
        if (RandomNum == 2)
        {
            Instantiate(staminaGlobePreFab, transform.position, Quaternion.identity);
        }if(RandomNum == 3)
        {
            int randomAmountOfGold=Random.Range(1,4);
            for(int i = 0; i < randomAmountOfGold; i++)
            {
                Instantiate(goldCoinPrefab, transform.position, Quaternion.identity);
            }
            
        }
        
    }
}
