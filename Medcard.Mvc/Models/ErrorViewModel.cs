using System;

namespace Medcard.Mvc.Models
{
    [Serializable]
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}