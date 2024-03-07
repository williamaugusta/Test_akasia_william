using System.Globalization;
using Test_Akasia.Helper.Service;
using Test_Akasia.Helper.Model;
using ConsoleTables;

namespace Test_Akasia.Helper.Service
{
        public class Output
        {
        //Format tipe pesan
            public static void Message(string message, Message.Type type)
            {
                switch (type)
                {
                    case Model.Message.Type.Success:
                        break;
                    case Model.Message.Type.Warning:
                        break;
                    case Model.Message.Type.Error:
                        break;
                    case Model.Message.Type.Info:
                        break;
                    case Model.Message.Type.Default:
                        break;
                }

                Console.WriteLine(message);
                Console.ForegroundColor = ConsoleColor.Gray;
            }

        public class ModelEmployee
        {
            // digunakan untuk employee id dengan string leght 4 digit
            int employeeCounter = 1000;

            // deklarasi employee di model
            List<Employee> Employees { get; set; }

            //digunakan untuk menyimpan data dalam model
            public ModelEmployee()
            {                
                Employees = new List<Employee>();
            }

            // function untuk menambah employee
            public void CreateEmployee()
            {
                try
                {
                    Console.Clear();
                    employeeCounter++;
                    Employees.Add(new Employee
                    {
                        EmployeeId = employeeCounter.ToString(),
                        FullName = GetFullName(),
                        BirthDate = GetBirthDate()
                    });

                    Console.Clear();

                    Employee employee = Employees.Where(o => o.EmployeeId.Equals(employeeCounter.ToString())).FirstOrDefault();
                    if (employee != null)
                    {
                        Output.Message("Create new employee Success:", Model.Message.Type.Success);
                        ShowAllEmployees();
                    }
                    else
                    {
                        Output.Message("Create new employee failed", Model.Message.Type.Error);
                        employeeCounter--;
                    }
                }
                catch (Exception ex)
                {
                    Output.Message($"Error: {ex.Message}", Model.Message.Type.Error);
                }
            }

            // Digunakan untuk menampilkan semua data employee
            public void ListEmployee()
            {
                Console.Clear();
                Output.Message("View All Data Employee:", Model.Message.Type.Default);
                ShowAllEmployees();
            }

            //Digunakan untuk menampilkan semua data dengan table 
            public void ShowAllEmployees()
            {
                var table = new ConsoleTable("EmployeeId", "FullName", "BirthDate");
                foreach (var a in Employees)
                {
                    table.AddRow(a.EmployeeId, a.FullName, a.BirthDate.ToString("dd-MMM-yyyy"));
                }
                table.Write();

            }

            // Digunakan untuk menghapus data employee 
            public void DeleteEmployee()
            {
                Console.Clear();
                try
                {

                    ShowAllEmployees();
                    Output.Message(" Pleaser insert EmployeeId you want to delete: ", Model.Message.Type.Default);

                    Employee employee = GetEmployee();

                    if (employee != null)
                    {
                        bool remove = Employees.Remove(employee);
                        if (remove)
                        {
                            Console.Clear();
                            Output.Message($"Success, delete data employee {employee.EmployeeId} - {employee.FullName}.", Model.Message.Type.Success);
                            ShowAllEmployees();
                        }
                        else
                        {
                            Output.Message("Failed, delete employee. Please try again.", Model.Message.Type.Warning);
                        }
                    }
                    else
                    {
                        Output.Message($"EmployeeId doesn't exist. Please try again.", Model.Message.Type.Warning);
                    }
                }
                catch (Exception ex)
                {
                    Output.Message($"Error: {ex.Message}", Model.Message.Type.Error);
                }
            }

            // Digunakan untuk update data
            public void UpdateEmployee()
            {
                Console.Clear();
                try
                {
                    ShowAllEmployees();
                    Output.Message("Pleaser insert EmployeeId you want to update: ", Model.Message.Type.Default);

                    Employee employee = GetEmployee();

                    if (employee != null)
                    {
                        employee.FullName = GetFullName();
                        employee.BirthDate = GetBirthDate();

                        Console.Clear();
                        Output.Message($"Successfully update employee {employee.EmployeeId} - {employee.FullName}.", Model.Message.Type.Success);
                        ShowAllEmployees();
                    }
                    else
                    {
                        Output.Message($"EmployeeId doesn't exist. Please try again.", Model.Message.Type.Warning);
                    }
                }
                catch (Exception ex)
                {
                    Output.Message($"Error: {ex.Message}", Model.Message.Type.Error);
                }
            }

            // Digunakan untuk mendapatkan 1 data untuk keperluan hapus atau edit data          
            public Employee GetEmployee()
            {
                Employee employee = new Employee();
                string EmployeeId = string.Empty;
                bool IsValidEmployeeId;
                bool IsAnyEmployee;

                do
                {
                    IsValidEmployeeId = false;
                    IsAnyEmployee = false;

                    EmployeeId = Console.ReadLine();

                    if (string.IsNullOrEmpty(EmployeeId))
                    {
                        Output.Message("EmployeeId must be filled. Please try again.", Model.Message.Type.Warning);
                    }
                    else
                    {
                        IsValidEmployeeId = int.TryParse(EmployeeId, CultureInfo.InvariantCulture, out int EmpId);
                        if (!IsValidEmployeeId)
                        {
                            Output.Message("EmployeeId is invalid. Please try again.", Model.Message.Type.Warning);
                        }
                        else
                        {
                            IsAnyEmployee = Employees.Any(o => o.EmployeeId.Equals(EmployeeId.ToString()));
                            if (IsAnyEmployee)
                            {
                                employee = Employees.FirstOrDefault(o => o.EmployeeId.Equals(EmployeeId.ToString()));
                            }
                            else
                            {
                                Output.Message($"Employee doesn't exist with id {EmployeeId}. Please try again.", Model.Message.Type.Warning);
                            }
                        }
                    }
                } while (string.IsNullOrEmpty(EmployeeId) || !IsValidEmployeeId || !IsAnyEmployee);

                return employee;
            }

            // digunakan untuk membaca dan masukan pengguna beserta validasi input tanggal
            public DateTime GetBirthDate()
            {
                string strBirthDate = string.Empty;
                DateTime birthDate = DateTime.MinValue;
                bool isValidDate = false;

                Output.Message("Enter birth date (dd/MM/yyyy): ", Model.Message.Type.Default);
                do
                {
                    strBirthDate = Console.ReadLine();
                    if (string.IsNullOrEmpty(strBirthDate))
                    {
                        Output.Message("Birth date must be filled. Please enter date in (dd/MM/yyyy) format.", Model.Message.Type.Default);
                    }
                    else
                    {
                        isValidDate = DateTime.TryParseExact(strBirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);
                        if (!isValidDate)
                        {
                            Output.Message("Invalid date format. Please enter date in (dd/MM/yyyy) format.", Model.Message.Type.Warning);
                        }
                    }
                } while (string.IsNullOrEmpty(strBirthDate) || !isValidDate);
                return birthDate;
            }

            // Digunakan untuk membaca dan masukan pengguna
            public string GetFullName()
            {
                string fullName = string.Empty;
                bool IsExist = false;

                Output.Message("Enter full name: ", Model.Message.Type.Default);
                do
                {
                    IsExist = false;
                    fullName = Console.ReadLine();
                    if (string.IsNullOrEmpty(fullName))
                    {
                        Output.Message("Full name must be filled. Please try again.", Model.Message.Type.Warning);
                    }
                    else if (IsDuplicate(fullName))
                    {
                        IsExist = true;
                        Output.Message("Full name already registered. Please try again.", Model.Message.Type.Warning);
                    }
                } while (string.IsNullOrEmpty(fullName) || IsExist);
                return fullName;
            }

            // Digunakan untuk pengecekan adanya duplikat nama
            public bool IsDuplicate(string fullName)
            {
                return Employees.Any(e => e.FullName.ToLower() == fullName.ToLower());
            }
        }

    }
}
