using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public float currentSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float default_walkSpeed;
    public float default_sprintSpeed;
    public float mouseSensitivity;
    public float timeToCrazy;
    public float energyTime;
    public bool rap;
    public bool outside;
    public bool mainGame;
    public bool spooky;
    public bool crazy;
    public bool guilty;
    public bool detention;
    public bool energetic;
    public bool squished;
    public string guilt;
    public Camera Camera;
    private CharacterController controller;
    public Slider stamina;
    public GameObject rest;
    public Vector3 stampedeSquishPos;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        GenericManagers.GUI.CursorManager.Lock();
    }

    private void Update()
    {
        mouseSensitivity = PlayerPrefs.GetFloat("mouseSensitivity");

        if (Time.timeScale > 0f & !squished)
        {
            Move();
            Look();

            if (Input.GetKey(KeyCode.Space))
            {
                Camera.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }

            else
            {
                Camera.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) & controller.velocity != Vector3.zero)
        {
            if (mainGame)
            {
                Sprint();
            }
        }

        else
        {
            currentSpeed = walkSpeed;
            if (mainGame)
            {
                if (!outside)
                {
                    if (controller.velocity == Vector3.zero)
                    {
                        stamina.value += 10 * Time.deltaTime;
                    }
                }

                else
                {
                    stamina.value += 20 * Time.deltaTime;
                }
            }
        }

        if (mainGame)
        {
            if (timeToCrazy > 0f & spooky)
            {
                timeToCrazy -= Time.deltaTime * 1f;
            }

            if (Input.GetKey(KeyCode.C) & PlayerPrefs.GetInt("debug") == 1)
            {
                timeToCrazy = 0f;
            }

            if (stamina.value == 0)
            {
                rest.SetActive(true);
            }

            else
            {
                rest.SetActive(false);
            }
        }

        if (timeToCrazy < 0f)
        {
            crazy = true;
        }

        else
        {
            crazy = false;
        }

        if (energyTime > 0f & energetic & controller.velocity != Vector3.zero)
        {
            energyTime -= Time.deltaTime;
        }

        if (energyTime < 0f)
        {
            energetic = false;
        }

        if (energetic)
        {
            if (walkSpeed == default_walkSpeed)
            {
                walkSpeed *= 2f;
            }

            if (sprintSpeed == default_sprintSpeed)
            {
                sprintSpeed *= 2f;
            }
        }

        else
        {
            walkSpeed = default_walkSpeed;
            sprintSpeed = default_sprintSpeed;
        }
    }

    private void LateUpdate()
    {
        if (squished)
        {
            transform.position = stampedeSquishPos;
            transform.localRotation = Quaternion.Euler(-90, 0, 0);
            Camera.transform.position = transform.position;
        }
    }

    private void Move()
    {
        if (rap == false)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * currentSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        }
    }

    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);
    }

    private void Sprint()
    {
        if (stamina.value > 0)
        {
            currentSpeed = sprintSpeed;

            if (!outside)
            {
                stamina.value -= 20 * Time.deltaTime;

                if (!detention)
                {
                    guilt = "run";
                }
            }
        }

        else
        {
            currentSpeed = walkSpeed;
            guilt = null;
        }
    }
}
