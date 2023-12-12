using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using TaskAPP.Models;

namespace TaskAPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        private static FirestoreDb firestoreDb;
        public async Task<ActionResult> Index()
        {
            List<Tasks> tasksList = new List<Tasks>();
            Query query = FirestoreDb.Create(FirebaseAuthHelper.firebaseAppId).Collection("task");


            if (query != null)
            {
                QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

                if (querySnapshot.Documents.Count > 0)
                {
                    foreach (var document in querySnapshot.Documents)
                    {
                        Dictionary<string, object> data = document.ToDictionary();

                        Tasks task = new Tasks
                        {
                            TaskId = Convert.ToInt32(data["TaskId"].ToString()),
                            Title = data["Title"].ToString(),
                            Description = data["Description"].ToString(),
                            DueDate = Convert.ToDateTime(data["DueDate"].ToString()),
                            IsCompleted = Convert.ToBoolean(data["IsCompleted"].ToString()),
                            Priority = data["Priority"].ToString()
                        };
                        tasksList.Add(task);

                    }
                    ViewBag.TaskData = tasksList;

                    return View();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<ActionResult> GetTask(int id)
        {
            List<Tasks> tasksList = new List<Tasks>();
            Query query = FirestoreDb.Create(FirebaseAuthHelper.firebaseAppId).Collection("task").WhereEqualTo("ID", id);


            if (query != null)
            {
                QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

                if (querySnapshot.Documents.Count > 0)
                {
                    foreach (var document in querySnapshot.Documents)
                    {
                        Dictionary<string, object> data = document.ToDictionary();

                        Tasks task = new Tasks
                        {
                            TaskId = Convert.ToInt32(data["TaskId"].ToString()),
                            Title = data["Title"].ToString(),
                            Description = data["Description"].ToString(),
                            DueDate = Convert.ToDateTime(data["DueDate"].ToString()),
                            IsCompleted = Convert.ToBoolean(data["IsCompleted"].ToString()),
                            Priority = data["Priority"].ToString()
                        };
                        tasksList.Add(task);

                    }
                    ViewBag.TaskData = tasksList;

                    return View("UpdateTask");
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<ActionResult> GetTaskByParameter(string parameter)
        {
            List<Tasks> tasksList = new List<Tasks>();
            Query query1 = FirestoreDb.Create(FirebaseAuthHelper.firebaseAppId).Collection("task").WhereEqualTo("Name", parameter);
            Query query2 = FirestoreDb.Create(FirebaseAuthHelper.firebaseAppId).Collection("task").WhereEqualTo("Description", parameter);


            if (query1 != null && query2 != null)
            {
                QuerySnapshot querySnapshot1 = await query1.GetSnapshotAsync();
                QuerySnapshot querySnapshot2 = await query2.GetSnapshotAsync();

                if (querySnapshot1.Documents.Count > 0)
                {
                    foreach (var document in querySnapshot1.Documents)
                    {
                        Dictionary<string, object> data = document.ToDictionary();

                        Tasks task = new Tasks
                        {
                            TaskId = Convert.ToInt32(data["TaskId"].ToString()),
                            Title = data["Title"].ToString(),
                            Description = data["Description"].ToString(),
                            DueDate = Convert.ToDateTime(data["DueDate"].ToString()),
                            IsCompleted = Convert.ToBoolean(data["IsCompleted"].ToString()),
                            Priority = data["Priority"].ToString()
                        };
                        tasksList.Add(task);

                    }
                    ViewBag.TaskData = tasksList;

                    return View("Index");
                }
                else if (querySnapshot2.Documents.Count > 0)
                {
                    foreach (var document in querySnapshot2.Documents)
                    {
                        Dictionary<string, object> data = document.ToDictionary();

                        Tasks task = new Tasks
                        {
                            TaskId = Convert.ToInt32(data["TaskId"].ToString()),
                            Title = data["Title"].ToString(),
                            Description = data["Description"].ToString(),
                            DueDate = Convert.ToDateTime(data["DueDate"].ToString()),
                            IsCompleted = Convert.ToBoolean(data["IsCompleted"].ToString()),
                            Priority = data["Priority"].ToString()
                        };
                        tasksList.Add(task);
                    }
                    ViewBag.TaskData = tasksList;

                    return View("Index");
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult CreateTask()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
