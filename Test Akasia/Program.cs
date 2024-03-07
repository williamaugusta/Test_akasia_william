
using System.Globalization;
using Test_Akasia.Helper.Model;
using Test_Akasia.Helper.Service;

namespace Test_Akasia
{
    class Program
    {
        static Helper.Service.Output.ModelEmployee employeeService = new Helper.Service.Output.ModelEmployee();

        static void Main(string[] args)
        {
            while (true)
            {
                // Memberikan informasi yang harus dilakukan              
                Output.Message("Welcome CRUD Application", Message.Type.Default);
                Output.Message("1. Create Employee", Message.Type.Default);
                Output.Message("2. Update Employee", Message.Type.Default);
                Output.Message("3. Delete Employee", Message.Type.Default);
                Output.Message("4. List Employees", Message.Type.Default);
                Output.Message("5. Exit", Message.Type.Default);
                Output.Message(" Please select number 1-5: ", Message.Type.Default);

                // deklarasi variable untuk user input
                string? strChoice;
                bool IsValidNumber;
                do
                {
                    try
                    {
                        // membaca user menulis apa
                        strChoice = Console.ReadLine();

                        // validasi yang diinput angka atau bukan
                        IsValidNumber = int.TryParse(strChoice, out int choice);
                        if (IsValidNumber)
                        {
                            switch (choice)
                            {
                                case 1:
                                    // membuat employee baru
                                    employeeService.CreateEmployee();
                                    break;
                                case 2:
                                    // update employee yang sudah ada di model
                                    employeeService.UpdateEmployee();
                                    break;
                                case 3:
                                    // menghapus employee yang ada sesuai id
                                    employeeService.DeleteEmployee();
                                    break;
                                case 4:
                                    // memunculkan semua employee yang sudah dimasukan
                                    employeeService.ListEmployee();
                                    break;
                                case 5:
                                    // untuk mengakhiri penggunaan aplikasi
                                    return;
                                default:
                                    // mengirimkan pesan cara penggunaan aplikasi
                                    Output.Message("Please Input number 1-5.", Message.Type.Warning);
                                    IsValidNumber = false;
                                    break;
                            }
                        }
                        // Jika user tekan enter tanpa input apa pun akan muncul pesan
                        else if (string.IsNullOrEmpty(strChoice))
                        {
                            Output.Message("Please enter your number.", Message.Type.Warning);
                        }
                        // Jika user menekan angka tapi tidak terdaftar 1-5
                        else
                        {
                            Output.Message("Invalid number. Please try again.", Message.Type.Warning);
                        }
                    }
                    // jika program membaca ada error
                    catch (Exception ex)
                    {
                        Output.Message($"Error: {ex.Message}", Message.Type.Error);
                        IsValidNumber = false;
                    }
                } while (!IsValidNumber);
            }
        }
    }
}
