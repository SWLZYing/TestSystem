using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace TestSystem.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var isDoing = true;

            while (isDoing)
            {
                HomeMsg();

                var into = Console.ReadLine();

                switch (into)
                {
                    case "1":
                        CreateUser();
                        break;

                    case "2":
                        SignIn();
                        break;

                    case "3":
                        QueryUser();
                        break;

                    case "4":
                        UpdateUser();
                        break;

                    case "5":
                        ResetPwd();
                        break;

                    case "6":
                        SignOut();
                        break;

                    case "7":
                    default:
                        isDoing = false;
                        break;
                }
            }

            Console.WriteLine("離開程式");
            Console.ReadLine();
        }

        private static void SignOut()
        {
            Console.WriteLine("帳號登出");
            Console.WriteLine("輸入帳號ID:");
            var id = Console.ReadLine();

            var response = CallApi("Sign/SignOut", new Dictionary<string, object>
            {
                { "AccId", Convert.ToInt32(id) },
            });

            Console.WriteLine(response);
        }

        private static void ResetPwd()
        {
            Console.WriteLine("密碼修改");
            Console.WriteLine("輸入帳號ID:");
            var id = Console.ReadLine();
            Console.WriteLine("輸入原密碼:");
            var oldPwd = Console.ReadLine();
            Console.WriteLine("輸入新密碼:");
            var pwd = Console.ReadLine();

            var response = CallApi("Account/ResetPwd", new Dictionary<string, object>
            {
                { "Id", Convert.ToInt32(id) },
                { "OldPwd", oldPwd },
                { "Pwd", pwd },
            });

            Console.WriteLine(response);
        }

        private static void UpdateUser()
        {
            Console.WriteLine("資料修改");
            Console.WriteLine("輸入帳號ID:");
            var id = Console.ReadLine();
            Console.WriteLine("輸入原密碼(不修改跳過):");
            var oldPwd = Console.ReadLine();
            Console.WriteLine("輸入新密碼(不修改跳過):");
            var pwd = Console.ReadLine();
            Console.WriteLine("輸入暱稱:");
            var nickname = Console.ReadLine();

            var response = CallApi("Account/UpdateUser", new Dictionary<string, object>
            {
                { "Id", Convert.ToInt32(id) },
                { "OldPwd", oldPwd },
                { "Pwd", pwd },
                { "Nickname", nickname },
            });

            Console.WriteLine(response);
        }

        private static void QueryUser()
        {
            Console.WriteLine("帳號查詢");
            Console.WriteLine("輸入帳號ID:");
            var id = Console.ReadLine();

            var response = CallApi("Account/QueryUser", new Dictionary<string, object>
            {
                { "AccId", Convert.ToInt32(id) },
            });

            Console.WriteLine(response);
        }

        private static void SignIn()
        {
            Console.WriteLine("帳號登入");
            Console.WriteLine("輸入帳號:");
            var acc = Console.ReadLine();
            Console.WriteLine("輸入密碼:");
            var pwd = Console.ReadLine();

            var response = CallApi("Sign/SignIn", new Dictionary<string, object>
            {
                { "Acc", acc },
                { "Pwd", pwd },
            });

            Console.WriteLine(response);
        }

        private static void CreateUser()
        {
            Console.WriteLine("帳號建立");
            Console.WriteLine("輸入帳號:");
            var acc = Console.ReadLine();
            Console.WriteLine("輸入密碼:");
            var pwd = Console.ReadLine();
            Console.WriteLine("輸入暱稱:");
            var nickname = Console.ReadLine();

            var response = CallApi("Account/CreateUser", new Dictionary<string, object>
            {
                { "Acc", acc },
                { "Pwd", pwd },
                { "Nickname", nickname },
            });

            Console.WriteLine(response);
        }

        private static string CallApi(string action, Dictionary<string, object> parameters)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:53172/Api/");

                    var request = new HttpRequestMessage(HttpMethod.Post, action)
                    {
                        Content = new StringContent(JsonSerializer.Serialize<object>(parameters), Encoding.UTF8, "application/json")
                    };

                    var response = client.SendAsync(request).GetAwaiter().GetResult();

                    if (response.IsSuccessStatusCode)
                    {
                        //取回傳值
                        return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    }
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static void HomeMsg()
        {
            Console.WriteLine("請選擇執行項目");
            Console.WriteLine("1: 帳號建立");
            Console.WriteLine("2: 帳號登入");
            Console.WriteLine("3: 帳號查詢");
            Console.WriteLine("4: 資料修改");
            Console.WriteLine("5: 密碼修改");
            Console.WriteLine("6: 帳號登出");
            Console.WriteLine("7: 離開");
            Console.WriteLine("請輸入編號:");
        }
    }
}
