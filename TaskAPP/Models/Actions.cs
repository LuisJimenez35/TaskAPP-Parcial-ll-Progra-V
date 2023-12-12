using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskAPP.Models
{
    public class Actions : Controller
    {
        public async Task<ActionResult> Index(string name, string Description, string duedate, string priority)
        {
            if (Convert.ToDateTime(duedate) < System.DateTime.Now)
            {
                string message = "Escoja otra fecha";
            }
            else
            {

                string id = new Random().Next(1000, 9999).ToString();
                DocumentReference addedDocRef =
                        await FirestoreDb.Create(FirebaseAuthHelper.firebaseAppId)
                            .Collection("task").AddAsync(new Dictionary<string, object>
                                {
                                { "DocumentId", "" },
                                { "TaskId", id },
                                { "Title", name },
                                { "Description", Description },
                                { "DueDate", duedate },
                                {"IsCompleted","No"},
                                {"Priority" ,priority }
                                });

                FirestoreDb db = FirestoreDb.Create(FirebaseAuthHelper.firebaseAppId);
                DocumentReference docRef = db.Collection("task").Document(addedDocRef.Id);
                Dictionary<string, object> dataToUpdate = new Dictionary<string, object>
                {
                    { "DocumentId", addedDocRef.Id },
                };
                WriteResult result = await docRef.UpdateAsync(dataToUpdate);
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<ActionResult> UpdateTask(string docu, string name, string Description, string duedate, string priority)
        {
            try
            {
                if (Convert.ToDateTime(duedate) < System.DateTime.Now)
                {
                    string message = "The date is invalid";
                }
                else
                {
                    CollectionReference collection = FirestoreDb.Create(FirebaseAuthHelper.firebaseAppId).Collection("task");

                    DocumentReference document = collection.Document(docu);

                    var updatedData = new Dictionary<string, object>
                {
                    { "Tile", name },
                    { "Description", Description },
                    { "DueDate", duedate },
                    { "IsCompleted", "No" },
                    { "Priority", priority }
                };

                    await document.SetAsync(updatedData, SetOptions.MergeAll);

                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Somthing went wrong to try update: {ex.Message}";
                return View("Error");
            }

        }
        public async Task<ActionResult> CompleteTask(string docu)
        {
            try
            {


                CollectionReference collection = FirestoreDb.Create(FirebaseAuthHelper.firebaseAppId).Collection("task");

                DocumentReference document = collection.Document(docu);

                var updatedData = new Dictionary<string, object>
                    {
                        { "isComplete", "Yes" },
                    };

                await document.SetAsync(updatedData, SetOptions.MergeAll);

                return RedirectToAction("Index", "Home");

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error al actualizar la tarea: {ex.Message}";
                return View("Error");
            }

        }

        public async Task<ActionResult> DeleteTask(string docu)
        {
            CollectionReference collection = FirestoreDb.Create(FirebaseAuthHelper.firebaseAppId).Collection("task");

            DocumentReference document = collection.Document(docu);
            await document.DeleteAsync();
            return RedirectToAction("Index", "Home");
        }

        // POST: Functions/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

