using System;
using System.Collections.Generic;
using System.Text;

namespace Start.model
{
    public abstract class Base
    {

        public string Name { get; }
        public int Age { get; }
        private List<int> grades = new List<int>();

        public List<int> Grades
        {
            get
            {
                return new List<int>(this.grades);
            }
        }

        public Base(string name, int age)
        {
            this.Age = age;
            this.Name = name;
        }

        public void AddGrade(int grade)
        {
            this.grades.Add(grade);
        }
    }
}
