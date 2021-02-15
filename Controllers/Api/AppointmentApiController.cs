using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AppointmentScheduling.Models.ViewModels;
using AppointmentScheduling.Services;
using AppointmentScheduling.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentScheduling.Controllers.Api
{
    [Route("api/Appointment")]
    [ApiController]
    public class AppointmentApiController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string loginUserId;
        private readonly string role;

        public AppointmentApiController(IAppointmentService appointmentService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor; // To get a generic user
            _appointmentService = appointmentService;
            loginUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier); // Populates an user Id
            role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }

        [HttpPost]
        [Route("SaveCalendarData")]
        public IActionResult SaveCalendarData(AppointmentViewModel data)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.status = _appointmentService.AddUpdate(data).Result;
                if (commonResponse.status == 1)
                {
                    commonResponse.message = Helper.appointmentUpdated;
                }
                if (commonResponse.status == 2)
                {
                    commonResponse.message = Helper.appointmentAdded;
                }
            } catch (Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = Helper.failure_code;
            }
            return Ok(commonResponse);
        }
    }
}