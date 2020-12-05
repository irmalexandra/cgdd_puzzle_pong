using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

public class BallController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D body;
    public Vector2 direction;
    public float speed;
    public float currentMagnitude;
    public Vector2 levelBounds;
    public Vector2 startPosition;

    private Vector3 nullVect = Vector3.zero;
    
    public float nudgePower;
    public int nudgeStaminaCost;

    private bool _thrustOnCooldown;
    
    public Light2D pointLight;
    public Light2D paraLight;
    private Color originalColor;
    void Start()
    {
        startPosition = transform.position;
        body.velocity = direction.normalized * speed;
        originalColor = paraLight.color;
    }

    // Update is called once per frame
    private void Update()
    {
        if (StaminaBar.instance.GetStamPercentage() == 0f)
        {
            Debug.Log("oncooldown = true");
            _thrustOnCooldown = true;
        }
        else if (StaminaBar.instance.GetStamPercentage() >= 0.99f)
        {
            Debug.Log("oncooldown = false");
            _thrustOnCooldown = false;
        }
        ChangeLights();
        
        if (!GameManager.Instance.IsPaused())
        {
            ProcessInputs();
        }
        
        currentMagnitude = body.velocity.magnitude;
        if (!(transform.position.x < -levelBounds.x) && !(transform.position.x > levelBounds.x) &&
            !(transform.position.y < -levelBounds.y) && !(transform.position.y > levelBounds.y)) return;
        if (GameManager.Instance.extraBalls > 0)
            GameManager.Instance.extraBalls--;
        if (GameManager.Instance.extraBalls != 0)
        {
            transform.position = startPosition;
            body.velocity = direction.normalized * speed;
        }
        

        if (GameManager.Instance.extraBalls == 0)
        {
            GameManager.Instance.TriggerGameOverMenu();
            
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 reDirection = GetComponent<Rigidbody2D>().velocity;
        

        if (other.gameObject.CompareTag("Paddle"))
        {
            SoundManagerScript.PlaySoundEffect("PaddleHit");
        }
        else
        {
            SoundManagerScript.PlaySoundEffect("BallHit");
            
            if (other.gameObject.CompareTag("ButtonFace"))
            {
                Vector3 otherBoost = other.transform.up * 2f;
                reDirection += new Vector2(otherBoost.x, otherBoost.y);
            }
        }

        reDirection = reDirection.normalized;
        reDirection *= speed;
        GetComponent<Rigidbody2D>().velocity = reDirection;
    }
    
    private void ProcessInputs()
    {
        /*if (Input.GetButton("Jump"))
        {
            
            if (StaminaBar.instance.UseStamina(nudgeStaminaCost))
            {
                Nudge();
            }
        }*/
        if (_thrustOnCooldown)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                SoundManagerScript.PlaySoundEffect("Cooldown");
            }
        }
        else
        {
            if (Input.GetButton("Fire1"))
            {
                if (StaminaBar.instance.UseStamina(1))
                {
                    Thrust();
                }
            }

            if (Input.GetButtonUp("Fire1"))
            {
                _thrustOnCooldown = true;
            }
        }
        
        
    }

    private void Thrust()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 fromMouseToBall = mousePos - new Vector2(transform.position.x, transform.position.y);
        
        var newDirection = Vector2.LerpUnclamped(body.velocity.normalized, fromMouseToBall.normalized, 0.0115f);

        body.velocity = newDirection * speed;

    }

    private  void Nudge()
    {
        Debug.Log("nudge nudge");

        var nudgeDirection = body.velocity;
        
        var above = transform.position.y <= Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        var right = transform.position.x <= Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        
        var goingHorizontal = Mathf.Abs(body.velocity.x) > Mathf.Abs(body.velocity.y);

        if (goingHorizontal)
        {
            if (above)
            {
                nudgeDirection.y += (nudgePower);
            }
            else
            {
                nudgeDirection.y -= nudgePower;
            }
        }
        else
        {
            if (right)
            {
                nudgeDirection.x += nudgePower;
            }
            else
            {
                nudgeDirection.x -= nudgePower;
            }
        }
        body.velocity = nudgeDirection;
    }

    private void ChangeLights()
    {
        pointLight.intensity = 1 * StaminaBar.instance.GetStamPercentage();
        Debug.Log(pointLight.intensity);
        Debug.Log("stam percentage:" +StaminaBar.instance.GetStamPercentage());
        if (StaminaBar.instance.GetStamPercentage() <= 0.99f)
        {
            pointLight.color = Color.LerpUnclamped(Color.red, originalColor , 1 * StaminaBar.instance.GetStamPercentage());
            paraLight.color = Color.LerpUnclamped(Color.red, originalColor, 1 * StaminaBar.instance.GetStamPercentage());
        }
        else
        {
            pointLight.color = originalColor;
            paraLight.color = originalColor;
        }
    }
}
