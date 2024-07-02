namespace Turnit.arguments;

public class Arguments
{
    string filename;

    public Arguments(string[] args)
    {
        filename = GetFilenameFromArgs(args);
        bool fileExists = File.Exists(filename);

        if (!fileExists) CLIFileNotFound();
    }

    string GetFilenameFromArgs(string[] args)
    {
        string lastArgument = "";
        string filename = "";

        // Find argument called filename, assume the next argument is its value
        foreach (string arg in args)
        {
            if (lastArgument.Equals("filename"))
            {
                filename = arg;
            }
            lastArgument = arg;
        }

        return filename;
    }

    public string GetFilename()
    {
        return filename;
    }

    void CLIFileNotFound()
    {
        Console.WriteLine("Could not find the file at " + filename);

        while (true)
        {
            Console.Write("Enter a new file path or type 'n' to continue: ");
            var input = Console.ReadLine();

            if (input == "n") break;

            bool fileExists = File.Exists(input);
            if (fileExists)
            {
                filename = input;
                break;
            }
        }
    }
}
