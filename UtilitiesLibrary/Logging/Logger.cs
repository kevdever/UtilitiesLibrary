/*
 * Copyright (c) 2018 FichterApps, LLC

 MIT License
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 * 
 * 
 * */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UtilitiesLibrary.Logging
{
    public class Logger
    {
        private static readonly Lazy<SemaphoreSlim> _mutex = new Lazy<SemaphoreSlim>(() => new SemaphoreSlim(1, 1));
        private readonly string _fullPath;

        /// <summary>
        /// Saves a file to the provided filepath. Include full path, filename, and extension.
        /// </summary>
        /// <param name="path"></param>
        public Logger(string path)
        {
            _fullPath = path;
        }

        /// <summary>
        /// Saves a file with the given name to the ApplicationData folder with the given applicationname.  Optionally include a subfolder between the file and the application directly.
        /// This method designed for use with WPF.
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="filename"></param>
        /// <param name="subfolderName"></param>
        public Logger(string applicationName, string filename, string subfolderName = null)
        {
            var _rootPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            if (string.IsNullOrWhiteSpace(subfolderName))
                _fullPath = Path.Combine(_rootPath, applicationName, filename);
            else
                _fullPath = Path.Combine(_rootPath, applicationName, subfolderName, filename);
        }

        public void SaveLogEntry(string message)
        {
            _mutex.Value.Wait();
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_fullPath));

                using (StreamWriter w = File.AppendText(_fullPath))
                    w.WriteLine($"{DateTime.Now}:  {message}");
            }
            finally
            {
                _mutex.Value.Release();
            }
        }

        public async Task SaveLogEntryAsync(string message)
        {
            await _mutex.Value.WaitAsync().ConfigureAwait(false);
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_fullPath));

                using (StreamWriter w = File.AppendText(_fullPath))
                    await w.WriteLineAsync($"{DateTime.Now}:  {message}").ConfigureAwait(false);
            }
            finally
            {
                _mutex.Value.Release();
            }
        }
    }
}
