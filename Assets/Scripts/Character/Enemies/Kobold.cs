using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kobold : MonoBehaviour
{
    [SerializeField] EnemyCharacterStats enemyStats;
    [SerializeField] float aggroRange;
    [SerializeField] float movementSpeed;
    [SerializeField] LayerMask possibleTargetLayer;

    Controller2D controller;
    private float gravity = -32;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<Controller2D>();
    }

    void Awake()
    {
        enemyStats.Health = enemyStats.MaxHealth;
        gameObject.name = "Kobold";
    }

    void Update()
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        Collider2D target = Physics2D.OverlapCircle(gameObject.transform.position, aggroRange, possibleTargetLayer);

        if (target != null)
        {
            if (target.transform.position.x > transform.position.x)
            {
                velocity.x = 1;
            }
            else
            {
                velocity.x = -1;
            }
        }
        else
        {
            velocity.x = 0;
        }

        velocity.y += gravity * Time.deltaTime;
        velocity.x *= movementSpeed;
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, aggroRange);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            Player target = other.GetComponent<Player>();
            target.TakeDamage((int)enemyStats.AttackDamage);
            target.Knockback(gameObject.transform, 7);
        }
    }

    public void TakeDamage(int amount)
    {
        enemyStats.Health -= (amount - enemyStats.Armor);
        if (enemyStats.Health <= 0)
        {
            ItemManager.Instance.AttemptDrop(1, gameObject.transform.position);
            QuestManager.Instance.MonsterSlayed(gameObject.name);
            Destroy(gameObject);
        }
    }
}
