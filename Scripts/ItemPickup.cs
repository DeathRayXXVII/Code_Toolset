using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    public InputActionReference input;
    [SerializeField]
    private Transform hand;
    private GameObject item;
    private Rigidbody itemRb;
    private bool inputTriggered;
    [SerializeField]
    private bool inputEnabled;
    
    [Header("Physics Settings")]
    [SerializeField] private float pickupRange = 5.0f;
    [SerializeField] private float pickupForce = 150.0f;

    private void Start()
    {
        if (input == null)
        {
            return;
        }
        if (inputEnabled)
        {
            input.action.Enable();
        }
        else
        {
            input.action.Disable();
        }
    }
    private void Update()
    {
        if (input.action.triggered)
        {
            inputTriggered = !inputTriggered;
            if (inputTriggered)
            {
                if (item == null)
                {
                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit, pickupRange))
                    {
                        PickupObject(hit.transform.gameObject);
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
        if (Vector3.Distance(item.transform.position, hand.position) > 0.1f)
        {
            Vector3 moveDirection = (hand.position - item.transform.position);
            itemRb.AddForce(moveDirection * pickupForce * Time.deltaTime);
        }
    }
    
    private void PickupObject(GameObject obj)
    {
        if (obj.GetComponent<Rigidbody>())
        {
            item = obj;
            itemRb = obj.GetComponent<Rigidbody>();
            itemRb.useGravity = false;
            //itemRb.isKinematic = true;
            itemRb.drag = 10;
            itemRb.constraints = RigidbodyConstraints.FreezeRotation;
            itemRb.transform.parent = hand;
            item.transform.position = hand.position;
        }
    }
    private void DropObject()
    {
        itemRb.useGravity = true;
        //itemRb.isKinematic = true;
        itemRb.drag = 1;
        itemRb.constraints = RigidbodyConstraints.None;
        itemRb.transform.parent = null;
        item = null;
        itemRb = null;
        
    }
    
    public void OnEnable()
    {
        if (input == null)
        {
            return;
        }
        input.action.Enable();
    }
    
    public void OnDisable()
    {
        if (input == null)
        {
            return;
        }
        input.action.Disable();
    }
}
