using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;



public class PlayerController : MonoBehaviour
    {
    public float speed = 5f;
    public float jumpspeed = 8f;
    public float jumpTime;
    private float jumpTimeCounter;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;
    private float direction = 0f;
    private Rigidbody2D player;
    private Animator playerAnimation;
    private SpriteRenderer sprite;
    private bool isJumping;
    public SpriteLibrary spriteLibrary;
    private SpriteLibraryAsset present;
    [SerializeField] 
    public SpriteLibraryAsset[] skins;
    public int selectedSkin=0;



        // Start is called before the first frame update
    void Start()
    {
        spriteLibrary = GetComponent<SpriteLibrary>();
        player = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        playerAnimation = GetComponent<Animator>();
        
    }
 
    void SelectSkin()
    {
        spriteLibrary.spriteLibraryAsset = skins[selectedSkin];
    }

    void Update()
    {
        int previousSelectedSkin = selectedSkin;
        if (Input.GetKeyDown(KeyCode.A))
        {
            selectedSkin = (selectedSkin + 2) % 3;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            selectedSkin = (selectedSkin + 1) % 3;
        }
        if (previousSelectedSkin != selectedSkin)
        {
            SelectSkin();
        }
        
    
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        direction = Input.GetAxis("Horizontal");
            
        if (direction > 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            sprite.flipX = false;
        }
        else if (direction < 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            sprite.flipX = true;
        }
        else
        {
           // player.velocity = new Vector2(0, player.velocity.y);
        }
        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            player.velocity = new Vector2(player.velocity.x, jumpspeed);
        }
        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                player.velocity = new Vector2(player.velocity.x, jumpspeed);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        playerAnimation.SetFloat("Speed", Mathf.Abs(player.velocity.x));
        playerAnimation.SetBool("OnGround", isTouchingGround);
    }
}

