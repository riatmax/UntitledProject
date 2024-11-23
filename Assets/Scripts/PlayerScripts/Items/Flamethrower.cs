using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : EquippableItem
{
    [Header("Flamethrower Stats")]
    [SerializeField] private float flameRange;
    [SerializeField] private float flameRadius;
    [SerializeField] private float flameDamage;
    [SerializeField] private float burnCooldown;

    [SerializeField] private Transform nozzle;

    private float nextDamageTime = 0f;
    private RaycastHit[] hits;
    public override void LeftClick()
    {
        hits = Physics.SphereCastAll(nozzle.transform.position,
                                        flameRadius,
                                        nozzle.transform.forward,
                                        flameRange);
        foreach (var hit in hits)
        {
            var enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.burnAmt += flameDamage * Time.deltaTime;
            }
        }
    }

    public override void RightClick()
    {
        
    }
    void Update()
    {
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            if (Time.time >= nextDamageTime)
            {
                nextDamageTime = Time.time + burnCooldown;
                LeftClick();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(nozzle.transform.position, flameRadius);
        Gizmos.DrawLine(nozzle.transform.position, nozzle.transform.position + nozzle.transform.forward * flameRange);
    }
}
