using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AHHelperTools
{
    public static class AHHelpers
    {
        private const string HttpContext = "MS_HttpContext";
        private const string RemoteEndpointMessage = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";

        public static Guid[] GetGuids(this string idsStr)
        {
            string[] split = idsStr.Split('|');
            List<Guid> retVal = new List<Guid>();
            Guid item = Guid.Empty;
            foreach (string str in split)
            {
                item = str.ToGuid();
                if (item != Guid.Empty)
                    retVal.Add(item);
            }
            return retVal.ToArray<Guid>();
        }

        public static Guid ToGuid(this string str)
        {
            try
            {
                Guid retVal = new Guid(str);
                return retVal;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        public static int ToInt(this string str)
        {
            try
            {
                int intValue = int.MinValue;
                if (int.TryParse(str, out intValue))
                    return intValue;
                return intValue;
            }
            catch (Exception)
            {
                return int.MinValue;
            }
        }

        public static DateTime ToDateTime(this string str)
        {
            DateTime dateValue = DateTime.MinValue;
            try
            {
                if (DateTime.TryParse(str, out dateValue))
                    return dateValue;
                return dateValue;
            }
            catch (Exception)
            {
                return dateValue;
            }
        }

        public static bool ToBool(this string str)
        {
            bool boolValue = false;
            try
            {
                if (bool.TryParse(str, out boolValue))
                    return boolValue;
                return boolValue;
            }
            catch (Exception)
            {
                return boolValue;
            }
        }

        /// <summary>
        /// get client IP
        /// </summary>
        /// <param name="request"></param>
        /// <returns>string: client IP</returns>
        public static string GetClientIpAddress(this HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey(HttpContext))
            {
                dynamic ctx = request.Properties[HttpContext];
                if (ctx != null)
                {
                    return ctx.Request.UserHostAddress;
                }
            }

            if (request.Properties.ContainsKey(RemoteEndpointMessage))
            {
                dynamic remoteEndpoint = request.Properties[RemoteEndpointMessage];
                if (remoteEndpoint != null)
                {
                    return remoteEndpoint.Address;
                }
            }

            return null;
        }
    }
}
