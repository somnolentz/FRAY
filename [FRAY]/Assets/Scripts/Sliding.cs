using UnityEngine;

public class Sliding : MonoBehaviour
{
    public float slideSpeed = 10f;
    public float slideDuration = 2f;
    public float slopeAngleThreshold = 30f;
    public Collider slopeCollider; // Reference to the specific collider for slope detection

    private Rigidbody rb;
    private bool isSliding = false;
    private float slideTimer = 0f;

    public string slideInputAxis = "Slide";

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (DetectQualifyingSlope(slopeCollider) && !isSliding)
        {
            Debug.Log("Sliding");
            StartSliding();
        }
    }

   

    private Vector3 GetSlideDirection()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            // Calculate the slide direction as the projection of the input direction onto the slope plane
            Vector3 slideDirection = Vector3.ProjectOnPlane(transform.forward, hit.normal).normalized;
            return slideDirection;
        }
        else
        {
            return transform.forward;
        }
    }




    private bool DetectQualifyingSlope(Collider collider)
    {
        RaycastHit hit;
        if (Physics.Raycast(collider.bounds.center, Vector3.down, out hit, collider.bounds.extents.y + 0.1f))
        {
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
            if (slopeAngle >= slopeAngleThreshold)
            {
                return true;
            }
        }
        return false;
    }

    private void StartSliding()
    {
        isSliding = true;
        slideTimer = 0f;
        // Apply any additional effects or animations for sliding
    }

    private void StopSliding()
    {
        isSliding = false;
        // Reset the character's velocity or apply any desired behavior after sliding
    }
}
