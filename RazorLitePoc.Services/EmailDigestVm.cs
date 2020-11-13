using System;

namespace RazorLitePoc.Services
{
    public class EmailDigestVm
    {
        public EmailDigestVm() { }

        public EmailDigestVm(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}