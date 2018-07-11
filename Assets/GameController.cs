using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour {

	// Use this for initialization
	public TextMesh InfoText;
	public GameObject ball;
	public Player player;
	public cup[] cups;

	private float resetTimer = 5f;
	

	void Start () {
		InfoText.text = "Pick the correct one!";
		StartCoroutine(SuffleRoutine());
	}
	
	// Update is called once per frame
	void Update () {
		if(player.picked){
			if(player.won){
				InfoText.text = "You Won! Yeppy";
			}else{
				InfoText.text = "Ooops! wrong one, try again.";
			}

			resetTimer -= Time.deltaTime;
			//InfoText.text = Math.Round(resetTimer,1).ToString();
			if(resetTimer <= 0f){
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
		}
	}

	private IEnumerator SuffleRoutine(){
		yield return new WaitForSeconds(1f);

		foreach(cup cup in cups){
			cup.MoveUp();
		}

		yield return new WaitForSeconds(0.5f);

		cup targetCup = cups[Random.Range(0, cups.Length)];
		targetCup.ball = ball;
		
		ball.transform.position = new Vector3(
			targetCup.transform.position.x,
			ball.transform.position.y,
			targetCup.transform.position.z
			);

		yield return new WaitForSeconds(1f);

		foreach(cup cup in cups){
			cup.MoveDown();
		}

		yield return new WaitForSeconds(1f);

		for(int i=0;i<5;i++){
			cup cup1 = cups[Random.Range(0, cups.Length)];
			cup cup2 = cup1;

			while(cup2 == cup1){
				cup2 = cups[Random.Range(0, cups.Length)];
			}

			Vector3 cup1Position = cup1.targetPosition;
			cup1.targetPosition = cup2.targetPosition;
			cup2.targetPosition = cup1Position;

			yield return new WaitForSeconds(0.75f);
		}

		player.canPick = true;
	}
}
