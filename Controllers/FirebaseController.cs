using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirebaseAdmin;
using Firebase.Storage;


namespace Assignment.Controllers
{
    [ApiController]
    [Route("upload")]
    public class FirebaseController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");
            var task = await new FirebaseStorage("imdb-application.appspot.com")
                    .Child("images")
                    .Child(Guid.NewGuid().ToString() + ".jpg")
                    .PutAsync(file.OpenReadStream());
            return Ok(task);
        }


    }
}
