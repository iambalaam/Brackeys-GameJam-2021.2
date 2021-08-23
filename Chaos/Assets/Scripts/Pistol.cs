using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public Camera Cam;
    public GameObject smokeParticleSystem;
    public bool shooting = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        mousePos = Cam.ScreenToWorldPoint(mousePos);

        Vector2 direc = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        transform.right = direc;

        if(Input.GetMouseButton(0) && !shooting)
        {
            StartCoroutine(Shoot());
        }
    }

    public IEnumerator Shoot()
    {
        shooting = true;
        smokeParticleSystem = Resources.Load("SmokeParticleSystem") as GameObject;
        Instantiate(smokeParticleSystem, this.transform);
        yield return new WaitForSeconds(2.0f);
        shooting = false;
    }
}
