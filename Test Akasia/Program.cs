
using System.Globalization;
using Test_Akasia.Helper.Model;
using Test_Akasia.Helper.Service;
using static System.Net.Mime.MediaTypeNames;

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
                Console.WriteLine("Welcome CRUD Application");
                Console.WriteLine("1.Create");
                Console.WriteLine("2.Update");
                Console.WriteLine("3.Delete");
                Console.WriteLine("4.Show All Data");
                Console.WriteLine("5.Exit");
                Console.WriteLine("Please select number 1-5:");

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
