﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	#region Variables
	public enum GameState { menu, playing, paused, gameover };

	public static int level = 1;
	public static int score = 0;
	public static int lives = 3;
	public static GameState state;
	public static GameObject player;
	public AudioClip GoNoise;
	public bool GoNoisePlayed = false;

	//UI elements
	public Text scoreText;
	//public Text levelCompleteText;
	public Text levelText;
	public Text levelcompletedText;
	public Text levelcompletedscore;
	public GameObject pausedTextObject;
	public GameObject gameoverTextObject;
	public Image[] lifeIcons;
	#endregion

	#region Methods
	void Start()
	{
		state = GameState.playing;

		//player = GameObject.FindGameObjectWithTag("Player");
	}
	void Update()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		levelcompletedText.text = "Level " + level + " Completed!";
		levelText.text = "Level " + level;
		scoreText.text = "" + score;
		levelcompletedscore.text = "Total Score: " + score;


		for (int i = 0; i < lifeIcons.Length; i++)
		{
			if (i < lives - 1)
			{
				lifeIcons[i].enabled = true;
			}
			else
			{
				lifeIcons[i].enabled = false;
			}
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (state == GameState.playing)
			{
				Time.timeScale = 0;
				state = GameState.paused;
			}
			else if (state == GameState.paused)
			{
				Time.timeScale = 1;
				state = GameState.playing;
			}
		}

		if (state == GameState.playing)
		{
			pausedTextObject.SetActive(false);
			gameoverTextObject.SetActive(false);

		}
		else if (state == GameState.paused)
		{
			pausedTextObject.SetActive(true);
		}
		else if (state == GameState.gameover && GoNoisePlayed == false)
		{
			gameoverTextObject.SetActive(true);
			if (GoNoise != null)
			{
				AudioSource.PlayClipAtPoint(GoNoise, transform.position, 3f);
				GoNoisePlayed = true;
			}
		}
	}

	public static void AddScore(int points)
	{
		score += points;
	}

	public static void Death()
	{
		//decrement lives
		lives--;

		//if lives == 0 
		if (lives <= 0)
		{
			state = GameState.gameover;
		}
		else
		{
			player.SendMessage("Respawn");
			//move the camera immediately to player position
			Camera.main.transform.position = new Vector3(player.transform.position.x,player.transform.position.y,Camera.main.transform.position.z);
		}

		//screen shake
		FollowCam cam = Camera.main.GetComponent<FollowCam>();
		if (cam != null)
		{
			cam.Shake();
		}
	}

	public static void NextLevel()
	{
		level++;
		SceneManager.LoadScene("Level " + level);
	}
	#endregion
}