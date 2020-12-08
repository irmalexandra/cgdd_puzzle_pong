using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class BallController : MonoBehaviour
{
    public Rigidbody2D body;
    public Vector2 direction;
    public float speed;
    public float currentMagnitude;
    public Vector2 levelBounds;
    public Vector2 startPosition;
    private float defaultTrailTime = 0.2f;

    private bool _thrustOnCooldown;
    private bool _thrustSoundCD;
    private float _thrustStaminaCost = 0.0115f;

    public TrailRenderer trail;
    /*public float nudgePower;
    public int nudgeStaminaCost;*/

    public float thrustPower = 0.0115f;
    public float thrustStaminaCost = 1f;

    public Light2D pointLight;
    public Light2D paraLight;
    private Color originalColor;

    private float _horizontal;
    private float _vertical;
    private bool _flashGreen;
    
    void Start()
    {
        trail = GetComponent<TrailRenderer>();
        trail.time = 0;
        startPosition = transform.position;
        body.velocity = direction.normalized * speed;
        originalColor = paraLight.color;
    }

    private void Update()
    {
        CooldownTrigger();
        ChangeLights();
        

        if (!GameManager.Instance.IsPaused())
        {
            ProcessInputs();
        }
        BoundCheck();
        if (GameManager.Instance.extraBalls == 0)
        {
            GameManager.Instance.TriggerGameOverMenu();

        }
    }

    private void BoundCheck()
    {
        currentMagnitude = body.velocity.magnitude;
        if (!(transform.position.x < -levelBounds.x) && !(transform.position.x > levelBounds.x) &&
            !(transform.position.y < -levelBounds.y) && !(transform.position.y > levelBounds.y)) return;
        if (GameManager.Instance.extraBalls > 0)
        {
            GameManager.Instance.extraBalls--;
            BallCounter.loseLife(GameManager.Instance.extraBalls);
        }

        if (GameManager.Instance.extraBalls == 0) return;
        transform.position = startPosition;
        body.velocity = direction.normalized * speed;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 reDirection = GetComponent<Rigidbody2D>().velocity;


        if (other.gameObject.CompareTag("Paddle"))
        {
            SoundManager.PlaySoundEffect("PaddleHit");
        }
        else
        {
            SoundManager.PlaySoundEffect("BallHit");

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
        /*_horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");*/
        /*if (Input.GetButton("Jump"))
        {
            
            if (StaminaBar.instance.UseStamina(nudgeStaminaCost))
            {
                Nudge();
            }
        }*/
        _horizontal = 0;
        _vertical = 0;
        if (Input.GetKey(KeyCode.A))
        {
            _horizontal = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _horizontal = 1;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            _vertical = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _vertical = -1;
        }
        if (_thrustOnCooldown)
        {
            trail.time -= 0.005f;
            if (Input.GetKeyDown(KeyCode.A) 
                || Input.GetKeyDown(KeyCode.S) 
                || Input.GetKeyDown(KeyCode.W) 
                || Input.GetKeyDown(KeyCode.D))
            {
                SoundManager.PlaySoundEffect("Cooldown");
            }
        }
        else
        {
            if (_vertical == 0 && _horizontal == 0)
            {
                trail.time -= 0.005f;
                return;
            }
                
            if (!StaminaBar.instance.UseStamina(thrustStaminaCost)) return;
            trail.time = defaultTrailTime;
            Thrust2();

            if (_vertical == 0 && _horizontal == 0)
            {
                trail.time -= 0.005f;
            }
        }
    }

    private void Thrust2()
    {
        var bodyVelocity = body.velocity;
        Vector2 position = transform.position;
        
        Vector2 target = new Vector2(position.x+_horizontal, position.y+_vertical);
        
        var newDirection = Vector2.LerpUnclamped(bodyVelocity.normalized, (target-position).normalized, _thrustStaminaCost);

        body.velocity = newDirection*speed;
    }

    private void Thrust()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 fromMouseToBall = mousePos - new Vector2(transform.position.x, transform.position.y);

        var newDirection = Vector2.LerpUnclamped(body.velocity.normalized, fromMouseToBall.normalized, _thrustStaminaCost);

        body.velocity = newDirection * speed;

    }

    private void CooldownTrigger()
    {
        if (StaminaBar.instance.GetStamPercentage() <= 0f)
        {
            _thrustOnCooldown = true;
        }
        else if (StaminaBar.instance.GetStamPercentage() <= 0.99f)
        {
            if (_flashGreen) return;
            _flashGreen = true;

        }
        else if (StaminaBar.instance.GetStamPercentage() >= 0.99f)
        {
            if (!_thrustOnCooldown) return;
            _thrustOnCooldown = false;
            _flashGreen = true;
        }
    }

    private void ChangeLights()
    {
        pointLight.intensity = 1 * StaminaBar.instance.GetStamPercentage();
        if (StaminaBar.instance.GetStamPercentage() < 1f)
        {
            pointLight.color =
                Color.LerpUnclamped(Color.red, originalColor, 1 * StaminaBar.instance.GetStamPercentage());
            paraLight.color =
                Color.LerpUnclamped(Color.red, originalColor, 1 * StaminaBar.instance.GetStamPercentage());

            trail.startColor = pointLight.color;
        }
        else if (_flashGreen)
        {
            StartCoroutine(ColorStatusUpdate());
            _flashGreen = false;
        }
        
        
    }


    private IEnumerator ColorStatusUpdate()
    {
        ChangeLightsColor(Color.green);
        SoundManager.PlaySoundEffect("ThrustReady");
        yield return new WaitForSeconds(0.3f);
        ChangeLightsColor(originalColor);
    }
    
    private void ChangeLightsColor(Color color)
    {
        pointLight.color = color;
        paraLight.color = color;
    }
}

/*private  void Nudge()
{

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
}*/