namespace SliceFile
{
    using System; 
    using System.IO;
    using System.Threading.Tasks;

    public class Startup
    {
        public static void Main()
        {
            Console.Write("Enter source path: ");
            var sourcePath = Console.ReadLine();

            Console.Write("Enter destination folder: ");
            var destinationPath = Console.ReadLine();

            Console.Write("Enter number of slices: ");
            var slices = int.Parse(Console.ReadLine());

            Task
                .Run(async () => await Slice(sourcePath, destinationPath, slices))
                .GetAwaiter()
                .GetResult();

            while (true)
            {
                Console.ReadLine();
            }
        }

        private static async Task Slice(string sourcePath, string destinationPath, int slices)
        {
            slices = Math.Max(1, slices);

            if (Directory.Exists(destinationPath))
            {
                Directory.Delete(destinationPath, true);
            }

            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            using (var source = new FileStream(sourcePath, FileMode.Open))
            {
                var fileInfo = new FileInfo(sourcePath);
                var sliceLength = (source.Length / slices) + 1;
                var currentByte = 0;

                Console.WriteLine($"Slicing file {fileInfo.FullName}...");

                for (int slice = 1; slice <= slices; slice++)
                {
                    var destinationFilePath = $"{destinationPath}/Slice-{slice}{fileInfo.Extension}";

                    using (var destination = new FileStream(destinationFilePath, FileMode.Create))
                    {
                        var buffer = new byte[1024];
                        while (currentByte <= sliceLength * slice)
                        {
                            var readBytes = await source.ReadAsync(buffer, 0, buffer.Length);
                            if (readBytes == 0)
                            {
                                break;
                            }

                            await destination.WriteAsync(buffer, 0, readBytes);
                            currentByte += readBytes;
                        }
                    }

                    Console.WriteLine($"Slice {slice}/{slices} ready.");
                }

                Console.WriteLine("Slicing complete.");
            }
        }
    }
}
