using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    static PlayerMove instance;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Player character;
    float horizontal;
    float vertical;
    bool lookingLeft;
    public bool isDead;

    void Awake()
    {
        character = GetComponent<Player>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lookingLeft = false;
        instance = this;
        isDead = false;
    }

    // Chú thích: Hệ thống điều khiển di chuyển của Nhân vật chính


    void Update()
    {
        if (!PauseMenu.isPaused && !Level.GetIsLevelUpTime())
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            // Tạo vector di chuyển và chuẩn hóa nó để đi hướng nào tốc độ cũng bằng nhau
            Vector2 moveDirection = new Vector2(horizontal, vertical);
            if (moveDirection.magnitude > 0f)
            {
                moveDirection.Normalize(); // Đưa độ dài vector về 1

                animator.SetInteger("AnimState", 1);

                // Xử lý lật Sprite dựa trên hướng hoành độ của Vector đã chuẩn hóa
                if (moveDirection.x > 0f) { spriteRenderer.flipX = false; lookingLeft = false; }
                else if (moveDirection.x < 0f) { spriteRenderer.flipX = true; lookingLeft = true; }
            }
            else
            {
                animator.SetInteger("AnimState", 0);
            }

            if (!isDead)
            {
                // Nhân với tốc độ chuẩn của nhân vật
                transform.Translate(moveDirection * character.GetSpeed() * Time.deltaTime);
            }
        }
    }

    public static PlayerMove GetInstance()
    {
        return instance;
    }

    public bool GetLookingLeft()
    {
        return lookingLeft;
    }

    public float GetHorizontal()
    {
        return horizontal;
    }

    public float GetVertical()
    {
        return vertical;
    }
}