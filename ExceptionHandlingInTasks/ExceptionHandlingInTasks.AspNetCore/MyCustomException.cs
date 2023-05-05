namespace ExceptionHandlingInTasks.AspNetCore
{
    public class MyCustomException : Exception
    {
        public string ExtraName { get; set; }

        public MyCustomException(string extraName):base("I am custom exception")
        {
            ExtraName = extraName;
        }
    }
}
