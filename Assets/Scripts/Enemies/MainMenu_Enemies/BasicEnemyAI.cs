using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{
    [SerializeField] private float roamChangeDirFloat = 2f;

    

    private enum State
    {
        Roaming
    }

    private State state;
    private BasicEnemyPathfinding basicEnemyPathfinding;

    private Vector2 roamPosition;
    private float timeRoaming = 0f;

    private void Awake()
    {
        basicEnemyPathfinding=GetComponent<BasicEnemyPathfinding>();
        state = State.Roaming;
    }

    private void Start()
    {
        roamPosition = GetRoamingPosition();
    }
    private void Update()
    {
        Roaming();
    }

    private void Roaming()
    {
        timeRoaming += Time.deltaTime;
        basicEnemyPathfinding.MoveTo(roamPosition);

        if (timeRoaming > roamChangeDirFloat)
        {
            roamPosition = GetRoamingPosition();
        }
    }

    private Vector2 GetRoamingPosition()
    {
        timeRoaming = 0f;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
