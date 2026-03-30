using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem
{
    private static string _path = Application.persistentDataPath + "/my.cookie";
    private static int[] _score = new int[5];

    public static void CheckScore(int score)
    {
        if (!File.Exists(_path))
        {
            FirstSave(score);
        }
        else
        {
            HighScore data = LoadScore();
            NewSave(score, data);
        }
    }

    public static int[] GetScore()
    {
        return _score;
    }

    private static void FirstSave(int score)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(_path, FileMode.Create);

        HighScore data = new HighScore(new int[]{ score, 0, 0, 0, 0 });

        formatter.Serialize(stream, data);
        stream.Close();

        _score = data._score; //array for high score text

    }

    private static void NewSave(int score, HighScore oldScore)
    {
        int isGraterThen = 6;
        int[] tempArray = new int[5];

        for (int i = 4; i<=4 && i >= 0; i--)      //find out which score to update
        {
            if (score > oldScore._score[i])
            {
                isGraterThen = i;
                tempArray[i] = oldScore._score[i];
            }
            else
                tempArray[i] = oldScore._score[i];
        }

        if (isGraterThen != 6)
        {

            for (int i = 4; i>= isGraterThen; i--)
            {
                if (i > 0)
                {
                    tempArray[i] = tempArray[i - 1];     
                }
            }
            tempArray[isGraterThen] = score;
            SaveScore(tempArray);
        }
        _score = tempArray; //array for high score text
        isGraterThen = 6;
    }

    private static void SaveScore(int[] scoreArray)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(_path, FileMode.Create);

        HighScore data = new HighScore(scoreArray);

        formatter.Serialize(stream,data);
        stream.Close();
    }

    private static HighScore LoadScore()
    {
        if (File.Exists(_path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(_path, FileMode.Open);

            HighScore data = formatter.Deserialize(stream) as HighScore;
            stream.Close();

            return data;
        }
        else
            return null;
    }
}
