using UnityEngine;
using System.Collections.Generic;

public static class GameDataManager
{
	// GAME DETAILS SECTION-------------------------------------------------------------
	[System.Serializable]
	public class GameDetails
	{
		public int selectedTheme = 0;
		public int gameNumber = 1;
		public int vibrationState = 1;
		public int soundState = 1;
		public int highestScore = 0;
	}

	static GameDetails gameDetails;

	// VIBRATION FUNCTIONS ------------------------------------------
	public static int GetVibrationState()
	{
		LoadGameDetails();
		return gameDetails.vibrationState;
	}

	public static void SetVibrationState(int vibrationState)
	{
		gameDetails.vibrationState = vibrationState;
		SaveGameDetails();
	}

	// SOUND FUNCTIONS ------------------------------------------
	public static int GetSoundState()
	{
		LoadGameDetails();
		return gameDetails.soundState;
	}

	public static void SetSoundState(int soundState)
	{
		gameDetails.soundState = soundState;
		SaveGameDetails();
	}
	
	// GAMENUMBER FUNCTIONS ------------------------------------------
	public static int GetGameNumber()
	{
		LoadGameDetails();
		return gameDetails.gameNumber;
	}

	public static void SetGameNumber(int currGameNumber)
	{
		gameDetails.gameNumber = currGameNumber;
		SaveGameDetails();
	}

	// THEME FUNCTIONS ------------------------------------------
	public static int SelectedTheme()
	{
		LoadGameDetails();
		return gameDetails.selectedTheme;
	}

	public static void SetLightTheme()
	{
		gameDetails.selectedTheme = 1;
		SaveGameDetails();
	}

	public static void SetDarkTheme()
	{
		gameDetails.selectedTheme = 0;
		SaveGameDetails();
	}

	static void SaveGameDetails()
	{
		BinarySerializer.Save(gameDetails, "game-data.txt");
    }

	static GameDetails LoadGameDetails()
	{
		gameDetails = BinarySerializer.Load<GameDetails>("game-data.txt");
		return gameDetails;
	}

	// HIGHSCORE SECTION-------------------------------------------------------------
	
	[System.Serializable]
	public class Highscores
	{
		public List<HighscoreEntry> highscoreEntryList = new List<HighscoreEntry>();
	}

	[System.Serializable]
	public class HighscoreEntry
	{
		public int score;
		public string date;
	}

	static Highscores highScores;

	public static void AddHighscoreEntry(int score, string date)
	{
		HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, date = date };
		highScores = LoadHighScoreData();

		if (highScores == null)
		{
			highScores = new Highscores()
			{
				highscoreEntryList = new List<HighscoreEntry>()
			};
		}

		highScores.highscoreEntryList.Add(highscoreEntry);
		CheckAndSave();
	}

	public static Highscores LoadHighScoreData()
	{
		highScores = BinarySerializer.Load<Highscores>("high-score-data.txt");
		return highScores;
	}

	public static void CheckAndSave()
    {
		if (highScores != null)
		{
			for (int i = 0; i < highScores.highscoreEntryList.Count; i++)
			{
				for (int j = i + 1; j < highScores.highscoreEntryList.Count; j++)
				{
					int scoreI = highScores.highscoreEntryList[i].score;
					int scoreJ = highScores.highscoreEntryList[j].score;

					if (scoreI < scoreJ)
					{
						GameDataManager.HighscoreEntry tmp = highScores.highscoreEntryList[i];
						highScores.highscoreEntryList[i] = highScores.highscoreEntryList[j];
						highScores.highscoreEntryList[j] = tmp;
					}
				}
			}
		}

		if (highScores.highscoreEntryList.Count <= 10)
		{
			SaveHighScoreData();
		}
		else
		{
			highScores.highscoreEntryList.RemoveAt(10);
			SaveHighScoreData();
		}
	}

	public static int GetHighestScore()
    {
		LoadGameDetails();
		return gameDetails.highestScore;
	}

	public static void SetHighestScore()
	{
		gameDetails.highestScore = highScores.highscoreEntryList[0].score;
		SaveGameDetails();
	}

	static void SaveHighScoreData()
	{
		SetHighestScore();
		BinarySerializer.Save(highScores, "high-score-data.txt");
	}
}