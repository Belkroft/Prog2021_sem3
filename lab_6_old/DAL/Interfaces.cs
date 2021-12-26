using System;
using System.Collections.Generic;

namespace lab_6
{
    public interface ILists { // For tasks and reports
        int Iid { get; }
    }
    public interface IInformation {
        void Add(ILists i);
        void Every_edit_task(ILists i);
        ILists Get_id(int id);
        List<ILists> Get_tasks_lists();
    }

    public class Lists : IInformation {

        private readonly string path;
        private readonly List<ILists> tasks_list = new List<ILists>();

        public Lists(string console_info) => path = console_info;
        public void Add(ILists task) => tasks_list.Add(task);

        public void Every_edit_task(ILists task) {

            if (!tasks_list.Contains(task)) {
                Console.Error.WriteLine("No such entry");
                return;
            }
            else tasks_list[tasks_list.IndexOf(tasks_list.Find(find_task =>
              find_task.Iid == task.Iid))] = task; // Entry
        }

        public ILists Get_id(int id) => tasks_list.Find(arg => arg.Iid == id);

        public List<ILists> Get_tasks_lists() => tasks_list;
    }
}