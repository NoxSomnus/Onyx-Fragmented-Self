using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCamp : MonoBehaviour
{

    private bool isPlayerInRange;
    private bool isResting=false;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject playerMovement;
    [SerializeField] private GameObject pressE;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && !isResting)
        {
            RestAndSave();
        }
        else if(isPlayerInRange && Input.GetKeyDown(KeyCode.E) && isResting)
        {
            StartCoroutine(Wait());

        }
    }
    private void RestAndSave()
    {
        pressE.SetActive(false);
        playerMovement.GetComponent<Animator>().SetBool("Rest", true);
        isResting = true;
        playerMovement.GetComponent<Movement>().enabled = false;

    }

    private IEnumerator Wait()
    {
        playerMovement.GetComponent<Animator>().SetBool("Rest", false);
        yield return new WaitForSeconds(1f); // Espera a que termine la animación de ataque
        playerMovement.GetComponent<Movement>().enabled = true;
        isResting = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pressE.SetActive(true);
            isPlayerInRange = true;

        }
    }
}
