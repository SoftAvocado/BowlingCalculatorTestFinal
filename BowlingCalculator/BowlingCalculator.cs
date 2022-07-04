using System;
using System.Collections.Generic;

namespace BowlingCalculator
{
    public class Calculator
    {
        Game game;

        public List<int> calculate_Throws(List<int> throws)
        {
            this.game = new Game();
            List<int> calculator_results = new List<int>();
            for (int i = 0; i < throws.Count; i++)
            {
                game.Throw_ball(throws[i]);
                calculator_results.Add(game.Get_latest_total_score());
            }

            return calculator_results;
        }

        public List<int> Get_FrameResults(int frame_id)
        {
            return game.Get_frame_result(frame_id);
        }

        public int Get_FrameScores(int frame_id)
        {
            return game.Get_frame_score(frame_id);
        }

        public int Get_FrameTotal(int frame_id)
        {
            return game.Get_total(frame_id);
        }
    }

    class Game
    {
        private List<Frame> results = new List<Frame>();
        static int frame_id_counter = 1;

        void AddFrameScoreToFrame(int frame_id, int result)
        {
            if ((results[frame_id].Get_isStrike() || results[frame_id].Get_isSpare())
                && (results[frame_id].Get_addedThrows().Count < 3))
            {
                results[frame_id].Add_FrameScore(result);
            }
        }

        void UpdateTotalScoreOfFrame(int frame_id)
        {
            if (frame_id + 1 == 1)
            {
                results[frame_id].Set_total(results[frame_id].Get_frameScore());
            }
            else
            {
                results[frame_id].Set_total(results[frame_id - 1].Get_total() + results[frame_id].Get_frameScore());
            }
        }

        void AddAllScores()
        {
            UpdateTotalScoreOfFrame(frame_id_counter - 1);
            frame_id_counter++;
        }

        void CreateFrame(int throw_id)
        {
            if ((results.Count == 0) || (results[(results.Count) - 1].Get_isFrameEnded()))
            {
                results.Add(new Frame(throw_id));
            }
        }

        public void Throw_ball(int result)
        {
            // create new frame if needed
            int throw_id = frame_id_counter;
            CreateFrame(throw_id);

            // 1st frame has been started
            if (frame_id_counter == 1)
            {
                results[frame_id_counter - 1].Add_resultThrow(result);

                if (results[frame_id_counter - 1].Get_isFrameEnded())
                {
                    AddAllScores();
                }
                else
                {
                    results[frame_id_counter - 1].Set_total(result);
                }
            }
            // 2d frame has been started
            else if (frame_id_counter == 2)
            {
                AddFrameScoreToFrame(frame_id_counter - 2, result);
                UpdateTotalScoreOfFrame(frame_id_counter - 2);
                results[frame_id_counter - 1].Add_resultThrow(result);


                if (results[frame_id_counter - 1].Get_isFrameEnded())
                {
                    AddAllScores();
                }
                else
                {
                    int previous_total = results[frame_id_counter - 2].Get_total();
                    results[frame_id_counter - 1].Set_total(previous_total + result);
                }
            }
            // 3d or more frame has been started
            else if ((frame_id_counter > 2) && (frame_id_counter < 10))
            {
                AddFrameScoreToFrame(frame_id_counter - 3, result);
                AddFrameScoreToFrame(frame_id_counter - 2, result);
                UpdateTotalScoreOfFrame(frame_id_counter - 3);
                UpdateTotalScoreOfFrame(frame_id_counter - 2);
                results[frame_id_counter - 1].Add_resultThrow(result);

                if (results[frame_id_counter - 1].Get_isFrameEnded())
                {
                    AddAllScores();
                }
                else
                {
                    int previous_total = results[frame_id_counter - 2].Get_total();
                    results[frame_id_counter - 1].Set_total(previous_total + result);
                }
            }
            // 10th frame has been started
            else if (frame_id_counter == 10)
            {
                AddFrameScoreToFrame(frame_id_counter - 3, result);
                AddFrameScoreToFrame(frame_id_counter - 2, result);
                UpdateTotalScoreOfFrame(frame_id_counter - 3);
                UpdateTotalScoreOfFrame(frame_id_counter - 2);
                results[frame_id_counter - 1].Set_isBonusThrow(true);

                results[frame_id_counter - 1].Add_resultThrow(result);
                UpdateTotalScoreOfFrame(frame_id_counter - 1);
            }
        }

        public List<int> Get_frame_result(int frame_id)
        {
            return results[frame_id - 1].Get_resultThrows();
        }

        public int Get_frame_score(int frame_id)
        {
            return results[frame_id - 1].Get_frameScore();
        }

        public int Get_total(int frame_id)
        {
            return results[frame_id - 1].Get_total();
        }

        public int Get_latest_total_score()
        {
            return results[results.Count - 1].Get_total();
        }

        class Frame
        {
            int id;
            bool isStrike;
            bool isSpare;
            bool isBonusThrow;
            bool isFrameEnded;
            List<int> resultThrows = new List<int>();
            List<int> addedThrows = new List<int>();
            int frameScore;
            int total;

            public Frame(int id)
            {
                this.id = id;
                this.isStrike = false;
                this.isSpare = false;
                this.isBonusThrow = false;
                this.isFrameEnded = false;
                this.frameScore = 0;
                this.total = 0;
            }

            public void Add_FrameScore(int resultThrow)
            {
                this.frameScore += resultThrow;
                this.addedThrows.Add(resultThrow);
            }

            public void Add_resultThrow(int resultThrow)
            {
                this.resultThrows.Add(resultThrow);
                this.Add_FrameScore(resultThrow);
                this.total += resultThrow;
                if (this.isFrameEnded == false)
                {
                    if ((resultThrow == 10))
                    {
                        this.isStrike = true;
                        if (!this.isBonusThrow) { }
                        this.isFrameEnded = true;
                    }
                    else if (addedThrows.Count == 2)
                    {
                        if ((addedThrows[0] + addedThrows[1]) == 10)
                        {
                            this.isSpare = true;
                        }
                        this.isFrameEnded = true;
                    }
                    // spare bonus end
                    if ((this.isBonusThrow) && (this.isSpare) && (this.addedThrows.Count == 3))
                    {
                        this.isFrameEnded = true;
                    }
                    // strike bonus end
                    else if ((this.isBonusThrow) && (this.isStrike) && (this.addedThrows.Count == 3))
                    {
                        this.isFrameEnded = true;
                    }
                    else if (this.isBonusThrow)
                    {
                        this.isFrameEnded = false;
                    }
                }
            }

            public bool Get_isFrameEnded()
            {
                return isFrameEnded;
            }

            public bool Get_isStrike()
            {
                return isStrike;
            }

            public bool Get_isSpare()
            {
                return isSpare;
            }

            public List<int> Get_resultThrows()
            {
                return resultThrows;
            }

            public List<int> Get_addedThrows()
            {
                return addedThrows;
            }

            public int Get_frameScore()
            {
                return frameScore;
            }

            public void Set_total(int total)
            {
                this.total = total;
            }

            public int Get_total()
            {
                return this.total;
            }

            public void Set_isBonusThrow(bool value)
            {
                this.isBonusThrow = value;
            }
        }
    }
}
