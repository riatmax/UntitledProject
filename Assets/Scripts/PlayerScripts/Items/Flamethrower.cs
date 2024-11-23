using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : EquippableItem
{
    [Header("Flamethrower Stats")]
    [SerializeField] private float flameRange;
    [SerializeField] private float flameRadius;
    [SerializeField] private float flameDamage;

    [SerializeField] private Transform nozzle;

    private bool isFlaming = false;
    public override void LeftClick()
    {
        if (Input.GetAxisRaw("Fire1") > 0 && !isFlaming)
        {
            isFlaming = true;
            RaycastHit[] hits;
            hits = Physics.CapsuleCastAll(nozzle.transform.position, 
                                   new Vector3(nozzle.transform.position.x, nozzle.transform.position.y, nozzle.transform.position.z + flameRange),
                                   flameRadius, 
                                   Vector3.forward, 
                                   flameRange);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject.GetComponent<Enemy>() != null)
                {
                    hits[i].collider.gameObject.GetComponent<Enemy>().burnAmt += flameDamage;
                }
            }
            isFlaming = false;
        }
    }

    public override void RightClick()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LeftClick();
    }
}
