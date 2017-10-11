namespace BusBoard.Web.ViewModels
{
    public class Error
    {
        public Error(string error)
        {
            ErrorMessage = getError(error);
        }

        private string getError(string error)
        {
            switch (error)
            {
                case "0":
                    return "Invalid post code";
                case "1":
                    return "Invalid max distance set.";
                case "":
                    return "";
                default:
                    return "Unknown error occurred";
            }
        }

        public string ErrorMessage { get; set; }
    }
}