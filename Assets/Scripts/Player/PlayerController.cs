using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singletoon<PlayerController>
{
    public bool FacingLeft { get { return facingLeft;} }
    
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private float DashTime = .2f;
    [SerializeField] private float DashCD = .25f;
    [SerializeField] private TrailRenderer MyTrailRenderer;
    [SerializeField] private Transform weaponCollider;
    [SerializeField] private Transform SlashAnimSpawnPoint;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer MySpriteRender;
    private Knockback knockback;
    private float startingMovementSpeed;
    private AudioManager audioManager;

    private bool facingLeft = false;
    private bool isDashing = false;

    protected override void Awake() {
      base.Awake();

      playerControls= new PlayerControls();  
      rb =GetComponent<Rigidbody2D>();
      myAnimator=GetComponent<Animator>();
      MySpriteRender = GetComponent<SpriteRenderer>();
      knockback= GetComponent<Knockback>();
      audioManager=FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash();
        startingMovementSpeed = moveSpeed;
        ActiveInventory.Instance.EquipStartingWeapon();
    }

    private void OnEnable() {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update() {
        PlayerInput();
    }

    private void FixedUpdate(){
        AdhustPlayerFacingDirection();
        Move();
    }

    public Transform GetWeaponCollider()
    {
        return weaponCollider;
    }

    public Transform GetSlashAnimSpawnPoint()
    {
        return SlashAnimSpawnPoint;
    }

    private void PlayerInput(){
        movement =playerControls.Movement.Move.ReadValue<Vector2>();
       
       myAnimator.SetFloat("MoveX",movement.x);
       myAnimator.SetFloat("MoveY",movement.y);
    }

    private void Move(){
        if (knockback.GettingKnockedBack||PlayerHealth.Instance.IsDead) { return; }
        rb.MovePosition(rb.position + movement * (moveSpeed *Time.fixedDeltaTime));       
    }

    private void AdhustPlayerFacingDirection(){
        Vector3 mousePos=Input.mousePosition;
        Vector3 PlayerScreenPoint=Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x<PlayerScreenPoint.x)
        {
            MySpriteRender.flipX = true;
            facingLeft = true;
        }else
        {
            MySpriteRender.flipX = false;
            facingLeft = false;
        }

    }
    private void Dash()
    {
        if (!isDashing&&Stamina.Instance.currentStamina>0)
        {
            Stamina.Instance.UseStamina();
            isDashing = true;
            moveSpeed *= dashSpeed;
            MyTrailRenderer.emitting = true;
            audioManager.Play("Dash");
            StartCoroutine(EndDashRoutine());
        }
      
    }

    private IEnumerator EndDashRoutine()
    {
        yield return new WaitForSeconds(DashTime);
        moveSpeed = startingMovementSpeed;
        MyTrailRenderer.emitting = false;
        yield return new WaitForSeconds(DashCD);
        isDashing=false;
    }

}
