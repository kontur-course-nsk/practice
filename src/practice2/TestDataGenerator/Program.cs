namespace TestDataGenerator
{
    public class Program
    {
        /// <summary>
        /// ������� ���� � �������� �������� ������
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            EventLogGenerator.Generate("catalog.csv");
        }
    }
}
