using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance; // ��������� ����������� ���������� instance

    public float moveSpeed, gravityForce, jumpForce, sprintSpeed; // ��������� ���������� sprintSpeed
    public CharacterController characterController;

    private Vector3 moveInput;

    public Transform cameraTransform;

    public float mouseSensitivity;

    private bool canJump; // ��������� ���������� canJump
    public Transform groundCheckPoint; // ��������� ���������� groundCheckPoint
    public LayerMask ground; // ��������� ���������� ground

    public Animator animator; // ��������� ���������� animator

    //public GameObject bullet;
    public Transform firePoint;

    public Gun activeGun;
    public List<Gun> allGuns = new List<Gun>();
    public int currentGun;

    public GameObject muzzleFlash;


    private void Awake() // �������� ����� Awake
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentGun--;
        SwitchGun();
    }

    // Update is called once per frame
    void Update()
    {
        if (!UI.instance.pauseScreen.activeInHierarchy)
        {


            float yVelocity = moveInput.y;

            // �������� ������
            Vector3 verticalMove = transform.forward * Input.GetAxis("Vertical");
            Vector3 horizontalMove = transform.right * Input.GetAxis("Horizontal");

            moveInput = horizontalMove + verticalMove;
            moveInput.Normalize();

            if (Input.GetKey(KeyCode.LeftShift)) // ��������� ������ �������
            {
                moveInput *= sprintSpeed;
            }
            else
            {
                moveInput *= moveSpeed;
            }

            moveInput.y = yVelocity;

            moveInput.y += Physics.gravity.y * gravityForce * Time.deltaTime;
            // Fall character
            if (characterController.isGrounded)
            {
                moveInput.y = Physics.gravity.y * gravityForce * Time.deltaTime;
            }

            canJump = Physics.OverlapSphere(groundCheckPoint.position, 0.2f, ground).Length > 0;

            //jump character
            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                moveInput.y = jumpForce;
            }

            characterController.Move(moveInput * Time.deltaTime);

            // �������� ������
            Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

            // �������� ������ �� �����������
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

            // �������� ������ �� ���������
            cameraTransform.rotation = Quaternion.Euler(cameraTransform.rotation.eulerAngles.x - mouseInput.y, cameraTransform.rotation.eulerAngles.y, cameraTransform.rotation.eulerAngles.z);

            // Shooting
            if (Input.GetMouseButtonDown(0) && activeGun.fireCounter <= 0)
            {
                RaycastHit hit;
                if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 200f))
                {
                    if (Vector3.Distance(cameraTransform.position, hit.point) > 1f)
                    {
                        firePoint.LookAt(hit.point);
                    }
                    else
                    {
                        firePoint.LookAt(cameraTransform.position + (cameraTransform.forward * 40f));
                    }
                }
                //Instantiate(bullet, firePoint.position, firePoint.rotation);
                FireShoot();
            }

            if (Input.GetMouseButton(0) && activeGun.canAutoFire)
            {
                if (activeGun.fireCounter <= 0)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 200f))
                    {
                        if (Vector3.Distance(cameraTransform.position, hit.point) > 1f)
                        {
                            firePoint.LookAt(hit.point);
                        }
                        else
                        {
                            firePoint.LookAt(cameraTransform.position + (cameraTransform.forward * 40f));
                        }
                    }

                    FireShoot();
                }
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                SwitchGun();
            }

            animator.SetFloat("moveSpeed", moveInput.magnitude); // ��������� �������� �������� � ��������
            animator.SetBool("onGround", canJump);
        }

    }

    public void FireShoot()
    {
        if (activeGun.currentAmmunition > 0)
        {
            activeGun.currentAmmunition--;

            Instantiate(activeGun.bullet, firePoint.position, firePoint.rotation);

            activeGun.fireCounter = activeGun.fireRate;

            UI.instance.ammunitionText.text = "" + activeGun.currentAmmunition;

            StartCoroutine(WaitAndSetActiveFalse());
        }
    }

    IEnumerator WaitAndSetActiveFalse()
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        muzzleFlash.SetActive(false);
    }

    public void SwitchGun()
    {
        activeGun.gameObject.SetActive(false);

        currentGun++;

        if (currentGun >= allGuns.Count) 
        {
            currentGun = 0;
        }

        activeGun = allGuns[currentGun];
        activeGun.gameObject.SetActive(true);

        UI.instance.ammunitionText.text = "" + activeGun.currentAmmunition;

        firePoint.position = activeGun.firepoint.position;
    }
}