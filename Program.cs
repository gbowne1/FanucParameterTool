using System;
using System.Collections.Generic; // For List<T>
using System.Runtime.Serialization.Formatters.Binary;

namespace FanucParameterTool
{
    public class Parameter
    {
        public string ParameterNumber { get; set; } = ""; // Initialize with empty string
    	public string Value { get; set; } = ""; // Initialize with empty string
    	public string Description { get; set; } = ""; // Initialize with null (optional)
    }

    public class MachineParameters
    {
        public List<Parameter> parameters;

        public MachineParameters()
        {
            parameters = new List<Parameter>();
        }

        public void LoadConfiguration(string filename)
        {
            // Implement logic to load parameter descriptions from a configuration file (optional)
			if (string.IsNullOrEmpty(filename))
			{
				Console.WriteLine("Error: Configuration filename cannot be empty.");
				return;
			}

			// Extract model type from filename (assuming format "model_config.txt")
			string modelType = Path.GetFileNameWithoutExtension(filename).Split('_')[0];

			if (!validModels.Contains(modelType.ToUpper()))
			{
				Console.WriteLine($"Warning: Unsupported model type '{modelType}' in configuration file.");
				return;
			}

			// Implement logic to read parameter descriptions from the configuration file (optional)
			// This could involve parsing the file format (e.g., key-value pairs) and storing descriptions in the Parameter objects
			Console.WriteLine($"**Note:** Configuration loading for parameter descriptions not fully implemented.");
		}

        public void GetUserInput(string parameterNumber)
        {
            // Implement logic to display parameter information, prompt user for value, and handle validation
        }

        public void SaveToFile(string filename, string format)
        {
            // Implement logic to save parameter data to a file in the chosen format (text, binary)
			if (format.ToLower() == "text")
			{
				SaveTextFile(filename);
			}
			else if (format.ToLower() == "binary")
			{
				SaveBinaryFile(filename);
			}
			else if (format.ToLower() == "raw text")
			{
				SaveRawTextFile(filename);
			}
			else
			{
				Console.WriteLine($"Error: Unsupported format '{format}'.");
			}
        }

		private void SaveTextFile(string filename)
		{
			// Open a StreamWriter for text output
			using (StreamWriter writer = new StreamWriter(filename))
 			{
				// Iterate through parameters and write each parameter in a specific format (e.g., parameter number:value)
				foreach (Parameter parameter in parameters)
				{
					writer.WriteLine($"{parameter.ParameterNumber}: {parameter.Value}");
				}
			}

			Console.WriteLine($"Parameter data saved to text file: {filename}");
		}

		private void SaveBinaryFile(string filename)
		{
			// Consider using a serialization library like System.Runtime.Serialization.Formatters.Binary
			// This approach requires converting the Parameter objects to a suitable binary format.
			//
			// Example (incomplete, requires implementation):
			//
			// using System.Runtime.Serialization.Formatters.Binary;
			//
			// ... open a FileStream in binary write mode ...
			//
			// BinaryFormatter formatter = new BinaryFormatter();
			// formatter.Serialize(stream, parameters); // Serialize the parameter list

			Console.WriteLine($"**Warning:** Binary format saving not fully implemented.");
		}

		private void SaveRawTextFile(string filename)
		{
			// Similar to SaveTextFile, but without line breaks
			using (StreamWriter writer = new StreamWriter(filename))
			{
				foreach (Parameter parameter in parameters)
				{
					writer.Write($"{parameter.ParameterNumber}:{parameter.Value}");
				}
			}

			Console.WriteLine($"Parameter data saved to raw text file: {filename}");
		}
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            MachineParameters parameters = new MachineParameters();

            // Model and type selection with configuration loading (optional)
            // parameters.LoadConfiguration("config.txt"); // Example usage

            // Define valid model types (including user-defined)
            List<string> validModels = new List<string>
            {
                "0M", "0T", "3M", "3T", "5M", "5T", "6M", "6T", "7M", "7T",
                "10M", "10T", "11M", "11T", "12M", "12T", "15M", "15T", "16M", "16T",
                "18M", "18T", "21M", "21T", "0i", "16i", "18i", "21i", "31i"
            };

            string modelType;
            do
            {
                Console.WriteLine("Enter machine model type (or 'q' to quit): ");
                modelType = Console.ReadLine().ToUpper(); // Convert to uppercase for case-insensitive matching
            } while (!validModels.Contains(modelType) && modelType != "Q");

            if (modelType == "Q")
            {
                Console.WriteLine("Exiting program.");
                return;
            }

            // Parameter entry loop (corrected to 9999)
            for (int i = 0; i < 9999; i++)
            {
                string parameterNumber = i.ToString().PadLeft(4, '0');
                parameters.GetUserInput(parameterNumber);

                // Save parameter data to the list after user input
                Parameter parameter = new Parameter
                {
                    ParameterNumber = parameterNumber,
                    Value = parameters.parameters.Last().Value // Assuming GetUserInput sets the value in the last element
                };
                parameters.parameters.Add(parameter);
            }

            // Implement saving functionality based on user choice
            string filename = "parameters.txt"; // Example filename
            string format = "text"; // Example format
            parameters.SaveToFile(filename, format);

            Console.WriteLine("Parameter data saved successfully!");
        }
    }
}
