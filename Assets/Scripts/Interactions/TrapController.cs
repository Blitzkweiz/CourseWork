using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    public int damage;
    private CharacterController characterController;
    private float lastDamage;
    public float damageDelay;

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.CompareTag("Player") && lastDamage + damageDelay < Time.time && col.gameObject.TryGetComponent<CharacterController>(out characterController))
        {
            characterController.TakeDamage(damage);
            lastDamage = Time.time;
        }
    }
}
