using System.Collections.Generic;
using System;

public static class GameDataManager
{
	//-------------------------------------- high score section--------------------------------------------------------------------------
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

		// Create HighscoreEntry
		HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, date = date };

		// Load saved Highscores
		highScores = LoadHighScoreData();

		if (highScores == null)
		{
			// There's no stored table, initialize
			highScores = new Highscores()
			{
				highscoreEntryList = new List<HighscoreEntry>()
			};
		}

		// Add new entry to Highscores
		highScores.highscoreEntryList.Add(highscoreEntry);

		CheckAndSave();

		// Save updated Highscores
		//SaveHighScoreData();
	}
	public static Highscores LoadHighScoreData()
	{
		highScores = BinarySerializer.Load<Highscores>("high-score-data.txt");
		//UnityEngine.Debug.Log("<color=blue>[Highscores Loaded.</Color>");
		return highScores;
	}

	public static void CheckAndSave()
    {

		if (highScores != null)
		{
			//Code to sort high Scores
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
			// removes the 11th entry.
			highScores.highscoreEntryList.RemoveAt(10);
			SaveHighScoreData();
		}
	}

	static void SaveHighScoreData()
	{
		BinarySerializer.Save(highScores, "high-score-data.txt");
		//UnityEngine.Debug.Log("<color=blue>[Highscores Saved.</Color>");
	}

}