using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    #region variables
    [Header("Properties player")]
    [SerializeField] private float speedMove = 2f;
    [SerializeField] private float speedRotate = 2f;
    [SerializeField] public bool playerIsEntryShop = false, allowMove, allowGain = false;

    protected float vert;
    protected float horz;
    protected Animator anim;
    protected AudioSource sfxShopCart;
    public GameObject[] products = new GameObject[6];

    public static ControlPlayer Instante;
    #endregion

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sfxShopCart = transform.Find("sound_shopping cart").GetComponent<AudioSource>();
        Instante = this;
    }

    void Update()
    {
        vert = Input.GetAxis("Vertical");
        horz = Input.GetAxis("Horizontal");

        if(allowMove)
            AnimationController();
    }

    private void FixedUpdate()
    {
        if(allowMove)
            Move();
    }

    private void Move()
    {
        if (vert != 0 || horz != 0)
        {
            //move forward and back
            if (vert > 0)
            {
                if(!Input.GetKey(KeyCode.LeftShift))
                    transform.Translate(Vector3.forward * speedMove * Time.fixedDeltaTime);
                else
                    transform.Translate(Vector3.forward * (speedMove + 2) * Time.fixedDeltaTime);

                //rotate player left and rigth - normal
                transform.Rotate(0f, horz * speedRotate * Time.fixedDeltaTime, 0f);
            }
            else if (vert < 0)
            {
                transform.Translate(Vector3.back * speedMove * Time.fixedDeltaTime);

                //rotate player left and rigth - inverted
                transform.Rotate(0f, -horz * speedRotate * Time.fixedDeltaTime, 0f);               
            }         
        }
    }

    private void AnimationController()
    {
        if (vert != 0)
        {
            if (vert > 0)
            {
                anim.SetInteger("action", 1);
                bool veryFast = Input.GetKey(KeyCode.LeftShift) ? true : false;

                if (veryFast)
                    anim.speed = 1.6f;
                else
                    anim.speed = 1f;
            }
            else if (vert < 0)
                anim.SetInteger("action", 2);
            sfxShopCart.mute = false;
        }
        else
        {
            anim.SetInteger("action", 0);
            sfxShopCart.mute = true;
        }
    }

    public void SetAnimationAfterLosing()
    {
        anim.SetInteger("action", 3);
        ManagerInteract.Instance.cam.gameObject.SetActive(false);
        ManagerInteract.Instance.camLose.gameObject.SetActive(true);
    }

    public IEnumerator GainProdcuct(Product product)
    {
        if (GameManager.Instance.numberOfProductCart < 6)
        {           
            if (product.listOfProducts.Count > 0)
            {
                allowMove = false;
                ManagerInteract.Instance.allowInteract = false;
                anim.SetInteger("action", 4);

                GameManager.Instance.overallPrice += product.price;
                product.GainProduct();
                yield return new WaitForSecondsRealtime(2f);

                allowMove = true;
                ManagerInteract.Instance.allowInteract = true;
                anim.SetInteger("action", 0);
                GameManager.Instance.AddProductShoppingCart();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "door")
        {
            playerIsEntryShop = true;
            if (other.transform.TryGetComponent(out DoorController door))
                StartCoroutine(door.OpenDoor(true));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "door")
            playerIsEntryShop = false;
    }
}
