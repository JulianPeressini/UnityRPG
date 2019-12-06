using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject questLogUI;
    private Inventory myInv;
    [SerializeField] private PlayableCharacterStats playerStats;

    [SerializeField] GameObject playerWeapon;

    Controller2D controller;

    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackDuration;
    [SerializeField] private float maxJumpSpeed;
    [SerializeField] private float minJumpSpeed;
    [SerializeField] private float maxJumpHeigth;
    [SerializeField] private float minJumpHeigth;
    [SerializeField] private float timeToJumpApex;
    [SerializeField] private float movementSpeed;
    private float gravity;

    private bool nearNPC;
    private bool interacting;
    private FriendlyCharacterInteraction nearFriendly;

    private bool knocked;
    private float knockDuration = 0.5f;
    private float knockTimer = 0;

    private bool attacking;
    private bool canAttack;
    private float attackTimer;
    private float attackCooldownTimer;

    private Vector3 velocity;
    private Vector2 input;

    public PlayableCharacterStats PlayerStats { get { return playerStats; } set { playerStats = value; } }

    void Awake()
    {
        menuUI.SetActive(false);
        questLogUI.SetActive(false);
        playerStats.InitializeStats();
    }

    void Start()
    {
        controller = GetComponent<Controller2D>();
        gravity = -(2 * maxJumpHeigth) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpSpeed = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpHeigth = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeigth);
        myInv = menuUI.GetComponentInChildren<Inventory>();
    }

    void Update()
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        if (Time.time > knockTimer)
        {
            knocked = false;
        }

        if (!interacting)
        {
            if (!menuUI.activeSelf)
            {
                if (!knocked)
                {
                    GetInputs();
                }
                
            }
            else if (inventoryUI.activeSelf)
            {
                GetInventoryInputs();
            }
            else
            {
                GetQuestLogInputs();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                interacting = false;
            }

            if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                interacting = false;
            }
        }

        if (!knocked)
        {
            velocity.x = input.x * movementSpeed;
        }

        if (attacking)
        {
            if (Time.time > attackTimer)
            {
                playerWeapon.SetActive(false);
                attacking = false;
            }
        }

        if (!canAttack)
        {
            if (Time.time > attackCooldownTimer)
            {
                canAttack = true;
            }
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void GetInputs()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            if (controller.collisions.below)
            {
                velocity.y = maxJumpSpeed;
            }
        }

        if (Input.GetKeyUp(KeyCode.Keypad2))
        {
            if (velocity.y > minJumpSpeed)
            {
                velocity.y = minJumpSpeed;
            }
        }

        if (Input.GetKeyDown(KeyCode.Keypad8) && velocity.x == 0 && velocity.y == 0)
        {
            if (nearNPC)
            {
                nearFriendly.Interact();
                interacting = true;
            }
            else
            {
                menuUI.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Keypad4) && canAttack)
        {
            attackCooldownTimer = Time.time + attackCooldown;
            attackTimer = Time.time + attackDuration;

            canAttack = false;
            attacking = true;
            playerWeapon.SetActive(true);
        }
    }

    private void GetInventoryInputs()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            myInv.MoveMarker("Up");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            myInv.MoveMarker("Left");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            myInv.MoveMarker("Down");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            myInv.MoveMarker("Right");
        }

        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            menuUI.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            myInv.SelectOnSlot();
        }

        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            myInv.Cancel();
        }

        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            myInv.UseOnSlot();
        }

        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            inventoryUI.SetActive(false);
            questLogUI.SetActive(true);
        }
    }

    private void GetQuestLogInputs()
    {
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            menuUI.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            questLogUI.SetActive(false);
            inventoryUI.SetActive(true);
        }
    }

    public void FriendlyOnRange(FriendlyCharacterInteraction _nearFriendly)
    {
        nearNPC = true;
        nearFriendly = _nearFriendly;
    }

    public void FriendlyOutOfRange()
    {
        nearNPC = false;
        nearFriendly = null;
    }

    public void TakeDamage(int amount)
    {
        playerStats.Health -= (amount - playerStats.Armor);

        if (playerStats.Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Knockback(Transform source, int force)
    {
        velocity.y = force * 1.5f;

        if (source.position.x < transform.position.x)
        {
            velocity.x = force;
        }
        else
        {
            velocity.x = -force;
        }

        knocked = true;
        knockTimer = Time.time + knockDuration;
    }
}
