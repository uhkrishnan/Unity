using UnityEngine;
using System.Collections.Generic;

public static class GameDataManager
{
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

	static void SaveHighScoreData()
	{
		PlayerPrefs.SetInt("highestScore", highScores.highscoreEntryList[0].score);		
		BinarySerializer.Save(highScores, "high-score-data.txt");
	}
}