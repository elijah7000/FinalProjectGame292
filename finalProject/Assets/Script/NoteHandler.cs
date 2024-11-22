using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteHandler : MonoBehaviour
{
    public bool canBePressed;
    public bool noteHit;
    public KeyCode keyToPress;

    public GameObject hitEffect, goodEffect, missEffect, perfectEffect;

    void Update()
    {
        if (Input.GetKeyDown(keyToPress) && canBePressed && !noteHit)
        {
            noteHit = true;
            gameObject.SetActive(false);

            float notePosition = Mathf.Abs(transform.position.y);

            if (notePosition > 219.5f)
            {
                Debug.Log("Hit");
                GameManager.Instance.RegisterNormalHit();
                SpawnEffect(hitEffect);
            }
            else if (notePosition > 218f)
            {
                Debug.Log("Good");
                GameManager.Instance.RegisterGoodHit();
                SpawnEffect(goodEffect);
            }
            else
            {
                Debug.Log("Perfect");
                GameManager.Instance.RegisterPerfectHit();
                SpawnEffect(perfectEffect);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Activater"))
        {
            canBePressed = true;
            noteHit = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Activater"))
        {
            if (!noteHit)
            {
                GameManager.Instance.RegisterMissedNote();
                SpawnEffect(missEffect);
            }
            canBePressed = false;
        }
    }

    private void SpawnEffect(GameObject effect)
    {
        if (effect != null)
        {
            Instantiate(effect, transform.position, effect.transform.rotation);
        }
        else
        {
            Debug.LogWarning("Effect not assigned!");
        }
    }
}
