using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMelee : MonoBehaviour
{
    private PlayerInput controls;
    private Input playerInputActions; 

    public float startup;
    public float activeTime;
    public float recovery;
    public int damage;

    public GameObject sword;    
    public GameObject prefabHitbox;
    public Camera playerCamera;
    public AudioClip swordSwingSFX;

    public bool attacking;
    public bool canAttack;
    public bool recovering;

    private float timer;
    private Animator animator;
    private GameObject weapon;
    private GunHolder weaponHolder;

    // Start is called before the first frame update
    void Awake()
    {
        controls = GetComponent<PlayerInput>();
        playerInputActions = new Input();
        
        timer = startup;
        attacking = false;
        canAttack = true;
        recovering = false;
        weaponHolder = GetComponent<GunHolder>();
        CurrentWeapon();
        animator = sword.GetComponent<Animator>();
        sword.SetActive(false);
    }

    void OnEnable()
    {
        playerInputActions.Player.Enable();
        playerInputActions.Player.Melee.performed += Fire;
    }

    private void OnDisable()
    {
      playerInputActions.Player.Melee.performed -= Fire;
      playerInputActions.Player.Disable();
    }

    // Update is called once per frame
    void Update()
    {

        if ((attacking | recovering) && timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (attacking && timer <= 0)
        {
            GameObject hitbox = Instantiate(prefabHitbox);
            MeleeHitbox meleeHitbox = hitbox.AddComponent<MeleeHitbox>();
            meleeHitbox.activeTime = activeTime;
            meleeHitbox.damage = damage;
            hitbox.transform.position = gameObject.transform.position;
            hitbox.transform.rotation = playerCamera.transform.rotation;
            attacking = false;
            recovering = true;
            timer = recovery;
        }

        if (recovering && timer <= 0)
        {
            weapon.SetActive(true);
            sword.SetActive(false);
            recovering = false;
            canAttack = true;
            weaponHolder.canSwitch = true;
        }
    }

    public void Fire(InputAction.CallbackContext ctx)
    {
        if (canAttack)
        {
            AudioSource audio = AudioPool.GetAudioSource();
            audio.clip = swordSwingSFX;
            audio.volume = 0.25f;
            audio.Play(0);
            CurrentWeapon();
            weaponHolder.canSwitch = false;
            weapon.SetActive(false);
            sword.SetActive(true);
            animator.SetTrigger("Attack");
            timer = startup;
            attacking = true;
            canAttack = false;
            recovering = false;
        }
    }
    private void CurrentWeapon()
    {
        if (weaponHolder.gunRoot != null && weaponHolder.gunRoot.activeSelf)
        {
            weapon = weaponHolder.gunRoot;
        }
    }
}
