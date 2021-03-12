using System;
using System.Collections.Generic;
using UnityEngine;

public class Score
{
    private int[] m_scores;

    public Score()
    {
        m_scores = new int[2];
        Reset();
    }

    public void Reset()
    {
        for(int i = 0; i < m_scores.Length; ++i)
        {
            m_scores[i] = 0;
        }
    }

    public int GetScore(int index)
    {
        return m_scores[index];
    }

    public void AddScore(int index, int score)
    {
        m_scores[index] += score;
    }
}