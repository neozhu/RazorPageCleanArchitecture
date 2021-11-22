namespace SmartAdmin.WebUI.Models
{






    public class SmartError
    {
        public string[][] Errors { get; set; } = Array.Empty<string[]>();

        public static SmartError Failed(params string[] errors) => new SmartError { Errors = new[] { errors } };
    }


}
