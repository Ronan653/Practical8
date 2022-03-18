
using Microsoft.AspNetCore.Mvc;

using SMS.Data.Models;
using SMS.Data.Services;

namespace SMS.Web.Controllers
{
    public class StudentController : BaseController
    {
        private IStudentService svc;

        public StudentController()
        {
            svc = new StudentServiceDb();
        }

        // GET /student
        public IActionResult Index()
        {
            // complete this method
            var students = svc.GetStudents();
            
            return View(students);
        }

        // GET /student/details/{id}
        public IActionResult Details(int id)
        {  
            // retrieve the student with specifed id from the service
            var s = svc.GetStudent(id);

           
            if (s == null)
            {
                // TBC - Display suitable warning alert and redirect to Index               
                Alert($"No such student {id}", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }

            // pass student as parameter to the view
            return View(s);
        }

        // GET: /student/create
        public IActionResult Create()
        {   
            // display blank form to create a student
            return View();
        }

        // POST /student/create
        [HttpPost]
        public IActionResult Create(Student s)
        {
            if(svc.IsDuplicateStudentEmail(s.Email, s.Id))
            {
                ModelState.AddModelError(nameof(s.Email), "This Email address is already registered");

            }

            // complete POST action to add student
            if (ModelState.IsValid)
            {
                // pass data to service to store 
                svc.AddStudent(s.Name, s.Course, s.Email, s.Age, s.Grade, s.PhotoUrl);

                // TBC - display suitable success alert
                Alert($"Student {s.Id} Created", AlertType.success);
               
                return RedirectToAction(nameof(Details), new { ID = s.Id});

                
            }
            
            // redisplay the form for editing as there are validation errors
            return View(s);
        }

        // GET /student/edit/{id}
        public IActionResult Edit(int id)
        {        
            // load the student using the service
            var s = svc.GetStudent(id);

            // TBC check if s is null and return NotFound()
            if (s == null)
            {               
                // TBC - Display suitable warning alert and redirect to Index               
                Alert($"No such student {id} to Edit", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }   

            // pass student to view for editing
            return View(s);
        }

        // POST /student/edit/{id}
        [HttpPost]
        public IActionResult Edit(int id, Student s)
        {
            // TBC - check if the emailaddress is a duplicate and if so add a validation error
            if(svc.IsDuplicateStudentEmail(s.Email, s.Id))
            {
                ModelState.AddModelError(nameof(s.Email), "This Email address is already registered");

            }

            // complete POST action to save student changes
            if (ModelState.IsValid)
            {
                // pass data to service to update
                svc.UpdateStudent(s);
                
                // TBC - display suitable success alert and redirect to details view
                Alert($"Student {id} has been successfully edited", AlertType.success);
                return RedirectToAction(nameof(Details), new { Id = s.Id });
            }

            // redisplay the form for editing as validation errors
            return View(s);
        }

        // GET / student/delete/{id}
        public IActionResult Delete(int id)
        {       
            // load the student using the service
            var s = svc.GetStudent(id);
            // check the student exists
            if (s == null)
            {
                // TBC - Display suitable warning alert and redirect to Index               
                Alert($"No such student {id} to Delete", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }     
            
            // pass student to view for deletion confirmation
            return View(s);
        }

        // POST /student/delete/{id}
        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {           
            svc.DeleteStudent(id);

            // TBC display success alert
            Alert($"Student {id} has been Deleted", AlertType.success);

            // redirect to the index view
            return RedirectToAction(nameof(Index));
        }


        // ============== Student ticket management ==============

        // GET /student/createticket/{id}
        public IActionResult TicketCreate(int id)
        {     
            var s = svc.GetStudent(id);
            if (s == null)
            {
                // TBC - Display suitable warning alert and redirect to Index               
                Alert($"No such student {id} to create Ticket", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }

            // create a ticket view model and set foreign key
            var ticket = new Ticket { StudentId = id }; 
            // render blank form
            return View( ticket );
        }

        // POST /student/create
        [HttpPost]
        public IActionResult TicketCreate(Ticket t)
        {
            if (ModelState.IsValid)
            {                
                var ticket = svc.CreateTicket(t.StudentId, t.Issue);

                // TBC - display suitable success alert
                Alert($"Ticket created", AlertType.success);
              
                return RedirectToAction(nameof(Details), new { Id = ticket.StudentId });
            }
            // redisplay the form for editing
            return View(t);
        }

         // GET /student/ticketdelete/{id}
        public IActionResult TicketDelete(int id)
        {
            // load the ticket using the service
            var ticket = svc.GetTicket(id);
            // check the returned Ticket is not null and if so return NotFound()
            if (ticket == null)
            {
                // TBC - Display suitable warning alert and redirect to Index               
                Alert($"No Ticket to delete", AlertType.warning);
                return RedirectToAction(nameof(Index));

            }     
            
            // pass ticket to view for deletion confirmation
            return View(ticket);
        }

        // POST /student/ticketdeleteconfirm/{id}
        [HttpPost]
        public IActionResult TicketDeleteConfirm(int id, int studentId)
        {
            // delete student via service
            svc.DeleteTicket(id);
           
            // TBC - Display a suitable success Alert
            Alert($"Ticket Created", AlertType.success);
            
           
            // redirect to the index view
            return RedirectToAction(nameof(Details), new { Id = studentId });
        }


    }
}
