using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ReqResLog.HttpLogger.Models
{
    internal static class MiddlewareModelExtensions
    {
        public static ContextData ApplyLogOptions(this ContextData data, IDictionary<string, string> filters)
        {
            if (data == null || filters == null || filters.Count == 0)
                return data;

            var bodyHasValue = !string.IsNullOrWhiteSpace(data.Request.Body);
            var formKyes = data.Request.Form == null ? new string[] { } : data.Request.Form.Keys.ToArray();

            foreach (var filter in filters)
            {
                var replacement = filter.Value ?? "";

                foreach (var key in formKyes)
                {
                    if (data.Request.Form[key] != null
                        && key.Equals(filter.Key, System.StringComparison.OrdinalIgnoreCase))
                    {
                        data.Request.Form[key] = replacement;
                    }
                }

                if (bodyHasValue)
                {
                    var pattern = $@"""{filter.Key}""\s*:\s*""(?<VALUE>.*?)""";
                    var regex = new Regex(pattern, RegexOptions.IgnoreCase);
                    data.Request.Body = regex.ReplaceGroup(data.Request.Body, "VALUE", replacement);
                }
            }

            return data;
        }

        internal static List<KeyValue> ToList(this IRequestCookieCollection obj)
        {
            if (obj == null)
                return null;

            var list = new List<KeyValue>();
            foreach (var key in obj.Keys)
            {
                if (key != null)
                {
                    if (obj.TryGetValue(key, out string value))
                        list.Add(new KeyValue(key, value));
                    else
                        list.Add(new KeyValue(key, "__ERROR_READING_VALUE__"));
                }
            }

            return list;
        }

        internal static Dictionary<string, string> ToDictionary(this IRequestCookieCollection obj)
        {
            if (obj == null)
                return null;

            var dic = new Dictionary<string, string>();
            foreach (var key in obj.Keys)
            {
                if (key != null)
                {
                    if (obj.TryGetValue(key, out string value))
                        dic.Add(key, value);
                    else
                        dic.Add(key, "__ERROR_READING_VALUE__");
                }
            }

            return dic;
        }

        internal static List<KeyValue> ToList(this IFormCollection obj)
        {
            if (obj == null)
                return null;

            var list = new List<KeyValue>();
            foreach (var key in obj.Keys)
            {
                if (key != null)
                {
                    if (obj.TryGetValue(key, out StringValues value))
                        list.Add(new KeyValue(key, value.ToString()));
                    else
                        list.Add(new KeyValue(key, "__ERROR_READING_VALUE__"));
                }
            }

            return list;
        }

        internal static Dictionary<string, string> ToDictionary(this IFormCollection obj)
        {
            if (obj == null)
                return null;

            var dic = new Dictionary<string, string>();
            foreach (var key in obj.Keys)
            {
                if (key != null)
                {
                    if (obj.TryGetValue(key, out StringValues value))
                        dic.Add(key, value);
                    else
                        dic.Add(key, "__ERROR_READING_VALUE__");
                }
            }

            return dic;
        }

        internal static List<KeyValue> ToList(this IFormFileCollection obj)
        {
            if (obj == null)
                return null;

            var list = new List<KeyValue>();
            foreach (var file in obj)
            {
                if (file != null)
                    list.Add(new KeyValue(file.Name, file.FileName));
            }

            return list;
        }

        //internal static List<FormFileData> ToList(this IFormFileCollection obj)
        //{
        //    if (obj == null)
        //        return null;

        //    var list = new List<FormFileData>();
        //    foreach (var file in obj)
        //    {
        //        if (file != null)
        //        {
        //            list.Add(new FormFileData
        //            {
        //                ContentType = file.ContentType,
        //                FileName = file.FileName,
        //                Headers = file.Headers.ToList(),
        //                Length = file.Length,
        //                Name = file.Name,
        //            });
        //        }
        //    }

        //    return list;
        //}

        internal static Dictionary<string, string> ToDictionary(this IFormFileCollection obj)
        {
            if (obj == null)
                return null;

            var dic = new Dictionary<string, string>();
            foreach (var file in obj)
            {
                if (file != null)
                    dic.Add(file.Name, file.FileName);
            }

            return dic;
        }

        internal static List<KeyValue> ToList(this IHeaderDictionary obj)
        {
            if (obj == null)
                return null;

            var list = new List<KeyValue>();
            foreach (var key in obj.Keys)
            {
                if (key != null)
                {
                    if (obj.TryGetValue(key, out StringValues value))
                        list.Add(new KeyValue(key, value.ToString()));
                    else
                        list.Add(new KeyValue(key, "__ERROR_READING_VALUE__"));
                }
            }

            return list;
        }

        internal static Dictionary<string, string> ToDictionary(this IHeaderDictionary obj)
        {
            if (obj == null)
                return null;

            var dic = new Dictionary<string, string>();
            foreach (var key in obj.Keys)
            {
                if (key != null)
                {
                    if (obj.TryGetValue(key, out StringValues value))
                        dic.Add(key, value);
                    else
                        dic.Add(key, "__ERROR_READING_VALUE__");
                }
            }

            return dic;
        }

        internal static List<KeyValue> ToList(this IQueryCollection obj)
        {
            if (obj == null)
                return null;

            var list = new List<KeyValue>();
            foreach (var key in obj.Keys)
            {
                if (key != null)
                {
                    if (obj.TryGetValue(key, out StringValues value))
                        list.Add(new KeyValue(key, value.ToString()));
                    else
                        list.Add(new KeyValue(key, "__ERROR_READING_VALUE__"));
                }
            }

            return list;
        }

        internal static Dictionary<string, string> ToDictionary(this IQueryCollection obj)
        {
            if (obj == null)
                return null;

            var dic = new Dictionary<string, string>();
            foreach (var key in obj.Keys)
            {
                if (key != null)
                {
                    if (obj.TryGetValue(key, out StringValues value))
                        dic.Add(key, value);
                    else
                        dic.Add(key, "__ERROR_READING_VALUE__");
                }
            }

            return dic;
        }

        //internal static async Task<string> ReadBodyAsStringAsync(this HttpRequest request)
        //{
        //    if (request.ContentLength == null || request.ContentLength == 0 || request.Body == null)
        //        return null;

        //    string body = null;

        //    /* request body is a FrameRequestStream and it doesn’t support seeking.
        //     * EnableRewind() allows us to set the reader for the request back at the beginning of its stream. */
        //    request.EnableRewind();
        //    request.Body.Seek(0, SeekOrigin.Begin);

        //    using (var reader = new StreamReader(request.Body))
        //    {
        //        body = await reader.ReadToEndAsync();

        //        /* we need to reset the reader, so that the client can read it.
        //         * seeking must be inside the 'using' block. */
        //        request.Body.Seek(0, SeekOrigin.Begin);
        //    }

        //    return body;
        //}

        //internal static async Task<string> ReadBodyAsStringAsync(this HttpResponse response)
        //{
        //    if (response.HasStarted)
        //        return "__RESPONSE_STARTED__";

        //    if (response.ContentLength == null || response.ContentLength == 0 || response.Body == null)
        //        return null;

        //    string body = null;

        //    // we need to read the response stream from the beginning...
        //    response.Body.Seek(0, SeekOrigin.Begin);
        //    using (var reader = new StreamReader(response.Body))
        //    {
        //        body = await reader.ReadToEndAsync();

        //        /* we need to reset the reader, so that the client can read it.
        //         * seeking must be inside the 'using' block. */
        //        response.Body.Seek(0, SeekOrigin.Begin);
        //    }

        //    return body;
        //}
    }
}
