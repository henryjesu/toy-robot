using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RobotAPI.Model;

namespace RobotAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RobotCommandController : ControllerBase
    {
        [HttpGet("{commandStr}")]
        public ActionResult Get(string commandStr)
        {
            var returnMsg = "";
            var commandArray = commandStr.Split(' ');
            var sCommandType = commandArray[0];
            if (Enum.TryParse<Command>(sCommandType.Trim().ToUpper(), out Command command))
            {
                if (command == Command.PLACE)
                {
                    if (commandArray.Length > 1)
                    {
                        var commandArgs = commandArray[1];
                        if (!string.IsNullOrEmpty(commandArgs))
                        {
                            var commandArgsArray = commandArgs.Split(",");
                            if (commandArgsArray.Length == 3) //0,0,NORTH -- Requires 3 Valid Arguments
                            {
                                if (int.TryParse(commandArgsArray[0], out int x) &&
                                int.TryParse(commandArgsArray[1], out int y) &&
                                Enum.TryParse<Direction>(commandArgsArray[2].ToUpper(), out Direction direction))
                                {
                                    var location = new Location { X = x, Y = y };
                                    returnMsg = Robot.ProcessCommand(command, location, direction);
                                }
                                //else
                                //{
                                //    returnMsg = "Invalid Place Command Arguments.";
                                //}
                            }
                            //else
                            //{
                            //    returnMsg = "Invalid Place Command Arguments.";
                            //}
                        }
                        //else
                        //{
                        //    returnMsg = "Place Command arguments Location and Direction is required.";
                        //}
                    }
                    //else
                    //{
                    //    returnMsg = "Place Command arguments Location and Direction is required.";
                    //}
                }
                else
                {
                    returnMsg = Robot.ProcessCommand(command);
                }
            }
            //else
            //{
            //    returnMsg = "Invalid Command";
            //}
            return Ok(returnMsg);
        }
       
    }
}
