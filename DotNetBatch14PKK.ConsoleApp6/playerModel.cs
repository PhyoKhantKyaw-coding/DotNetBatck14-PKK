using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.ConsoleApp6
{

    public class ResponseModel
    {
        public bool IsSuccessful { get; set; }
        public string? Message { get; set; }
    }

    public class PlayerModel
    {
        public int PlayerID { get; set; }
        public string? PlayerName { get; set; }
        public int CurrentPosition { get; set; }
    }

}
