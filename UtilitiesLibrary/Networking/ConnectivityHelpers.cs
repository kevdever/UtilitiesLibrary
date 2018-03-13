using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesLibrary.Networking
{
    public class ConnectivityHelpers
    {
        /// <summary>
        /// Pings an ipv4 address, returning the status of that request.  If the request fails, then it is retried once after three seconds.
        /// If you pass in an instance of Logging.Logger, the details are saved to file.
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static async Task<IPStatus> PingIp(string ip, Logging.Logger logger = null)
        {
            var ipAsBytes = ip.Split('.').Select(b => byte.Parse(b)).ToArray();
            using (var pingSender = new Ping())
            {
                System.Net.IPAddress addr;
                PingReply reply;
                try
                {
                    addr = new System.Net.IPAddress(ipAsBytes);
                    reply = pingSender.Send(addr);

                    if (reply.Status != IPStatus.Success)
                    {
                        if (logger != null)
                        {
                            var error = $"Ping sent to {ip} without exception, but it was not successful. Ipstatus: {reply.Status.ToString()}.";
                            //var logger = new Logger("PingErrors.txt");
                            await logger.SaveLogEntryAsync(error).ConfigureAwait(false);
                        }

                        await Task.Delay(TimeSpan.FromSeconds(3)).ConfigureAwait(false);
                        reply = pingSender.Send(addr);
                    }

                    return reply.Status;
                }
                catch (Exception e)
                {
                    var error = $"Something went wrong pinging {ip}.  {Exceptions.ExceptionHelpers.ConcatInnerExceptions(e)}";
                    if (logger != null)
                        await logger.SaveLogEntryAsync(error).ConfigureAwait(false);

                    Debug.WriteLine(error);
                    return default(IPStatus);
                }
            }
        }

        /// <summary>
        /// Pings a url.
        /// </summary>
        /// <param name="_HostURI"></param>
        /// <param name="_PortNumber"></param>
        /// <returns></returns>
        public static bool PingHost(string _HostURI, int _PortNumber)
        {
            try
            {
                using (var client = new TcpClient(_HostURI, _PortNumber))
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
