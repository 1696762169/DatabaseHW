namespace DatabaseHW
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder(args)  // ����WebӦ�ó��򹹽���
                .UseStartup<Startup>()  // Ӧ�ó�ʼ����
                .Build()    // ����WebӦ�ó���
                .Run(); // ����WebӦ�ó���
        }
    }
}