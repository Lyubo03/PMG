namespace PMG.App.Models
{
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;

    public class FileBindingModel
    {
        public IEnumerable<IFormFile> Files { get; set; }
    }
}