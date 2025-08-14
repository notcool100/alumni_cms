using System;
using System.ComponentModel.DataAnnotations;

namespace AlumniBackendApi.Models
{
    public class UrlAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return true;

            return Uri.TryCreate(value.ToString(), UriKind.Absolute, out Uri uriResult) 
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}