using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    [SerializeField] private Transform hand;
    private GameObject item;
    private Rigidbody itemRb;
    [SerializeField] private bool inputTriggered;
    [SerializeField] private bool inputEnabled;
    public PlayerController playerController;

    [Header("Physics Settings")]
    [SerializeField] private float pickupRange = 50.0f;
    [SerializeField] private float pickupForce = 150.0f;

    private void Start()
    {
        if (inputEnabled)
        {
            playerController.pickupControl.action.Enable();
        }
        else
        {
            playerController.pickupControl.action.Disable();
        }
    }

    private void Update()
    {
        if (playerController.pickupControl.action.triggered)
        {
            inputTriggered = !inputTriggered;
            if (inputTriggered)
            {
                if (item == null)
                {
                    // Perform a raycast from the player's position in the direction they are facing
                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit, pickupRange))
                    {
                        Debug.Log("Raycast hit: " + hit.transform.name); // Add this line

                        // If the raycast hits an object that can be picked up, pick up that object
                        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("ObjectPickup"))
                        {
                            Debug.Log("Raycast hit an object on the Pickupable layer: " + hit.transform.name); // Add this line
                            PickupObject(hit.transform.gameObject);
                        }
                    }
                    else
                    {
                        Debug.Log("Raycast did not hit any object"); // Add this line
                    }
                }
            }
            else
            {
                if (item != null)
                {
                    DropObject();
                }
            }
        }

        if (item != null)
        {
            MoveObject();
        }
    }

    private void MoveObject()
    {
        // if (Vector3.Distance(item.transform.position, hand.position) > 0.1f)
        // {
        //     Vector3 moveDirection = (hand.position - item.transform.position);
        //     itemRb.AddForce(moveDirection * pickupForce * Time.deltaTime);
        // }
        if (item != null)
        {
            float speed = 10f;
            item.transform.position = Vector3.Lerp(item.transform.position, hand.position, Time.deltaTime * speed);
        }
    }

    private void PickupObject(GameObject obj)
    {
        if (obj.GetComponent<Rigidbody>())
        {
            item = obj;
            item.transform.parent = hand;
            itemRb = obj.GetComponent<Rigidbody>();
            itemRb.useGravity = false;
            itemRb.drag = 10;
            itemRb.constraints = RigidbodyConstraints.FreezeRotation;
            itemRb.transform.parent = hand;
            item.transform.position = hand.position;
            item.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void DropObject()
    {
        item.transform.GetChild(0).gameObject.SetActive(true);
        item.transform.parent = null;
        itemRb.useGravity = true;
        itemRb.drag = 1;
        itemRb.constraints = RigidbodyConstraints.None;
        itemRb.transform.parent = null;
        item = null;
        itemRb = null;
    }
    
    public void SetGameObject(GameObject obj)
    {
        item = obj;
    }
    public void UnsetGameObject()
    {
        item = null;
    }

    public void OnEnable()
    {
        playerController.pickupControl.action.Enable();
        inputEnabled = true;
    }

    public void OnDisable()
    {
        playerController.pickupControl.action.Disable();
        inputEnabled = false;
    }
}