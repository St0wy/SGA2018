using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float Vertical;
    private float JoystickVertical;
    private float VerticalFire;
    private float Horizontal;
    private float JoystickHorizontal;
    private float HorizontalFire;
    private float JoystickVerticalFire;
    private float JoystickHorizontalFire;
    private float Special;
    private float JoystickSpecial;
    [SerializeField]
    private float shotPerSecond;
    private float rateOfFire;
    private float nextFireAllowed;

    public GameObject projectileNormal;

    public GameObject projectileSpecial;

    public GameObject head;
    public Sprite backHead;
    public Sprite frontHead;

    private float mana = 0f;

    private bool canFire = false;

    public int range = 3;

    private const int FACTOR = 9;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        rateOfFire = 1 / shotPerSecond;
        Vertical = Input.GetAxis("Vertical");
        VerticalFire = Input.GetAxis("VerticalFire");
        JoystickVertical = Input.GetAxis("JoystickVertical");
        JoystickVerticalFire = Input.GetAxis("JoystickVerticalFire");
        Horizontal = Input.GetAxis("Horizontal");
        HorizontalFire = Input.GetAxis("HorizontalFire");
        JoystickHorizontal = Input.GetAxis("JoystickHorizontal");
        JoystickHorizontalFire = Input.GetAxis("JoystickHorizontalFire");
        Special = Input.GetAxis("Special");
        JoystickSpecial = Input.GetAxis("JoystickSpecial");

        GetComponent<Rigidbody2D>().velocity = new Vector3(((Horizontal > 0 || Horizontal < 0) ? Horizontal : JoystickHorizontal) * FACTOR, ((Vertical > 0 || Vertical < 0) ? Vertical : JoystickVertical) * FACTOR, 0);

        if (Vertical > 0 || VerticalFire > 0 || JoystickVerticalFire < 0 ||  JoystickVertical < 0)
        {
            head.GetComponent<SpriteRenderer>().sprite = backHead;
        }
        else
        {
            head.GetComponent<SpriteRenderer>().sprite = frontHead;
        }

        mana += Time.deltaTime * 10;
        if (mana > 100)
            mana = 100;
        if (mana < 0)
            mana = 0;

        //special
        if ((Special > 0 || JoystickSpecial > 0) && mana >= 100)
        {
            mana = 0;

            GameObject projectil = Instantiate(projectileSpecial, transform.position, Quaternion.identity);

            if (VerticalFire > 0 || JoystickVerticalFire < 0)
            {

                projectil.GetComponent<Projectile>().direction = Vector2.up;
                projectil.GetComponent<Projectile>().range = range;
            }
            //down
            else if (VerticalFire < 0 || JoystickVerticalFire > 0)
            {
                projectil.GetComponent<Projectile>().direction = Vector2.down;
                projectil.GetComponent<Projectile>().range = range;
            }
            //right
            else if (HorizontalFire > 0 || JoystickHorizontalFire > 0)
            {
                projectil.GetComponent<Projectile>().direction = Vector2.right;
                projectil.GetComponent<Projectile>().range = range;
            }
            //left
            else if (HorizontalFire < 0 || JoystickHorizontalFire < 0)
            {
                projectil.GetComponent<Projectile>().direction = Vector2.left;
                projectil.GetComponent<Projectile>().range = range;
            }
            else
            {
                projectil.GetComponent<Projectile>().direction = Vector2.down;
            }
        }

        //special

        canFire = false;
        if (Time.time >= nextFireAllowed)
        {
            nextFireAllowed = Time.time + rateOfFire;
            canFire = true;
        }
        if (canFire)
        {

            if ((VerticalFire == 0 && HorizontalFire == 0) && (JoystickVerticalFire == 0 && JoystickHorizontalFire == 0))
                return;

            Debug.Log(JoystickVerticalFire + "----" + JoystickHorizontalFire + "----" + JoystickSpecial);

            GameObject projectil = Instantiate(projectileNormal, transform.position, Quaternion.identity);

            //up
            if (VerticalFire > 0 || JoystickVerticalFire < 0 && Special == 0)
            {
                projectil.GetComponent<Projectile>().direction = Vector2.up * new Vector2(0, 1.4f);
                projectil.GetComponent<Projectile>().range = range;
            }
            //down
            else if (VerticalFire < 0 || JoystickVerticalFire > 0 && Special == 0)
            {
                projectil.GetComponent<Projectile>().direction = Vector2.down * new Vector2(0, 1.4f);
                projectil.GetComponent<Projectile>().range = range;
            }
            //right
            else if (HorizontalFire > 0 || JoystickHorizontalFire > 0 && Special == 0)
            {
                projectil.GetComponent<Projectile>().direction = Vector2.right * new Vector2(1.4f, 0);
                projectil.GetComponent<Projectile>().range = range;
            }
            //left
            else if (HorizontalFire < 0 || JoystickHorizontalFire < 0 && Special == 0)
            {
                projectil.GetComponent<Projectile>().direction = Vector2.left * new Vector2(1.4f, 0);
                projectil.GetComponent<Projectile>().range = range;
            }
        }
    }
}
