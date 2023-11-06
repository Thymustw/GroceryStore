using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }


    private void Update()
    {   
        // Scenechanger.
        if (SceneManager.GetActiveScene().name == "ChooseScene")
            if (Input.GetMouseButtonDown(0) && MouseDetect().collider.CompareTag("AttackBox"))
                SceneManager.LoadScene("SampleScene");
    }


    // Create a ray to detect the things.
    private RaycastHit2D MouseDetect()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        
        return hit;
    }


    // A interface to bridge each units HP count.
    public void DamageCount(GameObject hurt)
    {
        Destroy(hurt);
    }
}
