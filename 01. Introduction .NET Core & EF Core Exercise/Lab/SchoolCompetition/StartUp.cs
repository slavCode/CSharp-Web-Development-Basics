namespace SchoolCompetition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class StartUp
    {
        static void Main()
        {
            var studentsPoints = new Dictionary<string, int>();
            var studentsCategories = new Dictionary<string, SortedSet<string>>();

            AddStudentsToCollection(studentsPoints, studentsCategories);
            PrintOrderedStudents(studentsPoints, studentsCategories);
        }

        private static void PrintOrderedStudents
            (Dictionary<string, int> studentsPoints,
             Dictionary<string, SortedSet<string>> studentsCategories)
        {
            var orderedStudents = studentsPoints
                                 .OrderByDescending(kvp => kvp.Value)
                                 .ThenBy(kvp => kvp.Key);

            foreach (var student in orderedStudents)
            {
                var name = student.Key;
                var points = student.Value;
                var categories = studentsCategories[name].ToList();

                Console.WriteLine($"{name}: {points} [{string.Join(", ", categories)}]");
            }
        }

        private static void AddStudentsToCollection
            (Dictionary<string, int> studentsPoints,
             Dictionary<string, SortedSet<string>> studentsCategories)
        {
            var input = Console.ReadLine();

            while (true)
            {
                if (input == "END") break;

                var studentInfo = input.Split(' ');
                var name = studentInfo[0];
                var category = studentInfo[1];
                var points = int.Parse(studentInfo[2]);

                if (!studentsPoints.ContainsKey(name))
                {
                    studentsPoints.Add(name, 0);
                }

                if (!studentsCategories.ContainsKey(name))
                {
                    studentsCategories.Add(name, new SortedSet<string>());
                }

                studentsPoints[name] += points;
                studentsCategories[name].Add(category);

                input = Console.ReadLine();
            }
        }
    }
}