using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LookAtPlayer : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform Player;

    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!(GameObject.Find("Body") == null))
        {
        enemy.SetDestination(Player.position);
        }
        else
        {
            enemy.ResetPath();
        }
        
        
    }
}
