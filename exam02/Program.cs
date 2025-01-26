using System;
using System.Collections.Generic;

namespace ExamSystem
{
    // Base Question class
    public abstract class Question
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public int Mark { get; set; }
        public List<string> Answers { get; set; } = new List<string>();
        public int RightAnswerId { get; set; }

        public abstract void DisplayQuestion();
    }

    // True/False Question class
    public class TrueFalseQuestion : Question
    {
        public override void DisplayQuestion()
        {
            Console.WriteLine($"True | False Question    Mark{Mark}");
            Console.WriteLine(Body);
            Console.WriteLine("1-True");
            Console.WriteLine("2-False");
        }
    }

    // MCQ Question class
    public class MCQQuestion : Question
    {
        public override void DisplayQuestion()
        {
            Console.WriteLine($"MCQ Question    Mark{Mark}");
            Console.WriteLine(Body);
            for (int i = 0; i < Answers.Count; i++)
            {
                Console.WriteLine($"{i + 1}-{Answers[i]}");
            }
        }
    }

    // Base Exam class
    public abstract class Exam
    {
        public int Time { get; set; }
        public int NumberOfQuestions { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();

        public abstract void ShowExam();
    }

    // Final Exam class
    public class FinalExam : Exam
    {
        public override void ShowExam()
        {
            int totalMarks = 0;
            int earnedMarks = 0;

            foreach (var question in Questions)
            {
                question.DisplayQuestion();
                Console.Write("Please Enter The Answer Id: ");
                int answerId = int.Parse(Console.ReadLine());

                if (answerId == question.RightAnswerId)
                {
                    earnedMarks += question.Mark;
                }
                totalMarks += question.Mark;
            }

            Console.WriteLine($"Your Grade is {earnedMarks} from {totalMarks}");
        }
    }

    // Practical Exam class
    public class PracticalExam : Exam
    {
        public override void ShowExam()
        {
            int totalMarks = 0;
            int earnedMarks = 0;

            foreach (var question in Questions)
            {
                question.DisplayQuestion();
                Console.Write("Please Enter The Answer Id: ");
                int answerId = int.Parse(Console.ReadLine());

                if (answerId == question.RightAnswerId)
                {
                    earnedMarks += question.Mark;
                }
                totalMarks += question.Mark;

                Console.WriteLine($"Right Answer => {question.Answers[question.RightAnswerId - 1]}");
            }

            Console.WriteLine($"Your Grade is {earnedMarks} from {totalMarks}");
        }
    }

    // Subject class
    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public Exam Exam { get; set; }

        //create a Practical Exam
        public void CreatePracticalExam()
        {
            Console.Write("Please Enter the time For Exam From (30 min to 180 min): ");
            int time = int.Parse(Console.ReadLine());

            Console.Write("Please Enter the Number Of questions: ");
            int numberOfQuestions = int.Parse(Console.ReadLine());

            
            Exam = new PracticalExam { Time = time, NumberOfQuestions = numberOfQuestions };

            
            for (int i = 0; i < numberOfQuestions; i++)
            {
                var trueFalseQuestion = new TrueFalseQuestion();
                Console.Write("Please Enter Question Body: ");
                trueFalseQuestion.Body = Console.ReadLine();
                Console.Write("Please Enter Question Mark: ");
                trueFalseQuestion.Mark = int.Parse(Console.ReadLine());

                Console.Write("Please Enter the right Answer id (1 for true | 2 For False): ");
                trueFalseQuestion.RightAnswerId = int.Parse(Console.ReadLine());

                Exam.Questions.Add(trueFalseQuestion);
            }
        }

        // create a Final Exam
        public void CreateFinalExam()
        {
            Console.Write("Please Enter the time For Exam From (30 min to 180 min): ");
            int time = int.Parse(Console.ReadLine());

            Console.Write("Please Enter the Number Of questions: ");
            int numberOfQuestions = int.Parse(Console.ReadLine());

            
            Exam = new FinalExam { Time = time, NumberOfQuestions = numberOfQuestions };

            
            for (int i = 0; i < numberOfQuestions; i++)
            {
                Console.Write("Please Enter Type Of Question (1 for MCQ | 2 For True | False): ");
                int questionType = int.Parse(Console.ReadLine());

                if (questionType == 1)
                {
                    var mcqQuestion = new MCQQuestion();
                    Console.Write("Please Enter Question Body: ");
                    mcqQuestion.Body = Console.ReadLine();
                    Console.Write("Please Enter Question Mark: ");
                    mcqQuestion.Mark = int.Parse(Console.ReadLine());

                    Console.WriteLine("Choices Of Question");
                    for (int j = 0; j < 3; j++)
                    {
                        Console.Write($"Please Enter Choice Number {j + 1}: ");
                        mcqQuestion.Answers.Add(Console.ReadLine());
                    }

                    Console.Write("Please Enter the right Answer id: ");
                    mcqQuestion.RightAnswerId = int.Parse(Console.ReadLine());

                    Exam.Questions.Add(mcqQuestion);
                }
                else
                {
                    var trueFalseQuestion = new TrueFalseQuestion();
                    Console.Write("Please Enter Question Body: ");
                    trueFalseQuestion.Body = Console.ReadLine();
                    Console.Write("Please Enter Question Mark: ");
                    trueFalseQuestion.Mark = int.Parse(Console.ReadLine());

                    Console.Write("Please Enter the right Answer id (1 for true | 2 For False): ");
                    trueFalseQuestion.RightAnswerId = int.Parse(Console.ReadLine());

                    Exam.Questions.Add(trueFalseQuestion);
                }
            }
        }
    }

    // Main program
    class Program
    {
        static void Main(string[] args)
        {
            Subject subject = new Subject { SubjectId = 1, SubjectName = "C#" };

            Console.Write("Please Enter The Type Of Exam (1 For Practical | 2 For Final): ");
            int examType = int.Parse(Console.ReadLine());

            if (examType == 1)
            {
                subject.CreatePracticalExam(); // Create a Practical Exam
            }
            else if (examType == 2)
            {
                subject.CreateFinalExam(); // Create a Final Exam
            }
            else
            {
                Console.WriteLine("Invalid exam type selected.");
                return;
            }

            Console.Write("Do You Want To Start Exam (Y|N): ");
            char startExam = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();

            if (startExam == 'Y')
            {
                subject.Exam.ShowExam(); // Start the exam
            }

            Console.WriteLine("Thank You");
        }
    }
}