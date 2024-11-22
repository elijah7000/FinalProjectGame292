using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public bool noteHit;     public KeyCode keyToPress;

    public GameObject hitEffect, goodEffect, missEffect, perfectEffect;


    void Update()
    {
      
        if (Input.GetKeyDown(keyToPress) && canBePressed && !noteHit)
        {
            noteHit = true; 
            gameObject.SetActive(false); 
    

            if(Mathf.Abs(transform.position.y ) > 219.5  ){
                Debug.Log("Hit");
                GameManager.instance.NormalHit();
                Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
            }else if( Mathf.Abs(transform.position.y) > 218){
                  Debug.Log("Good");
                GameManager.instance.GoodHit();
                 Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
            } else{
                Debug.Log("Perfect");
                GameManager.instance.PerfectHit();
                 Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activater")
        {
            canBePressed = true;
            noteHit = false; 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activater")
        {
            if (!noteHit) 
            {
                GameManager.instance.NoteMissed();
                 Instantiate(missEffect, transform.position, missEffect.transform.rotation);
            }
            canBePressed = false; // 노트를 놓쳤으므로 canBePressed 설정 해제
        }
    }
}