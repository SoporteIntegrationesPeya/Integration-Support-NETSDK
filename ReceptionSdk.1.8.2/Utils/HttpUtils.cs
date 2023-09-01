using ReceptionSdk.Exceptions;
using ReceptionSdk.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReceptionSdk.Utils
{
    public static class HttpUtils
    {
        public static bool ResponseIsSuccessful(Response response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                throw new ApiException(response);
            }
        }

        public static IList<object> ResponseData(Response response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Body.Data;
            }
            else
            {
                throw new ApiException(response);
            }
        }

        public static object ResponseBody(Response response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Body;
            }
            else
            {
                throw new ApiException(response);
            }
        }
    }
}
