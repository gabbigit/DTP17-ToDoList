using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace DTP_D17_hemtentamen
{
    internal class Todo
    {
        public static List<TodoItem> todoList = new List<TodoItem>();

        public const int Active = 1;
        public const int Waiting = 2;
        public const int Ready = 3;

        public static string StatusToString(int status)
        {
            switch (status)
            {
                case Active: return "Active";
                case Waiting: return "Waiting";
                case Ready: return "Ready";
                default: return "(Wrong input)";
            }
        }

        //set / num/  /status/
        public class TodoItem
        {
            public int status;
            public int priority;
            public string task;

            public TodoItem(int priority, string task)
            {
                this.status = Active;
                this.priority = priority;
                this.task = task;
            }

            public TodoItem(string todoLine)
            {
                string[] field = todoLine.Split('|');
                status = Int32.Parse(field[0]);
                priority = Int32.Parse(field[1]);
                task = field[2];
            }
            public void Print()
            {
                string statusString = StatusToString(status);
                Console.Write($"|{statusString,-12}|{priority,-6}|{task,-20}|");
                Console.WriteLine();
            }

        }
        static public string ReadCommand(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
        static public bool CommandString(string inputCommand, string expected)
        {
            string command = inputCommand.Trim();
            if (command == "") return false;
            else
            {
                string[] cmdWords = command.Split(' ');
                if (cmdWords[0] == expected) return true;
            }
            return false;
        }

        static public bool HasArgument(string inputCommand, string expected)
        {
            string command = inputCommand.Trim();
            if (command == "") return false;
            else
            {
                string[] cmdWords = command.Split(' ');
                if (cmdWords.Length < 2) return false;
                if (cmdWords[1] == expected) return true;
            }
            return false;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the todo list, write 'help' for help!");
            string command;
            do
            {
                command = Todo.ReadCommand(">");
                if (Todo.CommandString(command, "help"))
                {
                    Console.WriteLine("help         - display this help text");
                    Console.WriteLine("load file    - load a todo list");
                    Console.WriteLine("list         - list the todo list");
                    Console.WriteLine("list ready   - list the todo list");
                    Console.WriteLine("list waiting - list the todo list");
                    Console.WriteLine("list active  - list the todo list");
                    Console.WriteLine("new          - add a new task to the list");
                    Console.WriteLine("save         - save new task to the todo list");
                    Console.WriteLine("quit         - quit the program");

                }
                else if (Todo.CommandString(command, "load"))
                {
                    Console.WriteLine("load file.lis");
                    ReadListFromFile();

                }
                else if (Todo.CommandString(command, "list"))
                {

                    foreach (TodoItem item in todoList)
                    {
                        item.Print();
                    }

                }
                else if (HasArgument(command, "list ready"))  //NYI
                {
                   foreach (TodoItem item in todoList)
                   {

                        if (Todo.Ready == 3)
                        {
                            item.Print();
                        }
                    }
                }
                else if (Todo.CommandString(command, "list waiting")) //NYI
                {

                    foreach (TodoItem item in todoList)
                    {
                        if (Todo.Waiting == 2)
                        {
                            item.Print();
                        }
                    }
                }
                else if (Todo.CommandString(command, "list active")) //NYI
                {

                    foreach (TodoItem item in todoList)
                    {
                        if (Todo.Active == 1)
                        {
                            item.Print();
                        }
                    }
                }

                else if (Todo.CommandString(command, "new"))
                {
                    Console.Write("Add a new todo item to the list: ");
                    Console.Write("Priority?");
                    int priority = Int32.Parse(Console.ReadLine());
                    Console.Write("Task?");
                    string task = Console.ReadLine();
                    Console.Write("Status of the task?");
                    int status = Int32.Parse(Console.ReadLine());
                    TodoItem item = new TodoItem(priority, task);
                    todoList.Add(item);
                    Console.WriteLine();
                    Console.WriteLine("Added new task to the list!");
                    Console.WriteLine();
                }
                else if (Todo.CommandString(command, "save"))
                {
                    Console.WriteLine("save file2.lis");
                    string todoFileName = "file2.lis";
                    StreamWriter sw = new StreamWriter(todoFileName);
                    {
                        foreach (TodoItem item in todoList)
                        {
                            if (item != null)
                            {
                                sw.WriteLine(item.ToString());
                            }
                        }
                    }

                }
                else
                {
                    Console.WriteLine("Goodbye!");
                }
            }
            while (command != "quit");
        }

        private static void ReadListFromFile()
        {
            string todoFileName = "file.lis";
            StreamReader sr = new StreamReader(todoFileName);
            int numRead = 0;

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                TodoItem item = new TodoItem(line);
                Todo.todoList.Add(item);
                numRead++;
            }
            sr.Close();
            Console.WriteLine($"Läste {numRead} rader.");
        }

    }
}