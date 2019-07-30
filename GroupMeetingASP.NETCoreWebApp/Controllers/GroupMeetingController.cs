using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupMeetingASP.NETCoreWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroupMeetingASP.NETCoreWebApp.Controllers
{
    
    public class GroupMeetingController : Controller
    {
        GroupGreetingDbModule MeetingDbModule = new GroupGreetingDbModule();
        
        public IActionResult Index()
        {
            
            return View(MeetingDbModule.GetGroupMeetings());
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddGroupMeeting()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddGroupMeeting([Bind] GroupMeeting groupMeeting)
        {
            if (ModelState.IsValid)
            {
                if (MeetingDbModule.AddGroupMeeting(groupMeeting) > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(groupMeeting);
        }

        [Authorize]
        [HttpGet]
        public IActionResult EditMeeting(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            GroupMeeting group = MeetingDbModule.GetGroupMeetingById(id);
            if (group == null)
                return NotFound();
            return View(group);
        }

        [HttpPost]
        public IActionResult EditMeeting(int id, [Bind] GroupMeeting groupMeeting)
        {
            if (id != groupMeeting.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                MeetingDbModule.UpdateGroupMeeting(groupMeeting);
                return RedirectToAction("Index");
            }
            return View(groupMeeting);
        }

        [Authorize]
        public IActionResult DeleteMeeting(int id)
        {
            GroupMeeting group = MeetingDbModule.GetGroupMeetingById(id);
            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }
        [HttpPost]
        public IActionResult DeleteMeeting(int id, GroupMeeting groupMeeting)
        {
            if (MeetingDbModule.DeleteGroupMeeting(id) > 0)
            {
                return RedirectToAction("Index");
            }
            return View(groupMeeting);
        }
    }
}