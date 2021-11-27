using System;
using System.Collections.Generic;

namespace Lab1___Shops
{
    public class Student
    {
        internal string name;
        private readonly int student_id;
        internal int s_group_id;
        internal int stud_course_num;
        internal Student (Group group, string enter_name, int id)
        {
            name = enter_name;
            student_id = id;
            s_group_id = group.group_id;
            group.Group_Students.Add(this);
            stud_course_num = group.course_id;
            Console.WriteLine("New student");
            Console.WriteLine(enter_name);
        }
    }
    public class Group
    {
        public List<Student> Group_Students = new List<Student>(); // stud list

        private readonly int group_num; // group namber
        internal int course_id; // group's course
        internal int group_id; // XXX group id 
        internal string full_name; // (M3*course_id**group_num*) FULL group name

        public bool Check_name(string name) // check group name
        {

            if ((name.Length == 5) & (name.Substring(0, 2) == "M3") & (int.Parse(Convert.ToString(name[2])) > 0)
                & (int.Parse(Convert.ToString(name[2])) < 10) & (int.Parse(Convert.ToString(name[3])) > -1) &
                (int.Parse(Convert.ToString(name[3])) < 10) & (int.Parse(Convert.ToString(name[4])) > 0) &
                (int.Parse(Convert.ToString(name[4])) < 10))
            {
                return true;
            }
            else
            {
                Console.WriteLine("ERROR invalid group name");
                return false;
            }
        }

        public Group(string name)
        {
            

            if (Check_name(name))
            {
                full_name = name;
                group_num = int.Parse(full_name.Substring(3, 2));
                course_id = int.Parse(full_name.Substring(2, 1));
                group_id = int.Parse(full_name.Substring(2, 3));
                Console.WriteLine("New group");
                Console.WriteLine(full_name);
            } else
            {
                Console.WriteLine("Group name ERROR");
                // delete this
            }
        }
    }

    internal interface IIsuService
    {
        Group CreateGroup(string name); //create
        Group FindGroup(string name);
        List<Group> FindGroups(int course_number);

        Student CreateStudent(Group group, string name); //create and add to group
        Student GetStudent(int id);
        List<Student> FindStudents_name(string name);
        List<Student> FindStudents_group(string name);
        List<Student> FindStudents_course(int course_number);
        
        void ChangeStudentGroup(Student student, Group new_group);
        
    }

    public class Isu : IIsuService
    {
        public bool Check_name(string name) // check group name
        {

            if ((name.Length == 5) & (name.Substring(0, 2) == "M3") & (int.Parse(Convert.ToString(name[2])) > 0)
                & (int.Parse(Convert.ToString(name[2])) < 10) & (int.Parse(Convert.ToString(name[3])) > -1) &
                (int.Parse(Convert.ToString(name[3])) < 10) & (int.Parse(Convert.ToString(name[4])) > 0) &
                (int.Parse(Convert.ToString(name[4])) < 10))
            {
                return true;
            }
            else
            {
                Console.WriteLine("ERROR invalid group name");
                return false;
            }
        }

        public List<Group> Groups = new List<Group>();
        public List<Student> Students = new List<Student>();
        int student_id = -1;
        public Group CreateGroup(string name)
        {
            Group g = new Group(name);
            for (int i = 0; i < Groups.Count; i++)
            {
                if (Groups[i].full_name == name)
                {
                    Console.WriteLine("Same group name ERROR");
                    return null; // ERROR
                } 
            }
            Groups.Add(g);
            return g;
        }
        public Group FindGroup(string name)
        {
            if (Check_name(name))
            {
                for (int i = 0; i < Groups.Count; i++)
                {
                    if (Groups[i].full_name == name)
                    {
                        return Groups[i];
                    }
                }
            }
            else
            {
                return null;
            }
            Console.WriteLine("No such group");
            return null;
        }
        public List<Group> FindGroups(int course_number)
        {
            List<Group> groups_list = new List<Group>();
            for (int i = 0; i < Groups.Count; i++)
            {  
                if (Groups[i].course_id == course_number)
                {
                    groups_list.Add(Groups[i]);
                }
            }
            return groups_list;
        }

        public Student CreateStudent(Group group, string name)
        {
            student_id += 1;
            Student s = new Student(group, name, student_id);
            Students.Add(s);
            group.Group_Students.Add(s);
            return s;
        }
        public Student GetStudent(int id)
        {
            if (Students[id] == null)
            {
                Console.WriteLine("No such student id");
                return null; // ERROR
            } else
            {
                return Students[id];
            }   
        }
        public List<Student> FindStudents_name(string name)
        {
            List<Student> stud_list_find = new List<Student>();
            for (int i = 0; i < Students.Count; i++)
            {
                if (Students[i].name == name)
                {
                    stud_list_find.Add(Students[i]);
                }
            }
            return stud_list_find;
        }
        public List<Student> FindStudents_group(string name)
        {
            if (Check_name(name))
            {
                for (int i = 0; i < Groups.Count; i++)
                {
                    if (Groups[i].full_name == name)
                    {
                        return Groups[i].Group_Students;
                    }
                }
                Console.WriteLine("No such group");
                return null;
            }
            else
            {
                return null;
            }
            
        }
        public List<Student> FindStudents_course(int course_number)
        {
            List<Student> stud_list = new List<Student>();
            for (int i = 0; i < Students.Count; i++)
            {
                if (Students[i].stud_course_num == course_number)
                {
                    stud_list.Add(Students[i]);
                }
            }
            if (stud_list == null)
            {
                Console.WriteLine("No such course");
                return null;
            } else
            {
                return stud_list;
            }
        }
        public void ChangeStudentGroup(Student student, Group new_group)
        {
            student.stud_course_num = new_group.course_id;
            student.s_group_id = new_group.group_id;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Isu lab_1 = new Isu();

            //lab_1.CreateGroup("M3211");
            lab_1.CreateStudent(lab_1.CreateGroup("M3211"), "Max");
            //lab_1.CreateStudent(lab_1.CreateGroup("M3211"), "Tax");
            lab_1.GetStudent(0); // student by student id
            List<Student> stud_max = lab_1.FindStudents_name("Max");
            List<Student> stud_group = lab_1.FindStudents_group("M3211");
            List<Student> stud_course = lab_1.FindStudents_course(2);
            //Console.WriteLine(lab_1.FindGroup("M3211").full_name); // public group_id для наглядности
            List<Group> groups_courses = lab_1.FindGroups(2);
            lab_1.ChangeStudentGroup(lab_1.GetStudent(0), lab_1.CreateGroup("M3212"));
        }
    }
}
