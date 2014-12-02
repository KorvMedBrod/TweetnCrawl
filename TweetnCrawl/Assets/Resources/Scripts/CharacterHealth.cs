﻿using UnityEngine;
using System.Collections;

public class CharacterHealth : MonoBehaviour {
	
	public int health = 100;
	public Texture2D HB;
	public Transform Player;
	public AudioClip[] list;


	public float x;
	public float y;
	public float w;
	public float h;

	public Material mat;

	
	void OnGUI() {
		if (Event.current.type.Equals(EventType.Repaint)) {
			Rect box = new Rect(x, y, w, h);
			Graphics.DrawTexture(box, HB, mat);
		}
		}
	
	// Use this for initialization
	void Start () {
		
		Player = transform;

	}
	
	// Update is called once per frame
	void Update () {

		float healthy = 1f-(health/100f);
		if (healthy == 0) {
			healthy = 0.1f;
				}
		mat.SetFloat ("_Cutoff", healthy);
	



		if (health <= 0) {
			//no damage method is implemented yet so the enemy cannot die as of now.
			print("Blaaah you killed me!");
			PlayerDeath();
			GameObject.Find("Game_Over_Panel").GetComponent<GameOver>().gameover();
			
		}
		
	}
	
	public void receiveDamage (int dmg) {
		health = health - dmg;
		Debug.Log("Recieved this amount of damage "+dmg.ToString()+" now health="+health.ToString() );
		StartCoroutine(RandomSound());
        StartCoroutine(OnHitEffect());
	}
	
	public void PlayerDeath () {
		
		Vector3 pos = Player.transform.position;
		Instantiate(Resources.Load("BlueExplosion"), pos, Quaternion.identity);
	}

    public IEnumerator OnHitEffect()
    {

        yield return new WaitForSeconds(0.003f);
        renderer.enabled = false;
        yield return new WaitForSeconds(0.003f);
        renderer.material.color = Color.red;
        renderer.enabled = true;
        yield return new WaitForSeconds(0.003f);
        renderer.enabled = false;
        yield return new WaitForSeconds(0.003f);
        renderer.material.color = Color.black;
        renderer.enabled = true;
        yield return new WaitForSeconds(0.003f);
        renderer.material.color = Color.white;

        yield return null;
    }

	protected virtual IEnumerator RandomSound() {
		audio.PlayOneShot(list[Random.Range(0,list.Length)]);

		yield return null;
		
	}

}
