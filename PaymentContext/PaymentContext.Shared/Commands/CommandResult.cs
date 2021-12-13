using PaymentContext.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentContext.Shared.Commands
{
    public class CommandResult : ICommandResult
    {
        public CommandResult()
        {

        }

        public CommandResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
