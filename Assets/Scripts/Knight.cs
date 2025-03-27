using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Knight : MonoBehaviour
{
    SpriteRenderer sr;
    Animator animator;
    public float speed = 2;
    public bool canRun = true;



    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float directiion = Input.GetAxis("Horizontal");


        sr.flipX = (directiion < 0);

        animator.SetFloat("movement", Mathf.Abs(directiion));


        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("attack");
            canRun = false;
        }

        if (canRun == true)
        {
            transform.position += transform.right * directiion * speed * Time.deltaTime;
        }
    }


    public void AttackHasFinished()
    {
        Debug.Log("The attack is finsihed");
        canRun = true;
    }
}