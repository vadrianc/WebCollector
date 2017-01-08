namespace WebCollector.Rule
{
    using System;
    using System.IO;
    using SoftwareControllerApi.Rule;

    /// <summary>
    /// Factory for creating processor objects using a file path.
    /// </summary>
    public static class ProcessorFactory
    {
        /// <summary>
        /// Create processor objects using a file path.
        /// </summary>
        /// <param name="file">The file path.</param>
        /// <returns>Processor object.</returns>
        public static IResultProcessor Create(string file)
        {
            if (!Path.HasExtension(file)) {
                string msg = string.Format("Expected a file, but was {0}", file);
                throw new ArgumentException(msg);
            }

            string extension = Path.GetExtension(file);

            switch (extension) {
            case ".csv":
                return new CsvResultProcessor(file);
            }

            throw new ApplicationException(string.Format("Unsupported extension: {0}", extension));
        }
    }
}
