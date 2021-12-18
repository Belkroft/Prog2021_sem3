using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab_2_IsuEXTRA
{
	class Course
	{
		internal List<Faculty> facultys = new List<Faculty>();
		internal List<OGNP> ognps = new List<OGNP>();

		readonly byte course_num;
		internal Course(byte num)
		{
			if (num >= 1 & num <= 7)
			{
				course_num = num;
			}
			else
			{
				Console.WriteLine("Course num ERROR");
			}
		}
		public int stud_id = -1;
		//check megas 
		short fid = -1;
		internal bool Add_faculty (string name, char mega)
        {
			//check megas 
			fid += 1;
			Faculty f = new Faculty(name, mega, fid);
			this.facultys.Add(f);
			return true;
        }
		short oid = -1;
		internal bool Add_OGNP (string name, char mega)
		{
			//check megas 
			oid += 1;
			OGNP o = new OGNP(name, mega, oid);
			this.ognps.Add(o);
			return true;
		} //-------------------------------------------
		internal bool Student_to_O (OGNP ognp, Student student)
        {
			if (ognp.omega == student.mega || student.ognp2 != -1)
            {
				Console.WriteLine("Same MegaCourse or full OGNP");
				return false;
            }
			if (student.ognp1 == -1)
            {
				student.ognp1 = ognp.o_id;

            }
			else
            {

            }
			return true;
        } //----------------------------------------
		internal List<Faculty> Streams() //--------------------------------------
        {
			return facultys;
		}
	}
	internal class Mega
	{
		readonly internal List<char> megas;
		internal Mega(List<char> letters)
        {
			megas = letters;
        }
		bool Add_mega(char m)
        {
			//check
			this.megas.Add(m);
			return true;
        }
	}
	class Faculty
	{
		internal List<Group> groups = new List<Group>();
		char fmega;
		short f_id;
		string fname;
		internal Faculty (string name, char mega, short fid)
        {
			fname = name;
			f_id = fid;
			fmega = mega;
		}
		internal bool Add_group(byte limit)
        {
			Group g = new Group(limit);
			groups.Add(g);
			return true;
        }
		internal bool Add_student(string name, Course course)
		{
			//limit
			for (short i = 0; i < groups.Count; i++)
            {
				if (this.groups[i].students < this.groups[i].limit)
                {
					course.stud_id += 1;
					Student s = new Student(name, course.stud_id);
					s.mega = this.fmega;
					this.groups[i].stud_list.Add(s);
					this.groups[i].students += 1;
					return true;
				}
            }
			return false;
		}
	}
	class OGNP
	{
		internal List<Group> groups = new List<Group>();
		internal char omega;
		internal short o_id;
		string oname;
		internal OGNP(string name, char mega, short oid)
		{
			oname = name;
			o_id = oid;
			omega = mega;
		}
		internal bool Add_group(byte limit)
		{
			Group g = new Group(limit);
			groups.Add(g);
			return true;
		}
		internal bool Add_student (Student student, List<ushort> table) //---------------------------------------------
        {
			if (this.omega == student.mega) {
				Console.WriteLine("Same MegaCourse");
				return false;
            }
			else if (student.ognp2 != -1)
            {
				Console.WriteLine("Full OGNP");
				return false;
			}

			if (student.ognp1 == -1)
            {
				student.ognp1 = this.o_id;
			}
			else
            {
				student.ognp2 = this.o_id;
			}
			for (short i = 0; i < groups.Count; i++)
			{
				int j = this.groups[i].timetable_o.lesson - 1;
				if (this.groups[i].students < this.groups[i].limit & table[j] == 0)
				{
					this.groups[i].stud_list.Add(student);
					return true;
				}
			}
			Console.WriteLine("Same timetable");
			return false;
		}
		internal bool Delete_student (Student student) //-----------------------------------------
        {
			for (byte i = 0; i < this.groups.Count; i++)
			{
				for (byte j = 0; j < this.groups[j].stud_list.Count; j++)
				{
					if (student.s_id == this.groups[j].stud_list[i].s_id)
					{
						this.groups.RemoveAt(j);
						if (student.ognp1 == o_id)
						{
							student.ognp1 = -1;
						}
						else
						{
							student.ognp2 = -1;
						}
						return true;
					}
				}
			}
			return false;
        }
	}
	class TT
    {
		internal List<string> teacher = new List<string>();
		internal List<string> subject = new List<string>();
		internal List<ushort> cabinet = new List<ushort>();
	}
	class TT_o
    {
		internal byte lesson;
		internal string teacher_o;
		internal string subject_o;
		internal ushort cabinet_o;
	}
	class Group
	{
		internal TT timetable = new TT();
		internal TT_o timetable_o = new TT_o();
		internal byte lesson;
		internal List<Student> stud_list = new List<Student>();
		//internal List<int> students_o;
		internal byte limit;
		internal byte students = 0;
		//zero constr 30
		internal Group (byte num)
        {
			limit = num;
        }
		internal bool Add_TT(List<string> t, List<string> s, List<ushort> c)
        {
			//check
			this.timetable.teacher = t;
			this.timetable.subject = s;
			this.timetable.cabinet = c;
			return true;
        }
		internal bool Add_TT_o(byte l, string t, string s, ushort c)
		{
			//check
			this.timetable_o.lesson = l;
			this.timetable_o.teacher_o = t;
			this.timetable_o.subject_o = s;
			this.timetable_o.cabinet_o = c;
			return true;
		}
		internal List<Student> Stud_o_inference () //-------------------------------
        {
			return this.stud_list;
        }
		internal List<Student> Stud_no_o_inference() //-------------------------------
		{
			List<Student> empty_stud = new List<Student>();
			for (byte i = 0; i < this.stud_list.Count; i++)
            {
				if (this.stud_list[i].ognp1 == -1 & this.stud_list[i].ognp2 == -1)
                {
					empty_stud.Add(this.stud_list[i]);
				}
            }
			return empty_stud;
		}
	}

	class Student
	{
		string name;
		internal int s_id;
		internal char mega;
		internal short ognp1 = -1;
		internal short ognp2 = -1;

		internal Student(string sname, int id)
        {
			sname = name;
			s_id = id;
        }
	}

	class Program
    {
		static void Main(string[] args)
        {
			Mega megas = new Mega(new List<char>() { 'M', 'H' });
			Course c2 = new Course(2);
			//Course c3 = new Course(3);

			c2.Add_faculty("Math", megas.megas[0]);
			c2.Add_faculty("History", megas.megas[1]);

			c2.Add_OGNP("dop_Math", megas.megas[0]);
			c2.Add_OGNP("dop_History", megas.megas[1]);

			c2.facultys[0].Add_group(30);
			c2.facultys[0].Add_group(30);
			c2.facultys[1].Add_group(30);
			c2.facultys[1].Add_group(30);

			c2.facultys[0].groups[0].Add_TT(new List<string> { "", "", "", "", "", "", "", "" }, new List<string> { "", "", "", "", "", "", "", "" },
				new List<ushort> { 0, 0, 0, 0, 0, 0, 0, 0 });

			c2.ognps[0].Add_group(30);
			c2.ognps[1].Add_group(30);

			c2.ognps[1].groups[0].Add_TT_o(3, "Vasiliy", "History", 325);

			c2.facultys[0].Add_student("Max", c2);
			c2.facultys[1].Add_student("Eric", c2);

			c2.ognps[1].Add_student(c2.facultys[0].groups[0].stud_list[0], c2.facultys[0].groups[0].timetable.cabinet);
			c2.ognps[1].Delete_student(c2.facultys[0].groups[0].stud_list[0]);
		}
    }
}
