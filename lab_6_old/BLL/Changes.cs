using System;
using System.Collections.Generic;

namespace lab_6 {

    public class Change {
        public DateTime change_time;
        public (object, object) change_event;
        public Change(object state_before, object state_after) {
            change_time = DateTime.Now;
            change_event = (state_before, state_after);
        }
    }

    public class Report : ILists {

        private static int new_report_id = 0;
        private readonly int id;
        private readonly DateTime time;
        private readonly int worker_id;
        private readonly Report old;

        public int Iid => id;

        public int Charger_id => worker_id;

        public DateTime Time => time;

        public Dictionary<int, List<Change>> report_data = new Dictionary<int, List<Change>>();
        public Dictionary<int, List<Change>> Data => report_data;

        public Report(List<Report> reports) {
            for (int i = 0; i < reports.Count; i++) {
                Report report = reports[i];

                foreach (var data in report.Data) {
                    if (report_data.ContainsKey(data.Key)) {
                        report_data[data.Key].AddRange(data.Value);
                    } else {
                        report_data.Add(data.Key, data.Value);
                    }
                }
            }
        }

        public Report(int team_worker_id, List<Task_controller> tasks) {
            id = new_report_id++;
            time = DateTime.Now;
            worker_id = team_worker_id;
            for (int i = 0; i < tasks.Count; i++) {
                Task_controller task = tasks[i];
                report_data.Add(task.Iid, task.Changes);
            }
        }

        public Report(Report old_rep, int team_worker_id, List<Task_controller> tasks) {
            id = new_report_id++;
            time = DateTime.Now;
            this.old = old_rep;
            worker_id = team_worker_id;

            for (int i = 0; i < tasks.Count; i++) {
                Task_controller task = tasks[i];

                if (!old_rep.Data.ContainsKey(task.Iid)) {
                    report_data.Add(task.Iid, task.Changes);
                } else {
                    List<Change> more_changes = Get_changes(task.Iid, task.Changes);
                    if (more_changes.Count != 0) {
                        report_data.Add(task.Iid, more_changes);
                    }
                }
            }
        }

        private List<Change> Get_changes(int id, List<Change> changes) {
            List<Change> old_changes = old.Data[id];
            List<Change> all_changes = new List<Change>();

            for (int i = 0; i < changes.Count; i++) {
                Change change = changes[i];
                if (!old_changes.Contains(change)) {
                    all_changes.Add(change);
                }
            }
            return all_changes;
        }

        public void Write_results() {
            foreach (KeyValuePair<int, List<Change>> report in report_data) {
                Console.WriteLine($"Task №{report.Key}:");
                foreach (Change change in report.Value) {
                    Console.WriteLine($"{change.change_time} {change.change_event.Item1} next {change.change_event.Item2}");
                }
            }
            Console.WriteLine();
        }
    }
}