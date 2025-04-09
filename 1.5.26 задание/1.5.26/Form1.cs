
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QuizApp
{
    public partial class Form1 : Form
    {
        private List<Question> questions;
        private int currentQuestionIndex = 0;
        private int score = 0;
        private bool isAnswerChecked = false;

        public Form1()
        {
            InitializeComponent();
            LoadQuestions();
            DisplayQuestion();
        }

        private void LoadQuestions()
        {
            questions = new List<Question>
            {
                new Question("Какой язык используется для разработки .NET-приложений?", new[] { "Python", "Java", "C#", "PHP" }, 2),
                new Question("Сколько байт в одном килобайте?", new[] { "100", "1024", "512", "256" }, 1),
                new Question("Что такое IDE?", new[] { "Компьютер", "СУБД", "Среда разработки", "Алгоритм" }, 2)
            };
        }

        private void DisplayQuestion()
        {
            if (currentQuestionIndex >= questions.Count)
            {
                MessageBox.Show($"Тест завершен! Ваш результат: {score} из {questions.Count}", "Результат");
                Application.Exit();
                return;
            }

            var q = questions[currentQuestionIndex];
            label1.Text = q.Text;

            radioButton1.Text = q.Options[0];
            radioButton2.Text = q.Options[1];
            radioButton3.Text = q.Options[2];
            radioButton4.Text = q.Options[3];

            ClearSelection();
            button1.Text = "Проверить";
        }

        private void ClearSelection()
        {
            foreach (var rb in new[] { radioButton1, radioButton2, radioButton3, radioButton4 })
            {
                rb.Checked = false;
                rb.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isAnswerChecked)
            {
                int selectedIndex = GetSelectedOption();
                if (selectedIndex == -1)
                {
                    MessageBox.Show("Пожалуйста, выберите вариант ответа.");
                    return;
                }

                int correctIndex = questions[currentQuestionIndex].CorrectAnswer;

                GetRadioButton(correctIndex).ForeColor = System.Drawing.Color.Green;

                if (selectedIndex != correctIndex)
                    GetRadioButton(selectedIndex).ForeColor = System.Drawing.Color.Red;
                else
                    score++;

                isAnswerChecked = true;
                button1.Text = "Далее";
            }
            else
            {
                currentQuestionIndex++;
                isAnswerChecked = false;
                DisplayQuestion();
            }
        }

        private int GetSelectedOption()
        {
            if (radioButton1.Checked) return 0;
            if (radioButton2.Checked) return 1;
            if (radioButton3.Checked) return 2;
            if (radioButton4.Checked) return 3;
            return -1;
        }

        private RadioButton GetRadioButton(int index)
        {
            return index switch
            {
                0 => radioButton1,
                1 => radioButton2,
                2 => radioButton3,
                3 => radioButton4,
                _ => null
            };
        }
    }

    public class Question
    {
        public string Text { get; }
        public string[] Options { get; }
        public int CorrectAnswer { get; }

        public Question(string text, string[] options, int correctAnswer)
        {
            Text = text;
            Options = options;
            CorrectAnswer = correctAnswer;
        }
    }
}
