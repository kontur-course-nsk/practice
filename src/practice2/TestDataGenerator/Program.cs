namespace TestDataGenerator
{
    public class Program
    {
        /// <summary>
        /// Запусти меня и получишь тестовый файлик
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            EventLogGenerator.Generate("catalog.csv");
        }
    }
}
