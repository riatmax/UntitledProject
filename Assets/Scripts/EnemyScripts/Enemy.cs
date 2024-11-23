using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private float health;

    public void loseHealth(float healthAmt)
    {
        health -= healthAmt;
    }
    public float burnAmt
    {
        get
        {
            return burnAmt;
        }
        set
        {
            burnAmt = Mathf.Clamp(value, 0, 100);
        }
    }
    public void death()
    {
        if (health <= 0)
        {
            Destroy(this);
        }
    }
    private void Update()
    {
        death();
    }
}
