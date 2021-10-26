using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainingHashTable
{
    class Program
    {    

        // Class representing Employees as objects to enter into the hash table
        class Employee
        {
            private string firstName;
            private string lastName;
            private int id;

            // Since separate chaining requires a linked list, there needs to be a pointer
            // to the next employee
            public Employee next;

            // Constructor 
            public Employee(string f, string l, int i)
            {
                this.firstName = f;
                this.lastName = l;
                this.id = i;
                next = null;
            }

            // Functions to allow users to see information about each employee in the hash table
            public string getFirstName()
            {
                return this.firstName;
            }

            public string getLastName()
            {
                return this.lastName;
            }

            public int getID()
            {
                return this.id;
            }
        }


        // Hash table class
        class HashTable
        {
            // The number of indices for the table to contain
            private int indices;
            private Employee [] table;

            // Constructor to initialize the hash table
            public HashTable(int i)
            {
                indices = i;
                table = new Employee[indices];
            }

            // Hash function to assign each Employee an index based on the key value
            public int hashFunction(int ky)
            {
                return ky % indices;
            }

            // Function to insert an employee into the hash table
            public void insert(string firstNm, string lastNm, int id)
            {

                // Assign an employee to insert an index
                int index = hashFunction(id);

                // If the index is empty, just perform a simple insertion
                if (table[index] == null)
                    table[index] = new Employee(firstNm, lastNm, id);
                else // Otherwise perform the chaining function if the index is occupied
                {
                    
                    Employee current = table[index];

                    // Traverse to the end of the linked list at the assigned index
                    while (current.next != null)
                        current = current.next;

                    // Insertion via chaining
                    current.next = new Employee(firstNm, lastNm, id);
                }
            }

            // Function to remove an employee based on the key value
            public void delete(int key)
            {
                // Locate the index where the key may be located
                int index = hashFunction(key);

                Employee current = table[index];

                // If the index is empty, do not perform the deletion
                if (current == null)
                    return;
                else
                {
                    // Find the key inside the linked list at 
                    bool keyFound = false;

                    while (current.next != null)
                    {
                        if(current.next.getID() != key)
                            current = current.next;
                        else // if the key is found, stop looping
                        {
                            keyFound = true;
                            break;
                        }
                    }

                    // Perform the deletion only if the key is found
                    if (keyFound == true)
                    {
                        Employee temp = current.next.next;
                        current.next = temp;
                    }
                    else
                        Console.WriteLine("An employee with the ID: " + key + " does not exist in the table");
                }
            }

            // Function to output the hash table
            public void outputTable()
            {
                for(int i = 0; i < table.Length; i++)
                {
                    if (table[i] == null)
                        Console.WriteLine(i + ":  " + "No entry");
                    else
                    {
                        for (Employee current = table[i]; current != null; current = current.next)
                        {
                            Console.WriteLine(i + ":  " + current.getFirstName() + " "
                                + current.getLastName() + " " + current.getID());
                        }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            HashTable ht = new HashTable(10);

            ht.insert("Priscilla", "Betti", 19);
            ht.insert("Lorie", "Pester", 16);
            ht.insert("Magnus", "Carlsen", 21);
            ht.insert("Tifa", "Lockhart", 11);
            ht.insert("Julie", "Zenatti", 18);
            ht.insert("Hikaru", "Nakamura", 9);

            ht.delete(11);

            ht.outputTable();
        }
    }
}
