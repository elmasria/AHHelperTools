using AHHelperTools.DTOs;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace AHHelperTools
{
    public static class AHNetwork
    {
        private const string HttpContext = "MS_HttpContext";
        private const string RemoteEndpointMessage = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";

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

        /// <summary>
        /// Calling web api using post method
        /// </summary>
        /// <param name="URL"></param>
        /// <param name="DATA"></param>
        /// <returns></returns>
        public static string CallWebAPIPOST(string URL, string DATA)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "application/json";
            byte[] bdata = Encoding.UTF8.GetBytes(DATA);
            request.ContentLength = bdata.Length;
            using (Stream webStream = request.GetRequestStream())
            {
                webStream.Write(bdata, 0, bdata.Length);
                webStream.Close();
            }
            try
            {
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            string result = responseReader.ReadToEnd();
                            webResponse.Close();
                            return result;
                        }
                    }
                    webResponse.Close();
                    return string.Empty;
                }
            }
            catch (WebException e)
            {
                AHLogs.Log("WebException", e);
                return e.Message;
            }
            catch (Exception e)
            {
                AHLogs.Log("Exception", e);
                return e.Message;
            }
        }

        /// <summary>
        /// Calling a web api using Get method
        /// </summary>
        /// <param name="URL"></param>
        /// <returns></returns>
        public static string CallWebAPIGET(string URL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "GET";
            request.ContentType = "application/json";
            try
            {
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            string result = responseReader.ReadToEnd();
                            webResponse.Close();
                            return result;
                        }
                    }
                    webResponse.Close();
                    return string.Empty;
                }
            }
            catch (WebException e)
            {
                AHLogs.Log("WebException", e);
                return e.Message;
            }
            catch (Exception e)
            {
                AHLogs.Log("Exception", e);
                return e.Message;
            }
        }

        /// <summary>
        /// Calling a web api using Get method
        /// return more details
        /// </summary>
        /// <param name="URL"></param>
        /// <param name="Result"></param>
        public static void CallWebAPIGET(string URL, out WebAPIResult Result)
        {
            Result = new WebAPIResult();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "GET";
            request.ContentType = "application/json";
            try
            {
                HttpWebResponse webResponse = request.GetResponse() as HttpWebResponse;
                Result.StatusCode = webResponse.StatusCode;

                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            Result.Data = responseReader.ReadToEnd();
                        }
                    }
                    else
                    {
                        Result.Description = HttpStatusCode.NoContent.ToString();
                    }
                }
                webResponse.Close();
            }
            catch (WebException e)
            {
                HttpWebResponse errorResponse = e.Response as HttpWebResponse;
                Result.StatusCode = errorResponse.StatusCode;
                if (errorResponse.StatusCode != HttpStatusCode.NotFound)
                {
                    Result.Description = e.Status + " - " + e.Message;
                    AHLogs.Log("WebException - " + e.Status, e);
                }

            }
            catch (Exception e)
            {
                Result.StatusCode = HttpStatusCode.InternalServerError;
                Result.Description = e.Message;
                AHLogs.Log("Exception", e);
            }
        }

        public static void CallWebAPIPOST(string URL, string DATA, out WebAPIResult Result)
        {
            Result = new WebAPIResult();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "application/json";
            byte[] bdata = Encoding.UTF8.GetBytes(DATA);
            request.ContentLength = bdata.Length;
            using (Stream webStream = request.GetRequestStream())
            {
                webStream.Write(bdata, 0, bdata.Length);
                webStream.Close();
            }
            try
            {
                HttpWebResponse webResponse = request.GetResponse() as HttpWebResponse;
                Result.StatusCode = webResponse.StatusCode;
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            Result.Data = responseReader.ReadToEnd();
                        }
                    }
                    else
                    {
                        Result.Description = HttpStatusCode.NoContent.ToString();
                    }
                    webResponse.Close();
                }
            }
            catch (WebException e)
            {
                HttpWebResponse errorResponse = e.Response as HttpWebResponse;
                Result.StatusCode = errorResponse.StatusCode;
                if (errorResponse.StatusCode != HttpStatusCode.NotFound)
                {
                    Result.Description = e.Status + " - " + e.Message;
                    AHLogs.Log("WebException - " + e.Status, e);
                }

            }
            catch (Exception e)
            {
                Result.StatusCode = HttpStatusCode.InternalServerError;
                Result.Description = e.Message;
                AHLogs.Log("Exception", e);
            }
        }
    }
}
