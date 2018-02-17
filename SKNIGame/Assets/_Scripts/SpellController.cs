using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(GestureController))]
public class SpellController : MonoBehaviour {

    private GestureController gestureController;

    public List<SpellBall> spells = new List<SpellBall>();

    [Header("Rotation")]
    [Range(1f, 10f)]
    public float rotateSpeed = 5f;

    [Header("Shooting")]
    public float shootingRate = 1f;
    private float timer = 1;

    private void Start()
    {
        gestureController = GetComponent<GestureController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //check if objects arround controller are spells
        if (other.GetComponent<SpellBall>() != null)
        {
            //add spell to hand
            spells.Add(other.GetComponent<SpellBall>());
            other.transform.SetParent(this.transform);
        }
    }

    private void OnMouseDrag()
    {
        if (gestureController.m_ControlType == GestureController.ControlType.Mouse)
        {
            DragSpell();
        } else
        {
            return;
        }
    }

    //set controller position to mouse positon
    private void DragSpell()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float dist = 10f;
        this.transform.position = ray.GetPoint(dist);
    }
    
    private void Update()
    {
        //Rotate all speels around X and Y axis (cos and sin for fun)
        this.transform.Rotate(new Vector3(rotateSpeed*Mathf.Cos(Time.time),  -rotateSpeed*Mathf.Sin(Time.time), 0));
        if (gestureController.m_ControlType == GestureController.ControlType.Mouse)
        {
            if (Input.GetMouseButton(1))
            {
                Shot();
            }
            if(timer > 0)
            {
                timer -= Time.deltaTime;
            }
        }
    }

    private void Shot()
    {
        if (timer <= 0 && spells.Count > 0)
        {

            //set force (get force from spell library)
            float force = 1000f;

            if (gestureController.m_ControlType == GestureController.ControlType.Mouse)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                SpellBall spell = spells[0];
                spells.RemoveAt(0);
                spell.transform.parent = null;
                spell.GetComponent<Rigidbody>().AddForce(ray.GetPoint(force));

            } else if(gestureController.m_ControlType == GestureController.ControlType.ViveController)
            {
                //TODO shooting with vive
            }
            timer = 1 / shootingRate;
        }

    }
}
