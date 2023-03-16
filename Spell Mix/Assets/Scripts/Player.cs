using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Player : MonoBehaviour
{   
    public Spawner spawner;
    public Combine combined;
    public Button shootButton;
    public Animator animator;
    public GameObject target;

    private Rigidbody finalSpellRigidbody;

    public bool isShooting;

    private void FixedUpdate()
    {
        

    }

    private void Update()
    {

        if(combined.isReadyToShoot == true)
        {
            shootButton.interactable = true;
        }
        else
        {
            shootButton.interactable = false;
        }

        if(isShooting == true && finalSpellRigidbody != null)
        {
            finalSpellRigidbody.velocity = transform.forward * combined.finalSpellCast.speed;

            //    Vector3 forwardPos = new Vector3();
            //    Vector3 aimCenterPos = new Vector3();
            //    finalSpellRigidbody.position = Vector3.MoveTowards(finalSpellRigidbody.position, target.transform.position, combined.finalSpellCast.speed * Time.deltaTime);

            //finalSpellRigidbody.transform.forward = target.transform.position - finalSpellRigidbody.transform.position;
            //finalSpellRigidbody.position += target.transform.position * combined.finalSpellCast.speed * Time.deltaTime;

            var heading = target.transform.position - transform.position;

            var rotation = Quaternion.LookRotation(heading);
            finalSpellRigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, 160 * Time.deltaTime));

        }
    }
    public void Shoot()
    {
        finalSpellRigidbody = combined.finalSpellCast.GetComponent<Rigidbody>();
        combined.finalSpellCast.transform.parent = null;

        animator.SetBool("ReadyToShoot", false);

        spawner.LisEmpty = true;
        spawner.RisEmpty = true;
        combined.isReadyToShoot = false;

        finalSpellRigidbody.isKinematic = false;
        //finalSpellRigidbody.AddForce(transform.forward * 1000 * combined.finalSpellCast.speed);

        isShooting = true;
    }

}
