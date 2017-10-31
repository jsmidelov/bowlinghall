﻿using BowlingLib.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BowlingLib.Model
{
    class Game : IGame
    {
        private int score { get; set; }
        private int currentSeries { get; set; }
        private int currentRoll { get; set; }

        public IList<string> Series { get; }
        public int Score { get {
                if (Score == 0)
                    return CalculateScore();
                return score;
            }}
        

        public Game()
        {
            score = 0;
            currentSeries = 1;
            currentRoll = 1;
            Series = new List<string> { "", "", "" };
        }
        public int CalculateScore()
        {
            int tmp = 0;
            // Step through the score string (param?)
            // Add the number, 0 for -
            // For S and X, use 20 and 30 temporarily
            // TODO do something proper for S and X
            foreach (string series in Series)
            {
                var charArr = series.ToCharArray();
                foreach (char c in charArr)
                {
                    switch (c)
                    {
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                            tmp += int.Parse(c.ToString());
                            break;
                        case 'S':
                            tmp += 20;
                            break;
                        case 'X':
                            tmp += 30;
                            break;
                        case '-':
                            break;
                        default:
                            break;
                    }
                }
            }
            score = tmp;
            return score;
        }
        public void RegisterRoll(char roll)
        {
            string str = roll.ToString().ToUpper();
            if (!Regex.IsMatch(str, "[1-9-\\/X]") || currentSeries>3)
            {
                return; // No support for this character
            }
            Series[currentSeries] += str;
        }

        public char ThrowBall()
        {
            char[] possibleOutcomes = { '-', 'S', 'X','1','2','3','4','5','6','7','8','9' };
            Random dice = new Random();
            return possibleOutcomes[dice.Next(0,possibleOutcomes.Length)];
        }
    }
}
