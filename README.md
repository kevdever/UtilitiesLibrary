# Utilities Library

A set of C# utilitity methods.

## UtilitiesLibrary.Exceptions
  * ExceptionHelpers
    * public static string ConcatInnerExceptions(Exception e)

## UtilitiesLibrary.Logging
  * Logger --> A threadsafe logger.
    * public Logger(string path)
    * public Logger(string applicationName, string filename, string subfolderName = null)
    * public void SaveLogEntry(string message)
    * public async Task SaveLogEntryAsync(string message)

## UtilitiesLibrary.IEnumerableExtensions
  * Extensions
    * public static IEnumerable<IEnumerable<T>> SplitIntoNChunks<T>(this IEnumerable<T> source, int numChunks)
    * public static IEnumerable<IEnumerable<T>> SplitIntoBatchesOfSize<T>(this IEnumerable<T> source, int batchSize)
    * public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    * public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
  * RandomNumbers
    * public static IEnumerable<int> GetRandomNumbers(int numValues, int maxVal, bool allowRepetition = false)

## UtilitiesLibrary.MathConvenience
  * MathConvenience
      * public static double Max(params double[] values)
      * public static double Min(params double[] values)    
## UtilitiesLibrary.Networking
  * ConnectivityHelpers
    * public static async Task<IPStatus> PingIp(string ip, Logging.Logger logger = null)
    * public static bool PingHost(string _HostURI, int _PortNumber)