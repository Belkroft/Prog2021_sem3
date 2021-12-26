using System.Collections.Generic;

namespace lab_6
{

    public enum All_states {
        Zero, Open, Active, Resolved
    }

    public class Task_controller : ILists {

        private static int new_task_id = 0;
        public int id;
        private string task_name;
        private string task_description;
        public string comments;
        private All_states task_state;

        private Worker task_charger;

        public int Iid => id;
        public List<Change> Changes = new List<Change>();

        public Task_controller(string console_name, string description) {
            id = new_task_id++;
            task_name = console_name;
            task_description = description;
            task_state = All_states.Open;
            Changes.Add(new Change(All_states.Zero, task_state));
        }

        public void Set_task_name(string value) {
            Changes.Add(new Change(task_name, value));
            task_name = value;
        }

        public void Set_description(string value) {
            Changes.Add(new Change(task_description, value));
            task_description = value;
        }

        public All_states Get_state() => task_state;
        public void Set_state(All_states value) {
            Changes.Add(new Change(task_state, value));
            task_state = value;
        }

        public Worker Get_charger() { 
            return task_charger;
        }
        public void Set_сharger(Worker value) {
            Changes.Add(new Change(task_charger, value));
            task_charger = value;
        }

        public void Add_comment(string console_comments) => comments = console_comments;
    }
}