using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] Player owner;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Enemy")
        {
            other.GetComponent<Kobold>().TakeDamage((int)owner.PlayerStats.AttackDamage);
        }
    }

}
