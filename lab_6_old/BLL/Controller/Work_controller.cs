using System;
using System.Collections.Generic;

namespace lab_6 {

    public class Work_controller {

        private readonly IInformation tasks_list;
        private readonly IInformation reports_list;

        private readonly System.Timers.Timer time_for_work;
        
        public Work_controller(string console_tasks, string console_reports) {
            tasks_list = new Lists(console_tasks);
            reports_list = new Lists(console_reports);

            time_for_work = new System.Timers.Timer {
                Interval = 3000
            };
            time_for_work.Elapsed += Event_time;
            time_for_work.Enabled = true;
            time_for_work.AutoReset = true;
        }

        public int Add_task(string name, string description) {
            Task_controller task = new Task_controller(name, description);
            tasks_list.Add(task);

            Console.WriteLine($"{task.id} -> {((Task_controller)tasks_list.Get_id(task.id)).Get_state()}");
            return task.id;
        }

        public void Appoint_worker(int task_id, int worker_id) {
            Task_controller task = (Task_controller)tasks_list.Get_id(task_id);
            if (task == null) {
                Console.Error.WriteLine("No such task");
                return;
            } else if (!Worker.project_team_list.ContainsKey(worker_id)) {
                Console.Error.WriteLine("No such worker");
                return;
            }
            task.Set_сharger(Team.Get_team_id(worker_id));
            
            tasks_list.Every_edit_task(task);
        }

        public void Edit_description(int task_id, string new_description) {
            Task_controller task = (Task_controller)tasks_list.Get_id(task_id);
            if (task == null) {
                Console.Error.WriteLine("No such task");
                return;
            } else
            {
                task.Set_description(new_description);
                tasks_list.Every_edit_task(task);
            }
        }
        public void Edit_task_name(int task_id, string new_name) {
            Task_controller task = (Task_controller)tasks_list.Get_id(task_id);
            if (task == null) {
                Console.Error.WriteLine("No such task");
                return;
            } else {
                task.Set_task_name(new_name);
                tasks_list.Every_edit_task(task);
            }
        }
        public void Edit_main_state(int task_id, All_states new_state) {
            Task_controller task = (Task_controller)tasks_list.Get_id(task_id);
            if (task == null) {
                Console.Error.WriteLine("No such task");
                return;
            } else {
                task.Set_state(new_state);
                tasks_list.Every_edit_task(task);
            }
        }
        public void Add_new_comment(int task_id, string new_comment) {
            Task_controller task = (Task_controller)tasks_list.Get_id(task_id);
            if (task == null) {
                Console.Error.WriteLine("No such task");
                return;
            } else {
                task.Add_comment(new_comment);
                tasks_list.Every_edit_task(task);
            }
        }

        public List<Report> Get_sprint_reports() {

            List<ILists> reports = reports_list.Get_tasks_lists();
            List<Report> result = new List<Report>();

            for (int i = 0; i < reports.Count; i++) {
                result.Add(reports[i] as Report);
            }
            return result;
        }
        public void Sprint() {
            Report report = new Report(Get_sprint_reports());
            reports_list.Add(report);
            time_for_work.Enabled = false;
        }

        private void Event_time(object source, System.Timers.ElapsedEventArgs e) {
            Simple_report();
        }



        public List<Task_controller> Get_simple_reports(int team_workers) {

            List<Task_controller> tasks = new List<Task_controller>();
            for (int i = 0; i < tasks_list.Get_tasks_lists().Count; i++) {
                tasks.Add(tasks_list.Get_tasks_lists()[i] as Task_controller);
            }

            List<Task_controller> result = new List<Task_controller>();
            for (int i = 0; i < tasks.Count; i++) {
                if ((tasks[i] as Task_controller).Get_charger() != null && (tasks[i] as Task_controller).Get_charger().id == team_workers) {
                    result.Add(tasks[i] as Task_controller);
                }
            }
            return result;
        }

        public void Simple_report() {

            Console.WriteLine("\nSimple reports\n");
            List<Worker> workers_list = Team.Get_workers();

            for (int i = 0; i < workers_list.Count; i++) {

                Worker person = workers_list[i];
                //int person.id;
                Worker team_workers = Team.Get_team_id(person.id);
                HashSet<int> T = new HashSet<int> {
                person.id
            };

                foreach (var item in team_workers.workers_list) {
                    T.Add(item.Key);
                }

                List<Report> reports = Get_sprint_reports();

                for (int l = 0; l < reports.Count; l++) {
                    if (T.Contains(reports[l].Charger_id)) {
                        reports.Add(reports[l]);
                    }
                }

                if (reports.Count == 0) {

                    reports_list.Add(new Report(person.id, Get_simple_reports(person.id)));

                } else {

                    DateTime last = DateTime.MinValue;

                    for (int j = 0; j < reports.Count; j++) {
                        if (reports[j].Time > last) {
                            last = reports[j].Time;
                        }
                    }

                    Report report = new Report(reports.Find(r => {

                        if (r is null) {
                            Console.Error.WriteLine("Report NULL error");
                        }

                        return r.Time == last;
                    }), person.id, Get_simple_reports(person.id));
                    reports_list.Add(report);
                }
            }
        }
    }
}