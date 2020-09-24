using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///I got help with the line render and planet orbits from this tutorial
///https://www.youtube.com/watch?v=mQKGRoV_jBc
///
public class OrbitMotion : MonoBehaviour
{

    public Transform orbitingObject;
    public Orbit orbitPath;

    [Range(0f,1f)]
    public float orbitProgress =0f;
    public float orbitTime =3f;

    public bool orbitActive = true;

    void Start()
    {
        if(orbitingObject == null)
        {
            orbitActive = false;
            return;
        }
        SetOrbitPos();
        StartCoroutine(OrbitAnimation());
    }

    private void FixedUpdate()
    {
        
    }

    void SetOrbitPos()
    {
        Vector2 orbitPos = orbitPath.Evalute(orbitProgress);
        float starY = transform.parent.GetChild(0).GetComponent<Transform>().position.y;
        orbitingObject.localPosition = new Vector3(orbitPos.x, starY, orbitPos.y);
    }

   public IEnumerator OrbitAnimation()
    {
        if(orbitTime < 0.1f)
        {
            orbitTime = 0.1f;
        }
        float orbitSpeed = 1f / orbitTime;
        while (orbitActive)
        {
            orbitProgress += Time.deltaTime * orbitSpeed;
            orbitProgress %= 1f;
            SetOrbitPos();
            yield return null;
        }
    }

   
}
