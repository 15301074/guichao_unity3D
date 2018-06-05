using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterAIZZ : MonoBehaviour {

	[Header("Monster Attributes")]
	public int hp;
	public int hpMax = 100;


	[Space(15)]
	[Header("Audio Sources")]
	public AudioSource soundHit;
	public AudioSource soundDie;


	[Space(15)]
	[Header("Tools")]
	public Text hpText;
	public Slider hpSlider;
	public ParticleSystem bloodEFX;
    public string animationType;

	[Space(15)]
	[Header("Attributes")]
	private Animator animator;
	private Collider collider;
	private Collider atkSphereEnemy;


	// Use this for initialization
	void Awake () {
		hp = hpMax;
		animator = GetComponent<Animator> ();
		collider = GetComponent<Collider> ();
		atkSphereEnemy = GetComponentInChildren<SphereCollider> ();
	}

	// Update is called once per frame
	void Update () {
		hp = Mathf.Clamp (hp, 0, hpMax);
		hpText.text = hp + " / " + hpMax;
		hpSlider.value = (float)hp / (float)hpMax;

		if (hp <= 0) {
			creatureDieStart ();
		}
	}

	public void playBloodEFX(){
		bloodEFX.Play ();
	}

	void OnTriggerEnter(Collider collider){

        if (collider.tag == "ATK")
        {
            Vector3 lookCamera = Camera.main.transform.position - transform.position;
            lookCamera.y = 0;
            bloodEFX.transform.rotation = Quaternion.LookRotation(lookCamera);

            playBloodEFX();
            //hp = Mathf.Clamp (hp - 24, 0, hpMax);
            hp = Mathf.Clamp(hp - PlayerController.getAttr(PlayerController.attrType.atk), 0, hpMax);

            animator.SetTrigger("getHit");
            if (hp > 0)
            {
                soundHit.Play();
            }
            else
            {
                soundDie.Play();
            }
        }

        if (collider.tag == "ATKSkill1")
        {
            Vector3 lookCamera = Camera.main.transform.position - transform.position;
            lookCamera.y = 0;
            bloodEFX.transform.rotation = Quaternion.LookRotation(lookCamera);

            playBloodEFX();
            hp = Mathf.Clamp(hp - 30, 0, hpMax);


            animator.SetTrigger("getHit");
            if (hp > 0)
            {
                soundHit.Play();
            }
            else
            {
                soundDie.Play();
            }
        }

        if (collider.tag == "ATKSkill3")
        {
            Vector3 lookCamera = Camera.main.transform.position - transform.position;
            lookCamera.y = 0;
            bloodEFX.transform.rotation = Quaternion.LookRotation(lookCamera);

            playBloodEFX();
            hp = Mathf.Clamp(hp - 60, 0, hpMax);

            animator.SetTrigger("getHit");
            if (hp > 0)
            {
                soundHit.Play();
            }
            else
            {
                soundDie.Play();
            }
        }

        if (collider.tag == "ATKSkill4")
        {
            Vector3 lookCamera = Camera.main.transform.position - transform.position;
            lookCamera.y = 0;
            bloodEFX.transform.rotation = Quaternion.LookRotation(lookCamera);

            playBloodEFX();
            hp = Mathf.Clamp(hp - 80, 0, hpMax);

            animator.SetTrigger("getHit");
            if (hp > 0)
            {
                soundHit.Play();
            }
            else
            {
                soundDie.Play();
            }
        }
    }

	void creatureDieStart(){
        if (animationType == "human")
            animator.SetTrigger("creatureDie");
        else
            this.GetComponent<Animation>().Play("Death1");
		collider.enabled = false;
	}

	public void creatureDieTween(){
		iTweenEvent.GetEvent (gameObject, "creatureDieTween").Play ();
	}

	public void creatureDieEnd(){
		Destroy (gameObject,1.0f);
	}

	public void ATKStartEnemy(){
        atkSphereEnemy.enabled = true;
	}

	public void ATKEndEnemy(){
		atkSphereEnemy.enabled = false;
	}

    public void Doctor()
    {
        hp = hp + 10;
        if(hp>hpMax)
            hp=hpMax;
    }
}
