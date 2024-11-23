using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private float health;
    public float burnAmt;

    public void loseHealth(float healthAmt)
    {
        health -= healthAmt;
    }
    public void death()
    {
        if (burnAmt == health)
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        burnAmt = Mathf.Clamp(burnAmt, 0, health);
        death();
    }
}
