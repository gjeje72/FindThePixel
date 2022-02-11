using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Manager
{
    public class WinMenu : MonoBehaviour
    {
        public int scoreValue;
        public TextMeshProUGUI score;
        public TextMeshProUGUI scoreTotal;
        public TextMeshProUGUI highScore;
        public TextMeshProUGUI scoreTime;

        // Sets the score and time value when WinMenu opened.
        private void Start()
        {
            if (!score || !scoreTime)
                return;
            int activeSceneNumber;
            float time;

            this.SetScores(out activeSceneNumber, out time);

            this.SetActiveSceneDone(activeSceneNumber);

        }

        //  Method used to set scores.
        private void SetScores(out int activeSceneNumber, out float time)
        {
            SetTime(out activeSceneNumber, out time);
            SetCurrentScore(activeSceneNumber, time);
            int scoreTotalValue = SetTotalScore(activeSceneNumber);
            SetHighScore(activeSceneNumber, scoreTotalValue);
        }

        //  Method used to set time.
        private void SetTime(out int activeSceneNumber, out float time)
        {
            activeSceneNumber = SceneManager.GetActiveScene().buildIndex;
            time = Time.timeSinceLevelLoad;
            var minutes = Mathf.Floor(time / 60);
            var secondes = Mathf.Floor(time % 60);
            scoreTime.text = $"Pixel trouvé en \r\n {minutes} minutes et {secondes} secondes";
        }

        //  Method used to set current score.
        private void SetCurrentScore(int activeSceneNumber, float time)
        {
            scoreValue = (int)((1 / (time / 100)) * 300);
            score.text = $"Score : {scoreValue}";
            PlayerPrefs.SetInt($"scoreLevel{activeSceneNumber}", scoreValue);
        }

        //  Method used to set total score.
        private int SetTotalScore(int activeSceneNumber)
        {
            var scoreTotalValue = 0;
            for (int i = activeSceneNumber; i > 1; i--)
            {
                var score = PlayerPrefs.GetInt($"scoreLevel{i}");
                scoreTotalValue = scoreTotalValue + score;
            }
            this.scoreTotal.text = $"Score Total : {scoreTotalValue}";
            return scoreTotalValue;
        }

        //  Method used to set High score.
        private void SetHighScore(int activeSceneNumber, int scoreTotalValue)
        {
            if (activeSceneNumber == 10)
            {
                var highScore = PlayerPrefs.GetInt("highScore");

                if (highScore < scoreTotalValue)
                {
                    SaveHighScore(scoreTotalValue);
                    return;
                }
                else
                {
                    this.highScore.text = $"High score : {highScore}";
                }
            }
        }

        // Method used to save high score.
        private int SaveHighScore(int scoreTotalValue)
        {
            int highScore;
            PlayerPrefs.SetInt("highScore", scoreTotalValue);
            highScore = scoreTotalValue;
            this.highScore.text = $"High score : {highScore} NEW";
            return highScore;
        }

        // Method used to save active scene as done. 
        private void SetActiveSceneDone(int activeSceneNumber)
        {
            PlayerPrefs.SetInt($"isLevel{activeSceneNumber}Done", 1);
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown("n"))
                this.NextLevel();

            if (Input.GetKeyDown("m"))
                this.NavigateToMenu();

            if (Input.GetKeyDown("r"))
                this.RestartLevel();
        }

        // Method used to go to Menu.
        public void NavigateToMenu()
        {
            for (int i = 2; i < 11; i++)
            {
                PlayerPrefs.SetInt($"scoreLevel{i}", 0);
            }
            SceneManager.LoadScene(0);
        }

        // Method used to restart the level.
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Method used to start the next level.
        public void NextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
