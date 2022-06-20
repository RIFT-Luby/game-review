using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReview.Application.Options
{
    public class FileApiOptions
    {
        public string UserImgDirectory { get; set; } = "img\\games";
        public string GameImgDirectory { get; set; } = "img\\users";

        public string[] UserFileTypes { get; set; } = new string[0];
        public string[] GameFileTypes { get; set; } = new string[0];
        public string DefaultUserImgPath { get; set; } = "";
        public string DefaultGameImgPath { get; set; } = "";
    }
}
