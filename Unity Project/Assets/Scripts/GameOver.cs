﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour {

	public GameObject screenParent;
	public GameObject scoreParent;
	public UnityEngine.UI.Text loseText;
	public UnityEngine.UI.Text scoreText;
	public UnityEngine.UI.Image[] stars;

	// Use this for initialization
	void Start () {
		screenParent.SetActive (false);

		for (int i = 0; i < stars.Length; i++) {
			stars [i].enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowLose()
	{
		screenParent.SetActive (true);
		scoreParent.SetActive (false);

		Animator animator = GetComponent<Animator> ();

		if (animator) {
			animator.Play ("GameOverShow");
		}
	}

	public void ShowWin(int score, int starCount)
	{
		screenParent.SetActive (true);
		loseText.enabled = false;

		scoreText.text = score.ToString ();
		scoreText.enabled = false;

		Animator animator = GetComponent<Animator> ();

		if (animator) {
			animator.Play ("GameOverShow");
		}
		StoryProgress.score[int.Parse(UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name)-1] = score;
		StartCoroutine (ShowWinCoroutine (starCount));

	}

	private IEnumerator ShowWinCoroutine(int starCount)
	{
		yield return new WaitForSeconds (0.5f);

		if (starCount < stars.Length) {
			for (int i = 0; i <= starCount; i++) {
				stars [i].enabled = true;

				if (i > 0) {
					stars [i - 1].enabled = false;
				}

				yield return new WaitForSeconds (0.5f);
			}
		}
		StoryProgress.LevelStars[int.Parse(UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name)-1] = starCount;
		scoreText.enabled = true;
	}

	public void OnReplayClicked()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name);
	}

	public void OnDoneClicked()
	{
		
		UnityEngine.SceneManagement.SceneManager.LoadScene ("levelSelect");
	}
}
