using UnityEngine;

public class CustomFirstPersonController : MonoBehaviour
{
    private Rigidbody rb;
    public Camera playerCamera;

    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 7f;
    public float crouchSpeed = 2.5f;
    public float jumpForce = 5f;

    [Header("Keybindings")]
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Look Settings")]
    public float lookSensitivity = 2f;
    public float maxLookX = 45f;
    public float maxLookY = 45f;

    private float rotX;
    private bool isSprinting = false;
    private bool isCrouched = false;
    private bool FPVLocked = false; // Ajout pour gérer l'état de verrouillage du curseur

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!FPVLocked)
        {
            Move();
        }
        CamLook();
        if (Input.GetKeyDown(jumpKey))
            Jump();
        if (Input.GetKeyDown(crouchKey))
            ToggleCrouch();

        // Basculer l'état du verrouillage du curseur avec la touche "Escape"
        if (Input.GetKeyDown(KeyCode.Escape))
            SwitchCursorLock();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal") * (isCrouched ? crouchSpeed : isSprinting ? sprintSpeed : walkSpeed);
        float z = Input.GetAxis("Vertical") * (isCrouched ? crouchSpeed : isSprinting ? sprintSpeed : walkSpeed);
        Vector3 dir = transform.right * x + transform.forward * z;

        dir.y = rb.velocity.y;
        rb.velocity = dir;

        isSprinting = Input.GetKey(sprintKey) && !isCrouched;
    }

    private void Jump()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, 1.1f))
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void ToggleCrouch()
    {
        isCrouched = !isCrouched;
        transform.localScale = isCrouched ? new Vector3(1, 0.5f, 1) : Vector3.one;
    }

    private void CamLook()
    {
        rotX -= Input.GetAxis("Mouse Y") * lookSensitivity;
        rotX = Mathf.Clamp(rotX, -maxLookY, maxLookY);

        playerCamera.transform.localRotation = Quaternion.Euler(rotX, 0, 0);

        transform.eulerAngles += Vector3.up * Input.GetAxis("Mouse X") * lookSensitivity;
    }

    public void SwitchCursorLock()
    {
        FPVLocked = !FPVLocked;
    }

    public void unlockCursor()
    {
       FPVLocked = false;
    }

    public void lockCursor()
    {
        FPVLocked = true;
    }


}